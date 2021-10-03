using System;
using System.Collections.Generic;
using System.Text;


namespace PuzzleInterpretation
{
    public class MatchesPuzzle
    {
        private Digit[] digits = new Digit[3];

        private Operator firstOp = new Operator();

        private Operator secondOp = new Operator();

        private string answer;

        public int AttemptsLeft { get; set; }

        public int MatchesMoved { get; set; }

        public int MaxMatchesMoved { get; set; }

        public void Render()
        {

        }

        public MatchesPuzzle(KeyValuePair<KeyValuePair<string, string>, int> rawPuzzle)
        {
            string task = rawPuzzle.Key.Key;
            answer = rawPuzzle.Key.Value;

            MaxMatchesMoved = rawPuzzle.Value;
            digits[0].SetDigitState(task[0]);
            firstOp.SetState(task[1]);
            digits[1].SetDigitState(task[2]);
            secondOp.SetState(task[3]);
            digits[2].SetDigitState(task[4]);
        }

        public bool IsSolved
        {
            get
            {
                return digits[0].RepresentedDigit() == answer[0] &&
                    digits[1].RepresentedDigit() == answer[2] &&
                    digits[2].RepresentedDigit() == answer[4] &&
                    firstOp.RepresentedOperator() == answer[1] &&
                    secondOp.RepresentedOperator() == answer[3];
            }
        }
    }
}
