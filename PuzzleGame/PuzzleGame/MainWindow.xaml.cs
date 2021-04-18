using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using FileReading;
using PuzzleInterpretation;

namespace PuzzleGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestingParameters parameters = new TestingParameters();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StartTest(object sender, RoutedEventArgs e)
        {
            if (parameters.Puzzles.Count == 0)
            {
                MessageBox.Show("Задания не были загружены, так как файл не был выбран или содержит ошибки", "Ошибка");
                return;
            }

            if (attemptDuration.Text != "" && numAttempts.Text != "")
            {
                parameters.AttemptDuration = int.Parse(attemptDuration.Text);
                parameters.NumAttempts = int.Parse(numAttempts.Text);
                parameters.Description = description.Text;
                parameters.IsFeedbackNeeded = isFeedbackNeeded.IsEnabled;

                Content = new ParticipantForm(parameters);
            }
            else
            {
                MessageBox.Show("Заданы не все параметры эксперимента", "Ошибка");
            }
        }

        private void ChoosePuzzleFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileName.Text = openFileDialog.FileName;
                var rawPuzzles = FileStreams.readCsvFile(openFileDialog.FileName);
                List<MatchesPuzzle> puzzles = new List<MatchesPuzzle>();

                foreach (var puzzle in rawPuzzles)
                {
                    puzzles.Add(new MatchesPuzzle(puzzle));
                }

                parameters.Puzzles = puzzles;
            }
        }
    }
}
