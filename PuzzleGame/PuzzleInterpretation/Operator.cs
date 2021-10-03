using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleInterpretation
{
    public class Operator
    {
        /// <summary>
        /// It's complicated :/
        /// </summary>
        private bool[] m = new bool[4];

        public char RepresentedOperator()
        {
            bool[] conditions = new bool[3];

            conditions[0] = m[0] && m[2] && !(m[1] || m[3]);    // =
            conditions[1] = m[1] && !(m[2] || m[3] || m[0]);    // -
            conditions[2] = m[0] && m[3] && !(m[2] || m[1]);    // +

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
        }
    }
}
