using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Input;
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
            LoadDescription();
        }

        private void LoadDescription()
        {
            string rawDescription = null;
            try
            {
                rawDescription = File.ReadAllText("description.txt");
            }
            catch
            { }

            if (rawDescription != null && rawDescription != "")
            {
                description.Text = rawDescription;
            }
        }

        private void SaveDescription()
        {
            try
            {
                File.WriteAllText("description.txt", description.Text);
            }
            catch
            { }
        }

        private void NumberValidationTextBox1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || attemptDuration.Text.Length > 3;
        }

        private void NumberValidationTextBox2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || numAttempts.Text.Length > 2;
        }

        private void StartTest(object sender, RoutedEventArgs e)
        {
            if (parameters.Puzzles.Count == 0)
            {
                MessageBox.Show("Задания не были загружены, так как файл не был выбран или содержит ошибки",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (attemptDuration.Text != "" && numAttempts.Text != "")
            {
                parameters.AttemptDuration = int.Parse(attemptDuration.Text);
                parameters.NumAttempts = int.Parse(numAttempts.Text);
                parameters.Description = description.Text;
                parameters.IsFeedbackNeeded = isFeedbackNeeded.IsChecked.Value;
                parameters.AreSlotsVisible = areSlotsVisible.IsChecked.Value;

                SaveDescription();
                Content = new ParticipantForm(parameters);
            }
            else
            {
                MessageBox.Show("Заданы не все параметры эксперимента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

                try
                {
                    foreach (var puzzle in rawPuzzles)
                    {
                        puzzles.Add(new MatchesPuzzle(puzzle));
                    }
                }
                catch
                {}

                parameters.Puzzles = puzzles;
            }
        }
    }
}
