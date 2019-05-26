using System;

namespace EloCalculator
{
    public class EloIndividualResult
    {
        public EloPlayerIdentifier PlayerIdentifier { get; }
        public EloTeamIdentifier TeamIdentifier { get; }
        public EloRating RatingBefore { get; }
        public EloRating RatingAfter { get; }
        public int RatingDifference => RatingAfter - RatingBefore;

        public EloIndividualResult(EloPlayerIdentifier playerIdentifier, EloTeamIdentifier teamIdentifier, EloRating ratingBefore, EloRating ratingAfter)
        {
            PlayerIdentifier = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));
            TeamIdentifier = teamIdentifier ?? throw new ArgumentNullException(nameof(teamIdentifier));
            RatingBefore = ratingBefore;
            RatingAfter = ratingAfter;
        }

        public override string ToString()
            => $"Player identifier: {PlayerIdentifier}, Rating before: {RatingBefore}, Rating after: {RatingAfter}, Rating difference {RatingDifference}";
    }
}