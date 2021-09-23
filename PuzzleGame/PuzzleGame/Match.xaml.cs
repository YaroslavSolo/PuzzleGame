using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public double X { get; set; }

        public double Y { get; set; }

        public double RealX
        {
            get
            {
                return X + GetOffset().X;
            }
        }

        public double RealY
        {
            get
            {
                return Y + GetOffset().Y;
            }
        }

        public TranslateTransform GetOffset()
        {
            foreach (var el in ((TransformGroup)RenderTransform).Children)
            {
                if (el is TranslateTransform)
                    return (TranslateTransform)el;
            }

            return null;
        }

        public int SymbolNum { get; set; }

        public int MatchNum { get; set; }

        public bool Horizontal { get; set; }

        private MatchesPuzzle Puzzle { get; set; }

        public Slot Slot { get; set; }

        public string Id
        {
            get
            {
                return $"{SymbolNum}_{MatchNum}";
            }
        }

        public void SetCoordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Dist(Slot slot)
        {
            return Math.Sqrt(Math.Pow(RealX - slot.X, 2) + Math.Pow(RealY - slot.Y, 2));
        }

        public void AttachToSlot()
        {
            Puzzle.AttachToSlot(this);
        }

        public Match(MatchesPuzzle puzzle, int symbolNum, int matchNum = 0)
        {
            InitializeComponent();
            Puzzle = puzzle;
            SymbolNum = symbolNum;
            MatchNum = matchNum;
        }
    }
}
