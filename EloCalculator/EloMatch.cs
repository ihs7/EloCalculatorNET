using System;
using System.Collections.Generic;
using System.Linq;

namespace EloCalculator
{
    public class EloMatch
    {
        private readonly HashSet<EloTeam> _teams = new HashSet<EloTeam>();
        private int _kFactor = 15;

        public EloMatch()
        { }

        public EloMatch(IEnumerable<EloTeam> teams)
        {
            if (teams == null)
                throw new ArgumentNullException(nameof(teams));

            if (teams.GetPlayerIdentifiers().Distinct().Count() != teams.GetPlayerIdentifiers().Count())
                throw new ArgumentException($"Multiple players with same identifier.");

            if (teams.GetTeamIdentifiers().Distinct().Count() != teams.Count())
                throw new ArgumentException($"Multiple teams with same identifier");

            foreach (var team in teams)
            {
                if (!_teams.Add(team))
                    throw new ArgumentException($"Identical team with identifier {team.Identifier} already exists");
            }
        }

        public EloTeamIdentifier AddTeam(EloTeam team)
        {
            if (team == null)
                throw new ArgumentNullException(nameof(team));

            if (!_teams.Add(team))
                throw new ArgumentException($"Team with identifier {team.Identifier} already exists in this match.");

            return team.Identifier;
        }

        public void AddPlayerToTeam(EloTeamIdentifier teamIdentifier, EloPlayer playerToAdd)
        {
            if (playerToAdd == null)
                throw new ArgumentNullException(nameof(playerToAdd));

            if (_teams.Any(x => x.Members.Select(s => s.Identifier).Contains(playerToAdd.Identifier)))
                throw new ArgumentException($"Player with identifier {playerToAdd.Identifier} already exists in a team in this match.");

            var team = _teams.FirstOrDefault(x => x.Identifier == teamIdentifier);

            if (team == null)
                throw new InvalidOperationException($"No team with identifier {teamIdentifier} found in match.");

            team.AddPlayer(playerToAdd);
        }

        public void AddPlayerToTeam(EloTeamIdentifier teamIdentifier, int rating)
            => AddPlayerToTeam(teamIdentifier, new EloPlayer(rating));

        public EloPlayerIdentifier AddPlayer(int rating, int place)
        {
            var player = new EloPlayer(rating);
            _ = AddTeam(new EloTeam(player, place));
            return player.Identifier;
        }

        public EloPlayerIdentifier AddPlayer(int rating, bool won)
        {
            var player = new EloPlayer(rating);
            _ = AddTeam(new EloTeam(player, won));
            return player.Identifier;
        }

        public void SetKFactor(int kFactor)
            => _kFactor = kFactor;

        public EloResult Calculate()
        {
            var results = new List<EloIndividualResult>();

            foreach (var team in _teams)
            {
                double eloDiff = 0;

                foreach (var opponent in _teams.Where(x => x.Identifier != team.Identifier))
                {
                    float s;

                    if (team.Place < opponent.Place)
                        s = 1.0F;
                    else if (team.Place == opponent.Place)
                        s = 0.5F;
                    else
                        s = 0.0F;

                    eloDiff += _kFactor * (s - team.ExpectedScoreAgainst(opponent));
                }

                foreach (var member in team.Members)
                {
                    results.Add(new EloIndividualResult(member.Identifier,
                                                        team.Identifier,
                                                        member.Rating,
                                                        new EloRating(member.Rating + (int)eloDiff)));
                }


            }

            return new EloResult(results);
        }
    }
}