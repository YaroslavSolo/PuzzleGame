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

        public bool Horizontal
        {
            get
            {
                return RenderTransform != MatrixTransform.Identity;
            }

            set
            {
                if (value)
                    RenderTransform = new RotateTransform(90);
                else
                    RenderTransform = MatrixTransform.Identity;
            }
        }

        public bool Occupied
        {
            get
            {
                return ContentMatch != null;
            }
        }

        public Slot()
        {
            InitializeComponent();
        }
    }
}
