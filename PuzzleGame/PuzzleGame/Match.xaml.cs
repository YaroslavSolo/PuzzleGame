using System;
using System.Windows.Controls;
using System.Windows.Media;
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

        public double TrueOffsetX { get; set; }

        public double TrueOffsetY { get; set; }

        public double RealX
        {
            get
            {
                if (Horizontal)
                {
                    return X + TrueOffsetX;
                }
                else
                    return X + GetOffset().X;
            }
        }

        public double RealY
        {
            get
            {
                if (Horizontal)
                {
                    return Y + TrueOffsetY;
                }
                else
                    return Y + GetOffset().Y;
            }
        }

        public int SymbolNum { get; set; }

        public int MatchNum { get; set; }

        public bool Horizontal { get; set; }

        public bool WasMoved { get; set; }

        public MatchesPuzzle Puzzle { get; set; }

        public Slot Slot { get; set; }

        public string Id
        {
            get
            {
                return $"{SymbolNum}_{MatchNum}";
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
