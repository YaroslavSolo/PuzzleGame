using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PuzzleGame;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleInterpretation
{
    public class Digit
    {
        /// <summary>
        /// Number is represented like this
        /// 4  _
        /// 3 | | 5
        /// 2  -
        /// 1 | | 6
        /// 0  -
        /// </summary>
        private bool[] m = new bool[7];

        private Slot[] digitSlots = new Slot[7];

        public Digit(int digit)
        {
            SetDigitState(digit);
        }

        public void RenderSlots(Panel canvas, int x, int y)
        {
            RotateTransform rotate = new RotateTransform(90);

            for (int i = 0; i < digitSlots.Length; ++i)
                digitSlots[i] = new Slot();

            Canvas.SetLeft(digitSlots[0], 115 + x);
            Canvas.SetTop(digitSlots[0], 245 + y);
            digitSlots[0].RenderTransform = rotate;

            Canvas.SetLeft(digitSlots[1], x);
            Canvas.SetTop(digitSlots[1], 140 + y);

            Canvas.SetLeft(digitSlots[2], 115 + x);
            Canvas.SetTop(digitSlots[2], 125 + y);
            digitSlots[2].RenderTransform = rotate;

            Canvas.SetLeft(digitSlots[3], x);
            Canvas.SetTop(digitSlots[3], 20 + y);

            Canvas.SetLeft(digitSlots[4], 115 + x);
            Canvas.SetTop(digitSlots[4], 5 + y);
            digitSlots[4].RenderTransform = rotate;

            Canvas.SetLeft(digitSlots[5], 120 + x);
            Canvas.SetTop(digitSlots[5], 20 + y);

            Canvas.SetLeft(digitSlots[6], 120 + x);
            Canvas.SetTop(digitSlots[6], 140 + y);

            foreach (var slot in digitSlots)
            {
                canvas.Children.Add(slot);
            }
        }

        public void Render(Panel canvas, List<Match> allMatches, int x, int y)
        {
            Match[] matches = new Match[MatchesNeeded];
            for (int i = 0; i < MatchesNeeded; ++i)
                matches[i] = new Match();

            RotateTransform rotate = new RotateTransform(90);
            int cur = 0;

            foreach (var match in matches)
                canvas.Children.Add(match);
            allMatches.AddRange(matches);

            if (m[0])
            {
                Canvas.SetLeft(matches[cur], 60 + x);
                Canvas.SetTop(matches[cur], 200 + y);
                TransformGroup group = (TransformGroup)matches[cur].RenderTransform;
                group.Children.Add(rotate);
                ++cur;
            }
            if (m[1])
            {
                Canvas.SetLeft(matches[cur], x);
                Canvas.SetTop(matches[cur], 140 + y);
                ++cur;
            }
            if (m[2])
            {
                Canvas.SetLeft(matches[cur], 60 + x);
                Canvas.SetTop(matches[cur], 80 + y);
                TransformGroup group = (TransformGroup)matches[cur].RenderTransform;
                group.Children.Add(rotate);
                ++cur;
            }
            if (m[3])
            {
                Canvas.SetLeft(matches[cur], x);
                Canvas.SetTop(matches[cur], 20 + y);
                ++cur;
            }
            if (m[4])
            {
                Canvas.SetLeft(matches[cur], 60 + x);
                Canvas.SetTop(matches[cur], -40 + y);
                TransformGroup group = (TransformGroup)matches[cur].RenderTransform;
                group.Children.Add(rotate);
                ++cur;
            }
            if (m[5])
            {
                Canvas.SetLeft(matches[cur], 120 + x);
                Canvas.SetTop(matches[cur], 20 + y);
                ++cur;
            }
            if (m[6])
            {
                Canvas.SetLeft(matches[cur], 120 + x);
                Canvas.SetTop(matches[cur], 140 + y);
                ++cur;
            }
        }
        
        public int MatchesNeeded
        {
            get
            {
                return m.Count((e) => e);
            }
        }

        public bool this[int i]
        {
            get
            {
                if (i >= 0 && i < 8)
                    return m[i];
                else
                    throw new ArgumentOutOfRangeException("Position number should be between 0 and 7.");
            }
            set
            {
                if (i >= 0 && i < 8)
                    m[i] = value;
                else
                    throw new ArgumentOutOfRangeException("Position number should be between 0 and 7.");
            }
        }

        /// <summary>
        /// State to digit mapper
        /// </summary>
        /// <returns>Represented digit, -1 if the state doesn't correspond to any digit</returns>
        public int RepresentedDigit()
        {
            bool[] conditions = new bool[10];

            conditions[0] = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            conditions[1] = m[5] && m[6] && !(m[2] || m[0] || m[1] || m[3] || m[4]);
            conditions[2] = m[0] && m[1] && m[2] && m[4] && m[5] && !(m[2] || m[6]);
            conditions[3] = m[0] && m[2] && m[4] && m[5] && m[6] && !(m[1] || m[3]);
            conditions[4] = m[2] && m[3] && m[5] && m[6] && !(m[0] || m[1] || m[4]);
            conditions[5] = m[0] && m[2] && m[3] && m[4] && m[6] && !(m[1] || m[5]);
            conditions[6] = m[0] && m[1] && m[2] && m[3] && m[4] && m[6] && !(m[5]);
            conditions[7] = m[4] && m[5] && m[6] && !(m[0] || m[1] || m[2] || m[3]);
            conditions[8] = Array.TrueForAll(m, (e) => true);
            conditions[9] = m[0] && m[2] && m[3] && m[4] && m[5] && m[6] && !(m[1]);

            for (int i = 0; i < conditions.Length; ++i)
            {
                if (conditions[i])
                    return i;
            }

            return -1;
        }

        public void SetDigitState(int digit)
        {
            if (digit >= 0 && digit < 10)
            {
                Array.Fill(m, true);
                switch (digit)
                {
                    case 0:
                        m[2] = false;
                        break;
                    case 1:
                        m[2] = m[0] = m[1] = m[3] = m[4] = false;
                        break;
                    case 2:
                        m[2] = m[6] = false;
                        break;
                    case 3:
                        m[1] = m[3] = false;
                        break;
                    case 4:
                        m[0] = m[1] = m[4] = false;
                        break;
                    case 5:
                        m[1] = m[5] = false;
                        break;
                    case 6:
                        m[5] = false;
                        break;
                    case 7:
                        m[0] = m[1] = m[2] = m[3] = false;
                        break;
                    case 8:
                        // nothing is needed
                        break;
                    case 9:
                        m[1] = false;
                        break;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("State must be represented by a single digit.");
            }
        }
    }
}
