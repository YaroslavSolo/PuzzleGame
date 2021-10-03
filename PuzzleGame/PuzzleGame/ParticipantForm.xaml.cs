﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using DataAnalysis;

namespace PuzzleGame
{
    /// <summary>
    /// Логика взаимодействия для ParticipantForm.xaml
    /// </summary>
    public partial class ParticipantForm : ContentControl
    {
        public TestingParameters TestingParams { get; set; }

        public ParticipantForm(TestingParameters parameters)
        {
            TestingParams = parameters;
            InitializeComponent();
            description.Text = parameters.Description;
        }

        private void LengthValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = nickNameTextBox.Text.Length > 25;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || numericTextBox.Text.Length > 2;
        }

        private void ShowExample(object sender, RoutedEventArgs e)
        {
            string gender = "not_stated";

            if (maleRadioButton.IsChecked.Value)
                gender = "male";
            if (femaleRadioButton.IsChecked.Value)
                gender = "female";

            if (nickNameTextBox.Text != "" && numericTextBox.Text != "")
            {
                Datawriter.DataConsumer(nickNameTextBox.Text, System.DateTime.Now, int.Parse(numericTextBox.Text), 0, gender);
                Content = new ExamplePage(TestingParams);
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
