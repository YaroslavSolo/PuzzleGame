using PuzzleInterpretation;
using System.Collections.Generic;


namespace PuzzleGame
{
    public class TestingParameters
    {
        public List<MatchesPuzzle> Puzzles = new List<MatchesPuzzle>();

        public int AttemptDuration { get; set; }

        public int NumAttempts { get; set; }

        public string Description { get; set; }

        public bool IsFeedbackNeeded { get; set; }

        public bool AreSlotsVisible { get; set; }
    }
}
