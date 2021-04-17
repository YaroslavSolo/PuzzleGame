using System;
using System.Collections.Generic;
using System.Text;


namespace PuzzleInterpretation
{
    class MatchesPuzzle
    {
        private Digit[] digits = new Digit[3];

        private Operator firstOp = new Operator();

        private Operator secondOp = new Operator();

        public MatchesPuzzle()
        {

        }

        public bool IsSolved
        {
            get
            {
                return false;
            }
        }
    }
}
