using NUnit.Framework;
using System;

namespace EloCalculator.Tests
{
    [TestFixture]
    public class EloCalculatorEqualityTests
    {
        [Test]
        public void PlayerWithSameIdentifierAndRating_AssertEquality()
        {
            var identifier = new EloPlayerIdentifier(Guid.NewGuid());
            var rating = new EloRating(1200);
            var player1 = new EloPlayer(identifier, rating);
            var player2 = new EloPlayer(identifier, rating);

            Assert.AreEqual(player1, player2);
            Assert.IsTrue(player1 == player2);
            Assert.IsTrue(player1.Equals(player2));
        }

        [Test]
        public void TwoTeamsWithSamePlayers_AssertEquality()
        {
            var playerIdentifier = new EloPlayerIdentifier();
            var rating = new EloRating(1200);
            var player1 = new EloPlayer(playerIdentifier, rating);
            var player2 = new EloPlayer(playerIdentifier, rating);
            var teamIdentifier = new EloTeamIdentifier();
            var team1 = new EloTeam(teamIdentifier, player1, true);
            var team2 = new EloTeam(teamIdentifier, player2, false);

            Assert.AreEqual(team1, team2);
            Assert.IsTrue(team1 == team2);
            Assert.IsTrue(team1.Equals(team2));
        }
    }
}
