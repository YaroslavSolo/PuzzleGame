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
using System.Windows.Shapes;

namespace PuzzleGame
{
    /// <summary>
    /// Логика взаимодействия для Slot.xaml
    /// </summary>
    public partial class Slot : UserControl
    {
        public Match ContentMatch { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public bool Horizontal { get; set; }

        public bool Occupied
        {
            get
            {
                return ContentMatch != null;
            }
        }

        public void SetCoordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Slot()
        {
            InitializeComponent();
        }
    }
}
