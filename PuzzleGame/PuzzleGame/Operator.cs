using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PuzzleGame;

namespace PuzzleInterpretation
{
    public class Operator
    {
        /// <summary>
        /// It's complicated :/
        /// </summary>
        private bool[] m = new bool[4];

        public void RenderSlots(Panel canvas, int x, int y)
        {
            RotateTransform rotate = new RotateTransform(90);

            Rectangle slot1 = new Rectangle
            {
                Width = 10,
                Height = 100,
                Fill = Brushes.LightGray
            };

            Rectangle slot2 = new Rectangle
            {
                Width = 10,
                Height = 100,
                Fill = Brushes.LightGray
            };

            switch (RepresentedOperator())
            {
                case '=':
                    Canvas.SetLeft(slot1, x);
                    Canvas.SetTop(slot1, 25 + y);

                    Canvas.SetLeft(slot2, x);
                    Canvas.SetTop(slot2, 65 + y);
                    slot1.RenderTransform = rotate;
                    slot2.RenderTransform = rotate;
                    canvas.Children.Add(slot2);
                    break;
                case '-':
                    Canvas.SetLeft(slot1, x);
                    Canvas.SetTop(slot1, 45 + y);
                    slot1.RenderTransform = rotate;
                    break;
                case '+':
                    Canvas.SetLeft(slot1, x);
                    Canvas.SetTop(slot1, 45 + y);

                    Canvas.SetLeft(slot2, -55 + x);
                    Canvas.SetTop(slot2, y);
                    slot1.RenderTransform = rotate;
                    canvas.Children.Add(slot2);
                    break;
            }

            canvas.Children.Add(slot1);
        }

        public void Render(Panel canvas, List<Match> allMatches, int x, int y)
        {
            TransformGroup group = null;
            RotateTransform rotate = new RotateTransform(90);
            Match slot1 = new Match();
            Match slot2 = new Match();

            switch (RepresentedOperator())
            {
                case '=':
                    Canvas.SetLeft(slot1, -55 + x);
                    Canvas.SetTop(slot1, -20 + y);

                    Canvas.SetLeft(slot2, -55 + x);
                    Canvas.SetTop(slot2, 20 + y);

                    group = (TransformGroup)slot1.RenderTransform;
                    group.Children.Add(rotate);
                    group = (TransformGroup)slot2.RenderTransform;
                    group.Children.Add(rotate);

                    canvas.Children.Add(slot2);
                    break;
                case '-':
                    Canvas.SetLeft(slot1, -55 + x);
                    Canvas.SetTop(slot1, y);
                    group = (TransformGroup)slot1.RenderTransform;
                    group.Children.Add(rotate);
                    break;
                case '+':
                    Canvas.SetLeft(slot1, -55 + x);
                    Canvas.SetTop(slot1, y);

                    Canvas.SetLeft(slot2, x);
                    Canvas.SetTop(slot2, y);
                    group = (TransformGroup)slot1.RenderTransform;
                    group.Children.Add(rotate);
                    canvas.Children.Add(slot2);
                    break;
            }

            canvas.Children.Add(slot1);
        }

        public char RepresentedOperator()
        {
            bool[] conditions = new bool[3];

            conditions[0] = m[0] && m[2] && !(m[1] || m[3]);    // =
            conditions[1] = m[1] && !(m[2] || m[3] || m[0]);    // -
            conditions[2] = m[1] && m[3] && !(m[2] || m[0]);    // +

            if (conditions[0])
                return '=';
            else if (conditions[1])
                return '-';
            else if (conditions[2])
                return '+';
            else return ' ';
        }

        public void SetState(char ch)
        {
            if (ch == '=')
            {
                m[0] = m[2] = true;
                m[1] = m[3] = false;
            }
            else if (ch == '-')
            {
                m[1] = true;
                m[0] = m[2] = m[3] = false;
            }
            else if (ch == '+')
            {
                m[1] = m[3] = true;
                m[0] = m[2] = false;
            }
            else
            {
                throw new ArgumentException("Only three types of operator are possible: +, -, =.");
            }
        }
    }
}
