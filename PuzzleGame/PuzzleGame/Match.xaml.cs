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

namespace PuzzleGame
{
    /// <summary>
    /// Логика взаимодействия для Match.xaml
    /// </summary>
    public partial class Match : UserControl
    {
        private MatchesPuzzle Puzzle { get; set; }

        public Match(MatchesPuzzle puzzle)
        {
            InitializeComponent();
            Puzzle = puzzle;
        }
    }
}
