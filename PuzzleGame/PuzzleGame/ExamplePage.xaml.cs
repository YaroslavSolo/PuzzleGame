using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PuzzleInterpretation;
using System.Windows.Threading;
using DataAnalysis;

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
            movesLeft.Text = puzzles[curPuzzle].MatchesToMoveLeft.ToString();

            foreach (var puzzle in puzzles)
            {
                puzzle.AttemptsLeft = parameters.NumAttempts;
                puzzle.TimeLeft = parameters.AttemptDuration;
                puzzle.ParentPage = this;
            }

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(GameTick);

            RenderPuzzle();

            timer.Start();
            Datawriter.DataConsumer("Probe_start", System.DateTime.Now, 0, 0, "");
        }

        private void RenderPuzzle()
        {
            canvas.Children.Clear();
            puzzles[curPuzzle].RenderSlots(canvas, 125, 60, TestingParams.AreSlotsVisible);
            puzzles[curPuzzle].Render(canvas, 125, 60);
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (--puzzles[curPuzzle].TimeLeft > 0)
            {
                timeLeft.Text = puzzles[curPuzzle].TimeLeft.ToString();
            }
            else
            {
                timeLeft.Text = "0";
                timer.Stop();
                MessageBox.Show("Время на решения головоломки истекло", "Увы...", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                NextPuzzle();
            }
        }

        private void NewAttempt(object sender, RoutedEventArgs e)
        {
            if (--puzzles[curPuzzle].AttemptsLeft > 0)
            {
                puzzles[curPuzzle].MatchesToMoveLeft = puzzles[curPuzzle].MatchesToMove;
                numAttemptsLeft.Text = puzzles[curPuzzle].AttemptsLeft.ToString();
                movesLeft.Text = puzzles[curPuzzle].MatchesToMove.ToString();
                RenderPuzzle();
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
            Datawriter.DataConsumer("Probe_end", System.DateTime.Now, 0, 0, puzzles[curPuzzle].IsSolved.ToString());

            timer.Stop();
            if (TestingParams.IsFeedbackNeeded && timeLeft.Text != "0")
            {
                if (puzzles[curPuzzle].IsSolved)
                    MessageBox.Show("Задание решено верно", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                else
                    MessageBox.Show("Задание решено неверно", "Увы...", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            if (++curPuzzle < puzzles.Count)
            {
                numAttemptsLeft.Text = puzzles[curPuzzle].AttemptsLeft.ToString();
                newAttempt.IsEnabled = true;
                RenderPuzzle();
                movesLeft.Text = puzzles[curPuzzle].MatchesToMoveLeft.ToString();
                timer.Start();
                Datawriter.DataConsumer("Probe_start", System.DateTime.Now, 0, 0, "");
            }
            else
            {
                Content = new FinalWindow();
            }
        }
    }
}
