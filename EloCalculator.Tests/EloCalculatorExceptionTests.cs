using NUnit.Framework;
using System;

namespace EloCalculator.Tests
{
    [TestFixture]
    public class EloCalculatorExceptionTests
    {
        [Test]
        public void AddPlayerToTwoTeams_ThrowsArgumentException()
        {
            var player = new EloPlayer(1350);
            Assert.Throws<ArgumentException>(() => new EloMatch(new[] { new EloTeam(player, true), new EloTeam(player, false) }));
        }

        [Test]
        public void AddPlayerWithSameIdentifierToTwoTeams_ThrowsArgumentException()
        {
            var playerIdentifier = new EloPlayerIdentifier();
            var player1 = new EloPlayer(playerIdentifier, 1350);
            var player2 = new EloPlayer(playerIdentifier, 1330);
            Assert.Throws<ArgumentException>(() => new EloMatch(new[] { new EloTeam(player1, true), new EloTeam(player2, false) }));
        }

        [Test]
        public void AddIdenticalPlayerToSameTeamTwice_ThrowsInvalidOperationException()
        {
            var playerIdentifier = new EloPlayerIdentifier();
            var player1 = new EloPlayer(playerIdentifier, 1350);
            var player2 = new EloPlayer(playerIdentifier, 1330);
            var team = new EloTeam(player1, true);
            Assert.Throws<InvalidOperationException>(() => team.AddPlayer(player2));
        }

        [Test]
        public void AddSamePlayerToSameTeamTwice_ThrowsInvalidOperationException()
        {
            var playerIdentifier = new EloPlayerIdentifier();
            var player = new EloPlayer(playerIdentifier, 1350);
            var team = new EloTeam(player, true);
            Assert.Throws<InvalidOperationException>(() => team.AddPlayer(player));
        }

        [Test]
        public void AddTwoTeamsWithSameIdentifierToMatch_ThrowsArgumentException()
        {
            var teamIdentifier = new EloTeamIdentifier();
            var team1 = new EloTeam(teamIdentifier, new EloPlayer(1200), true);
            var team2 = new EloTeam(teamIdentifier, new EloPlayer(1350), false);
            Assert.Throws<ArgumentException>(() => new EloMatch(new[] { team1, team2 }));
        }

        [Test]
        public void CreateMatchDynamically_AddSameTeam_ThrowsArgumentException()
        {
            var match = new EloMatch();
            var teamIdentifier = match.AddTeam(new EloTeam(true));
            Assert.Throws<ArgumentException>(() => match.AddTeam(new EloTeam(teamIdentifier, false)));
        }

        [Test]
        public void CreateMatchDynamically_AddSamePlayer_ThrowsArgumentException()
        {
            var match = new EloMatch();
            var teamIdentifier = match.AddTeam(new EloTeam(true));
            var player = new EloPlayer(1200);
            match.AddPlayerToTeam(teamIdentifier, player);
            Assert.Throws<ArgumentException>(() => match.AddPlayerToTeam(teamIdentifier, player));
        }

        [Test]
        public void AddPlayerToNonExistingTeam_ThrowsInvalidOperationException()
        {
            var match = new EloMatch();
            Assert.Throws<InvalidOperationException>(() => match.AddPlayerToTeam(new EloTeamIdentifier(), 1200));
        }

        [Test]
        public void GetResultForPlayerThatIsNotDefined_ThrowsInvalidOperationException()
        {
            var result = new EloMatch(new[] { new EloTeam(1200, 1), new EloTeam(1300, 2) }).Calculate();
            Assert.Throws<InvalidOperationException>(() => result.GetResult(new EloPlayerIdentifier()));
        }
    }
}
