using System;

namespace EloCalculator
{
    public static class EloExpected
    {
        /// <summary>
        /// Calculates the expected score between two players.
        /// </summary>
        /// <param name="playerRating">Player to calculate expected score for.</param>
        /// <param name="versusRating">Opponent to calculate expected score against.</param>
        /// <returns>Returns a float value between 0 and 1 representing the likelyhood of player winning another.</returns>
        public static float ExpectedScoreAgainst(this EloPlayer player, EloPlayer opponent)
            => ExpectedScore(player.Rating, opponent.Rating);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team to calculate expected score for.</param>
        /// <param name="opponentTeam">Opponent team to calculate expected score against.</param>
        /// <returns></returns>
        public static float ExpectedScoreAgainst(this EloTeam team, EloTeam opponentTeam)
            => ExpectedScore(team.Rating, opponentTeam.Rating);

        /// <summary>
        /// Calculates the expected score between two ratings.
        /// </summary>
        /// <param name="playerRating">Rating to calculate for.</param>
        /// <param name="opponentRating">Rating to calculate against.</param>
        /// <returns>Returns a float value between 0 and 1 representing the likelyhood of player winning another.</returns>
        public static float ExpectedScoreAgainst(this EloRating playerRating, EloRating opponentRating)
            => ExpectedScore(playerRating, opponentRating);

        /// <summary>
        /// Calculates the expected score between two ratings.
        /// </summary>
        /// <param name="playerRating">Rating to calculate for.</param>
        /// <param name="opponentRating">Rating to calculate against.</param>
        /// <returns>Returns a float value between 0 and 1 representing the likelyhood of player winning another.</returns>
        public static float ExpectedScoreAgainst(this EloRating playerRating, int opponentRating)
            => ExpectedScore(playerRating, opponentRating);

        private static float ExpectedScore(int playerRating, int versusRating)
            => 1 / (1.0f + (float)Math.Pow(10.0f, (versusRating - playerRating) / 400.0f));
    }
}