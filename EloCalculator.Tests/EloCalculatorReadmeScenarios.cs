﻿using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EloCalculator.Tests
{
    [TestFixture]
    public class EloCalculatorReadmeScenarios
    {
        [Test]
        public void TwoPlayerScenario()
        {
            var match = new EloMatch();
            var player1Identifier = match.AddPlayer(1200, true);
            var player2Identifier = match.AddPlayer(1320, false);
            var result = match.Calculate();

            var player1EloDifference = result.GetRatingDifference(player1Identifier);
            var player2EloDifference = result.GetRatingDifference(player2Identifier);

            Assert.Greater(player1EloDifference, player2EloDifference);
        }

        [Test]
        public void ThreePlayerScenario()
        {
            var match = new EloMatch();
            var player1Identifier = match.AddPlayer(1280, 1);
            var player2Identifier = match.AddPlayer(1300, 2);
            var player3Identifier = match.AddPlayer(1220, 3);
            var result = match.Calculate();

            var player1NewRating = result.GetRatingAfter(player1Identifier);
            var player2NewRating = result.GetRatingAfter(player2Identifier);
            var player3NewRating = result.GetRatingAfter(player3Identifier);

            Assert.Greater(player1NewRating, 1280);
            Assert.Less(player2NewRating, 1300);
            Assert.Less(player3NewRating, 1220);
        }

        [Test]
        public void TenPlayersScenario()
        {
            var teams = new List<EloTeam>();

            for (var i = 0; i < 10; i++)
                teams.Add(new EloTeam(1200, i == 0));

            var result = new EloMatch(teams).Calculate();

            foreach (var ir in result.GetResults())
                Console.WriteLine($"{ir.PlayerIdentifier}, rating before: {ir.RatingBefore}, rating after: {ir.RatingAfter}");

            Assert.Pass();
        }

        [Test]
        public void TwoversustwoScenario()
        {
            var match = new EloMatch();
            var team1 = match.AddTeam(new EloTeam(true));
            match.AddPlayerToTeam(team1, 1230);
            match.AddPlayerToTeam(team1, 1260);
            var team2 = match.AddTeam(new EloTeam(false));
            match.AddPlayerToTeam(team2, 1120);
            match.AddPlayerToTeam(team2, 1410);
            var result = match.Calculate();

            foreach (var ir in result.GetResults(team1))
                Console.WriteLine($"Team 1 - Player: {ir.PlayerIdentifier}, Rating difference: {ir.RatingDifference}");

            foreach (var ir in result.GetResults(team2))
                Console.WriteLine($"Team 2 - Player: {ir.PlayerIdentifier}, Rating difference: {ir.RatingDifference}");

            Assert.Pass();
        }

        [Test]
        public void GetExpectedScoreScenario()
        {
            var expectedScore = new EloRating(1460).ExpectedScoreAgainst(1130);

            Assert.Greater(expectedScore, 0.5);
        }
    }
}
