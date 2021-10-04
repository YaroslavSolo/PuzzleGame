using System;
using System.Windows.Controls;
using System.Windows.Media;
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

        public Slot[] digitSlots;

        private Match[] matches;
         
        private MatchesPuzzle puzzle;

        private int symbolNum;

        public Digit(int digit, MatchesPuzzle puzzle, int symbolNum)
        {
            this.puzzle = puzzle;
            this.symbolNum = symbolNum;
            SetDigitState(digit);
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
            foreach (Slot slot in digitSlots)
            {
                if (!slot.Occupied && m.Horizontal == slot.Horizontal && m.Dist(slot) <= 100)
                    System.Diagnostics.Trace.WriteLine(m.Dist(slot));
                if (!slot.Occupied && m.Horizontal == slot.Horizontal && m.Dist(slot) <= attachDist)
                {                
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

        public void RenderSlots(Panel canvas, int x, int y, bool areSlotsVisible)
        {
            digitSlots = new Slot[7];
            for (int i = 0; i < digitSlots.Length; ++i)
            {
                digitSlots[i] = new Slot();
                if (!areSlotsVisible)
                    digitSlots[i].Visibility = System.Windows.Visibility.Hidden;
            }       

            PlaceSlot(digitSlots[0], 115 + x, 245 + y, true);
            PlaceSlot(digitSlots[1], x, 140 + y);
            PlaceSlot(digitSlots[2], 115 + x, 125 + y, true);
            PlaceSlot(digitSlots[3], x, 20 + y);
            PlaceSlot(digitSlots[4], 115 + x, 5 + y, true);
            PlaceSlot(digitSlots[5], 120 + x, 20 + y);
            PlaceSlot(digitSlots[6], 120 + x, 140 + y);

            foreach (var slot in digitSlots)
                canvas.Children.Add(slot);
        }

        public void Render(Panel canvas, List<Match> allMatches, int x, int y)
        {
            matches = new Match[MatchesNeeded];
            for (int i = 0; i < MatchesNeeded; ++i)
                matches[i] = new Match(puzzle, symbolNum);

            int cur = 0;

            foreach (var match in matches)
                canvas.Children.Add(match);
               
            allMatches.AddRange(matches);

            if (m[0])
            {
                PlaceMatch(matches[cur], 60 + x, 200 + y, true);
                matches[cur].Slot = digitSlots[0];
                matches[cur].MatchNum = 0;
                digitSlots[0].ContentMatch = matches[cur++];
            }
            if (m[1])
            {
                PlaceMatch(matches[cur], x, 140 + y);
                matches[cur].Slot = digitSlots[1];
                matches[cur].MatchNum = 1;
                digitSlots[1].ContentMatch = matches[cur++];
            }
            if (m[2])
            {
                PlaceMatch(matches[cur], 60 + x, 80 + y, true);
                matches[cur].Slot = digitSlots[2];
                matches[cur].MatchNum = 2;
                digitSlots[2].ContentMatch = matches[cur++];
            }
            if (m[3])
            {
                PlaceMatch(matches[cur], x, 20 + y);
                matches[cur].Slot = digitSlots[3];
                matches[cur].MatchNum = 3;
                digitSlots[3].ContentMatch = matches[cur++];
            }
            if (m[4])
            {
                PlaceMatch(matches[cur], 60 + x, -40 + y, true);
                matches[cur].Slot = digitSlots[4];
                matches[cur].MatchNum = 4;
                digitSlots[4].ContentMatch = matches[cur++];
            }
            if (m[5])
            {
                PlaceMatch(matches[cur], 120 + x, 20 + y);
                matches[cur].Slot = digitSlots[5];
                matches[cur].MatchNum = 5;
                digitSlots[5].ContentMatch = matches[cur++];
            }
            if (m[6])
            {
                PlaceMatch(matches[cur], 120 + x, 140 + y);
                matches[cur].Slot = digitSlots[6];
                matches[cur].MatchNum = 6;
                digitSlots[6].ContentMatch = matches[cur++];
            }
        }
        
        public int MatchesNeeded
        {
            get
            {
                return m.Count((e) => e);
            }
        }

        /// <summary>
        /// State to digit mapper
        /// </summary>
        /// <returns>Represented digit, -1 if the state doesn't correspond to any digit</returns>
        public int RepresentedDigit()
        {
            for (int i = 0; i < 7; ++i)
                m[i] = digitSlots[i].Occupied;

            bool[] conditions = new bool[10];

            conditions[0] = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            conditions[1] = m[5] && m[6] && !(m[2] || m[0] || m[1] || m[3] || m[4]);
            conditions[2] = m[0] && m[1] && m[2] && m[4] && m[5] && !(m[2] || m[6]);
            conditions[3] = m[0] && m[2] && m[4] && m[5] && m[6] && !(m[1] || m[3]);
            conditions[4] = m[2] && m[3] && m[5] && m[6] && !(m[0] || m[1] || m[4]);
            conditions[5] = m[0] && m[2] && m[3] && m[4] && m[6] && !(m[1] || m[5]);
            conditions[6] = m[0] && m[1] && m[2] && m[3] && m[4] && m[6] && !(m[5]);
            conditions[7] = m[4] && m[5] && m[6] && !(m[0] || m[1] || m[2] || m[3]);
            conditions[8] = m[0] && m[1] && m[2] && m[3] && m[4] && m[5] && m[6];
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
