using System.Collections.Generic;
using System.Windows.Controls;
using PuzzleGame;

namespace PuzzleInterpretation
{
    public class MatchesPuzzle
    {
        private Digit[] digits = new Digit[3];

        private Operator firstOp;

        private Operator secondOp;

        private string answer;

        private const double ATTACH_DIST = 18;

        public List<Match> matches = new List<Match>();

        public int AttemptsLeft { get; set; }

        public int TimeLeft { get; set; }

        public int MatchesToMove { get; private set; }

        public int MatchesToMoveLeft { get; set; }

        public bool IsSolved
        {
            get
            {
                firstOp.UpdateState();
                secondOp.UpdateState();

                return (digits[0].RepresentedDigit() + '0') == answer[0] &&
                       (digits[1].RepresentedDigit() + '0') == answer[2] &&
                       (digits[2].RepresentedDigit() + '0') == answer[4] &&
                       firstOp.RepresentedOperator() == answer[1] &&
                       secondOp.RepresentedOperator() == answer[3];
            }
        }

        public MatchesPuzzle(KeyValuePair<KeyValuePair<string, string>, int> rawPuzzle)
        {
            firstOp = new Operator(this, 2);
            secondOp = new Operator(this, 4);

            string task = rawPuzzle.Key.Key;
            answer = rawPuzzle.Key.Value;

            MatchesToMove = rawPuzzle.Value;
            MatchesToMoveLeft = rawPuzzle.Value;
            digits[0] = new Digit(task[0] - '0', this, 1);
            firstOp.SetState(task[1]);
            digits[1] = new Digit(task[2] - '0', this, 3);
            secondOp.SetState(task[3]);
            digits[2] = new Digit(task[4] - '0', this, 5);
        }

        public void RenderSlots(Panel canvas, int x, int y, bool areSlotsVisible)
        {
            digits[0].RenderSlots(canvas, x, y, areSlotsVisible);
            digits[1].RenderSlots(canvas, 300 + x, y, areSlotsVisible);
            digits[2].RenderSlots(canvas, 600 + x, y, areSlotsVisible);

            firstOp.RenderSlots(canvas, 265 + x, 80 + y);
            secondOp.RenderSlots(canvas, 565 + x, 80 + y);
        }

        public void Render(Panel canvas, int x, int y)
        {
            matches.Clear();
            digits[0].Render(canvas, matches, x, y);
            digits[1].Render(canvas, matches, 300 + x, y);
            digits[2].Render(canvas, matches, 600 + x, y);

            firstOp.Render(canvas, matches, 265 + x, 80 + y);
            secondOp.Render(canvas, matches, 565 + x, 80 + y);
        }

        public void AttachToSlot(Match match)
        {
            if (!match.WasMoved)
            {
                --MatchesToMoveLeft;
                match.WasMoved = true;
            }

            digits[0].AttachIfPossible(match, ATTACH_DIST);
            digits[1].AttachIfPossible(match, ATTACH_DIST);
            digits[2].AttachIfPossible(match, ATTACH_DIST);
            firstOp.AttachIfPossible(match, ATTACH_DIST);
            secondOp.AttachIfPossible(match, ATTACH_DIST);
        }
    }
}
