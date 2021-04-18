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

namespace PuzzleGame
{
    /// <summary>
    /// Логика взаимодействия для Match.xaml
    /// </summary>
    public partial class Match : UserControl
    {
        public Match()
        {
            InitializeComponent();      
        }

        public bool Horizontal
        {
            get
            {
                return (bool)GetValue(HorizontalProperty);
            }
            set
            {
                SetValue(HorizontalProperty, value);     
                
            }
        }

        public static readonly DependencyProperty HorizontalProperty =
            DependencyProperty.Register("Horizontal", typeof(bool), typeof(Match), new PropertyMetadata(false));
    }
}
