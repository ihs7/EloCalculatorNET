using EloCalculator;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class EloCalculatorTests
    {
        [Test]
        public void Player1WinsPlayer2_AssertPlayer1GainsElo()
        {
            var match = new EloMatch();
            var player1Identifier = match.AddPlayer(1200, 1);
            var player2Identifier = match.AddPlayer(1300, 2);
            var result = match.Calculate();
            var player1EloDiff = result.GetRatingDifference(player1Identifier);
            var player2EloDiff = result.GetRatingDifference(player2Identifier);

            Assert.Greater(player1EloDiff, player2EloDiff);
        }

        [Test]
        public void Player1WinsMultiMatch_AssertPlayer1GainsMostElo()
        {
            var match = new EloMatch();
            var player1Identifier = match.AddPlayer(1200, 1);
            var player2Identifier = match.AddPlayer(1300, 2);
            var player3Identifier = match.AddPlayer(1250, 3);
            var result = match.Calculate();
            var player1EloDiff = result.GetRatingDifference(player1Identifier);
            var player2EloDiff = result.GetRatingDifference(player2Identifier);
            var player3EloDiff = result.GetRatingDifference(player3Identifier);

            Assert.Greater(player1EloDiff, player2EloDiff);
            Assert.Greater(player1EloDiff, player3EloDiff);
            Assert.Greater(player2EloDiff, player3EloDiff);
        }

        [Test]
        public void MultiMatch_1000Contestants_DoesNotThrow()
        {
            var match = new EloMatch();

            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    match.AddPlayer(i + 1200, i);
                }

                var result = match.Calculate();
                int x = 0;
            });
        }

        [Test]
        public void SimpleMatch_Player1Wins()
        {
            var player1 = new EloPlayer(1200, true);
            var player2 = new EloPlayer(1300, false);
            var players = new List<EloPlayer> { player1, player2 };
            var match = new EloMatch(players);
            var result = match.Calculate();
            var player1EloDiff = result.GetRatingDifference(player1.Identifier);
            var player2EloDiff = result.GetRatingDifference(player2.Identifier);

            Assert.Greater(player1EloDiff, player2EloDiff);
        }

        [Test]
        public void SimpleMatch_SetKFactorAs0_Player1Wins_NoEloGained()
        {
            var player1 = new EloPlayer(1200, true);
            var player2 = new EloPlayer(1300, false);
            var players = new List<EloPlayer> { player1, player2 };
            var match = new EloMatch(players);
            match.SetKFactor(0);
            var result = match.Calculate();
            var player1EloDiff = result.GetRatingDifference(player1.Identifier);
            var player2EloDiff = result.GetRatingDifference(player2.Identifier);

            Assert.AreEqual(0, player1EloDiff);
            Assert.AreEqual(0, player2EloDiff);
        }

        [Test]
        public void SimpleMatch_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new EloMatch(new EloPlayer(1200, true), new EloPlayer(1500, false)).Calculate());
        }
    }
}