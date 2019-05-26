using System;

namespace EloCalculator
{
    public class EloIndividualResult
    {
        public EloPlayerIdentifier Identifier { get; }
        public int RatingBefore { get; }
        public int RatingAfter { get; }
        public int RatingDifference => RatingAfter - RatingBefore;

        public EloIndividualResult(EloPlayerIdentifier identifier, int ratingBefore, int ratingAfter)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            RatingBefore = ratingBefore;
            RatingAfter = ratingAfter;
        }
    }
}