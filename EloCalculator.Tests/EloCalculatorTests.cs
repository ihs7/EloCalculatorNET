using NUnit.Framework;
using System.Collections.Generic;

namespace EloCalculator.Tests
{
    [TestFixture]
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
        public void MultiMatch_100Contestants_DoesNotThrow()
        {
            var match = new EloMatch();

            Assert.DoesNotThrow(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    _ = match.AddPlayer(i + 1200, i == 0 ? true : false);
                }

                var result = match.Calculate();
            });
        }

        [Test]
        public void SimpleMatch_Player1Wins()
        {
            var player1 = new EloPlayer(1200);
            var player2 = new EloPlayer(1300);
            var teams = new List<EloTeam> { new EloTeam(player1, true), new EloTeam(player2, false) };
            var match = new EloMatch(teams);
            var result = match.Calculate();
            var player1EloDiff = result.GetRatingDifference(player1.Identifier);
            var player2EloDiff = result.GetRatingDifference(player2.Identifier);

            Assert.Greater(player1EloDiff, player2EloDiff);
        }

        [Test]
        public void SimpleMatch_SetKFactorAs0_Player1Wins_NoEloGained()
        {
            var player1 = new EloPlayer(1200);
            var player2 = new EloPlayer(1300);
            var players = new List<EloTeam> { new EloTeam(player1, true), new EloTeam(player2, false) };
            var match = new EloMatch(players);
            match.SetKFactor(0);
            var result = match.Calculate();
            var player1EloDiff = result.GetRatingDifference(player1.Identifier);
            var player2EloDiff = result.GetRatingDifference(player2.Identifier);

            Assert.AreEqual(0, player1EloDiff);
            Assert.AreEqual(0, player2EloDiff);
        }

        [Test]
        public void SimpleMatch_ByWon_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new EloMatch(new[] { new EloTeam(1200, true), new EloTeam(1500, false) }).Calculate());
        }

        [Test]
        public void SimpleMatch_ByPlace_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new EloMatch(new[] { new EloTeam(1200, 1), new EloTeam(1500, 2) }).Calculate());
        }

        [Test]
        public void EloExpected_HigherEloShouldBeExpectedToWin()
        {
            var expectedScore1 = new EloRating(1400).ExpectedScoreAgainst(1300);
            var expectedScore2 = new EloRating(1300).ExpectedScoreAgainst(1400);

            Assert.Greater(expectedScore1, expectedScore2);
        }

        [Test]
        public void EloExpected_EqualEloShouldHaveSameValue()
        {
            var expectedScore1 = new EloRating(1300).ExpectedScoreAgainst(1300);
            var expectedScore2 = new EloRating(1300).ExpectedScoreAgainst(1300);

            Assert.AreEqual(expectedScore1, expectedScore2);
        }
    }
}