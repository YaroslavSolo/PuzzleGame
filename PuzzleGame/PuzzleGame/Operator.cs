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
        /// 
        /// </summary>
        private bool[] m = new bool[4];

        public readonly Slot[] opSlots = new Slot[4];

        private MatchesPuzzle puzzle;

        private int symbolNum;

        public Operator(MatchesPuzzle puzzle, int symbolNum)
        {
            this.puzzle = puzzle;
            this.symbolNum = symbolNum;
        }

        public void PlaceSlot(Slot slot, int x, int y, bool horizontal = false)
        {
            Canvas.SetLeft(slot, x);
            Canvas.SetTop(slot, y);
            if (horizontal)
                slot.SetCoordinates(x - 50, y - 50);
            else
                slot.SetCoordinates(x, y);
            if (horizontal)
            {
                RotateTransform rotate = new RotateTransform(90);
                slot.RenderTransform = rotate;
                slot.Horizontal = true;
            }
        }

        public void PlaceMatch(Match match, int x, int y, bool horizontal = false)
        {
            Canvas.SetLeft(match, x);
            Canvas.SetTop(match, y);
            match.SetCoordinates(x, y);
            if (horizontal)
            {
                RotateTransform rotate = new RotateTransform(90);
                TransformGroup group = (TransformGroup)match.RenderTransform;
                group.Children.Add(rotate);
                match.Horizontal = true;
            }
        }

        public bool AttachIfPossible(Match m, double attachDist)
        {
            foreach (Slot slot in opSlots)
            {
                if (!slot.Occupied && m.Horizontal == slot.Horizontal && m.Dist(slot) <= attachDist)
                {
                    System.Diagnostics.Trace.WriteLine(m.Dist(slot));
                    slot.ContentMatch = m;
                    m.Slot.ContentMatch = null;
                    m.Slot = slot;
                    m.GetOffset().X += slot.X - m.RealX;
                    m.GetOffset().Y += slot.Y - m.RealY;
                    return true;
                }
            }

            return false;
        }

        public void RenderSlots(Panel canvas, int x, int y)
        {
            for (int i = 0; i < opSlots.Length; ++i)
            {
                opSlots[i] = new Slot();
                //opSlots[i].Visibility = System.Windows.Visibility.Hidden;  // uncomment on release
            }
                
            PlaceSlot(opSlots[0], -55 + x, y);
            PlaceSlot(opSlots[1], x, 65 + y, true);
            PlaceSlot(opSlots[2], x, 45 + y, true);
            PlaceSlot(opSlots[3], x, 25 + y, true);

            foreach (var slot in opSlots)
            {
                canvas.Children.Add(slot);
            }
        }

        public void Render(Panel canvas, List<Match> allMatches, int x, int y)
        {
            Match match1 = new Match(puzzle, symbolNum, 1);
            Match match2 = new Match(puzzle, symbolNum, 2);

            switch (RepresentedOperator())
            {
                case '=':
                    PlaceMatch(match2, -55 + x, -20 + y, true);
                    opSlots[3].ContentMatch = match2;
                    match2.Slot = opSlots[3];

                    PlaceMatch(match1, -55 + x, 20 + y, true);
                    opSlots[1].ContentMatch = match1;
                    match1.Slot = opSlots[1];

                    canvas.Children.Add(match2);
                    break;
                case '-':
                    PlaceMatch(match1, -55 + x, y, true);
                    opSlots[2].ContentMatch = match1;
                    match1.Slot = opSlots[2];
                    break;
                case '+':
                    PlaceMatch(match1, -55 + x, y, true);
                    opSlots[2].ContentMatch = match1;
                    match1.Slot = opSlots[2];

                    PlaceMatch(match2, x, y);
                    opSlots[0].ContentMatch = match2;
                    match2.Slot = opSlots[0];

                    canvas.Children.Add(match2);
                    break;
            }

            canvas.Children.Add(match1);
        }

        public void UpdateState()
        {
            for (int i = 0; i < 4; ++i)
            {
                m[i] = opSlots[i].Occupied;
            }
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
