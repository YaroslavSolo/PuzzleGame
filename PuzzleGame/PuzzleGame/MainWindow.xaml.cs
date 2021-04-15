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

using DataAnalysis;

namespace PuzzleGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            if (attemptDuration.Text != "" && numAttempts.Text != "")
            {
                TestingParameters parameters = new TestingParameters();

                parameters.AttemptDuration = int.Parse(attemptDuration.Text);
                parameters.NumAttempts = int.Parse(numAttempts.Text);
                parameters.Description = description.Text;
                parameters.FeedbackIsNeeded = isFeedbackNeeded.IsEnabled;

                Content = new ParticipantForm(parameters);
            }
            else
            {
                MessageBox.Show("Заданы не все параметры эксперимента");
            }
        }

        private void ChoosePuzzleFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileName.Text = openFileDialog.FileName;
            }
        }
    }
}
