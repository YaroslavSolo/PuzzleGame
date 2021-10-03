using System.Windows.Controls;

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
