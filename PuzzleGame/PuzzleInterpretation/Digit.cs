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

        //TODO
        /// <summary>
        /// State to digit mapper
        /// </summary>
        /// <returns></returns>
        public int RepresentedNumber()
        {
            bool zeroCondition  = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            bool oneCondition   = m[5] && m[6] && !(m[2] || m[0] || m[1] || m[3] || m[4]);
            bool twoCondition   = m[0] && m[1] && m[2] && m[4] && m[5] && !(m[2] || m[6]);
            bool threeCondition = m[0] && m[2] && m[4] && m[5] && m[6] && !(m[1] || m[3]);
            bool fourCondition  = m[2] && m[3] && m[5] && m[6] && !(m[0] && m[1] || m[4]);
            bool fiveCondition  = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            bool sixCondition   = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            bool sevenCondition = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            bool eightCondition = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);
            bool nineCondition  = m[0] && m[1] && m[3] && m[4] && m[5] && m[6] && !(m[2]);

            return 0;
        }
    }
}
