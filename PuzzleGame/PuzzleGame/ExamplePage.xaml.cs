using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PuzzleInterpretation;
using System.Windows.Threading;

namespace PuzzleGame
{
    /// <summary>
    /// Логика взаимодействия для ExamplePage.xaml
    /// </summary>
    public partial class ExamplePage : ContentControl
    {
        public TestingParameters TestingParams { get; set; }

        private List<MatchesPuzzle> puzzles;

        private int curPuzzle = 0;

        private DispatcherTimer timer = new DispatcherTimer();

        public ExamplePage(TestingParameters parameters)
        {
            TestingParams = parameters;
            InitializeComponent();
            puzzles = TestingParams.Puzzles;
            numAttemptsLeft.Text = TestingParams.NumAttempts.ToString();
            timeLeft.Text = TestingParams.AttemptDuration.ToString();

            foreach (var puzzle in puzzles)
            {
                puzzle.AttemptsLeft = parameters.NumAttempts;
                puzzle.TimeLeft = parameters.AttemptDuration;
            }

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(GameTick);

            puzzles[curPuzzle].RenderSlots(canvas, 20, 40);
            puzzles[curPuzzle].Render(canvas, 20, 40);
        }

        void GameTick(object sender, EventArgs e)
        {
            if (--puzzles[curPuzzle].TimeLeft > 0)
            {
                timeLeft.Text = puzzles[curPuzzle].TimeLeft.ToString();
            }
            else
            {
                timeLeft.Text = "0";
                timer.Stop();
                NextPuzzle();
            }
        }

        private void NewAttempt(object sender, RoutedEventArgs e)
        {
            if (--puzzles[curPuzzle].AttemptsLeft > 0)
            {           
                numAttemptsLeft.Text = puzzles[curPuzzle].AttemptsLeft.ToString();
                canvas.Children.Clear();
                puzzles[curPuzzle].RenderSlots(canvas, 20, 40);
                puzzles[curPuzzle].Render(canvas, 20, 40);
            }
            else
            {
                numAttemptsLeft.Text = "0";
                newAttempt.IsEnabled = false;
            }
        }

        private void NextPuzzleClick(object sender, RoutedEventArgs e)
        {
            NextPuzzle();    
        }

        private void NextPuzzle()
        {
            timer.Stop();
            if (TestingParams.IsFeedbackNeeded)
            {
                if (puzzles[curPuzzle].IsSolved)
                    MessageBox.Show("Задание решено верно", "Поздравляем!");
                else
                    MessageBox.Show("Задание решено неверно", "Увы...");
            }

            if (++curPuzzle < puzzles.Count)
            {
                numAttemptsLeft.Text = puzzles[curPuzzle].AttemptsLeft.ToString();
                newAttempt.IsEnabled = true;
                canvas.Children.Clear();
                puzzles[curPuzzle].RenderSlots(canvas, 20, 40);
                puzzles[curPuzzle].Render(canvas, 20, 40); 
                timer.Start();
            }
            else
            {
                Content = new FinalWindow();
            }
        }
    }
}
