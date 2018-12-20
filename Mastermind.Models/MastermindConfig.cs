namespace Mastermind.Models
{
    public class MastermindConfig
    {
        public int NumberOrGuesses { get; set; }
        public int InclusiveLowerLimit { get; set; }
        public int ExclusiveUpperLimit { get; set; }
        public int SequenceLength { get; set; }
    }
}
