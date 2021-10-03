using System;

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

        public Digit(int state)
        {
            SetDigitState(state);
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
                Array.ForEach(m, (e) => e = true);
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
