using EloCalculator.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloCalculator
{
    public class EloTeam : ValueObject
    {
        public HashSet<EloPlayer> Members { get; }
        public EloTeamIdentifier Identifier { get; }
        public EloRating Rating => new EloRating((int)Members.Average(x => x.Rating));
        public int Place { get; }

        public EloTeam(EloTeamIdentifier identifier, IEnumerable<EloPlayer> players, int place)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            Identifier = identifier;
            Place = place;
            Members = new HashSet<EloPlayer>();

            foreach (var player in players)
                AddPlayer(player);
        }

        public EloTeam(EloTeamIdentifier identifier, EloPlayer player, int place)
            : this(identifier, new HashSet<EloPlayer> { player }, place)
        { }

        public EloTeam(EloTeamIdentifier identifier, EloPlayer player, bool won)
            : this(identifier, new HashSet<EloPlayer> { player }, won ? 0 : 1)
        { }

        public EloTeam(IEnumerable<EloPlayer> members, int place)
            : this(new EloTeamIdentifier(Guid.NewGuid()), members, place)
        { }

        public EloTeam(EloPlayer player, int place)
            : this(new List<EloPlayer> { player }, place)
        { }

        public EloTeam(EloPlayer player, bool won)
            : this(player, won ? 0 : 1)
        { }

        public EloTeam(int rating, int place)
            : this(new EloPlayer(rating), place)
        { }

        public EloTeam(int rating, bool won)
            : this(new EloPlayer(rating), won)
        { }

        public EloTeam(int place)
            : this(new EloTeamIdentifier(), new HashSet<EloPlayer>(), place)
        { }

        public EloTeam(EloTeamIdentifier identifier, int place)
            : this(identifier, new HashSet<EloPlayer>(), place)
        { }

        public EloTeam(bool won)
            : this(new EloTeamIdentifier(), new HashSet<EloPlayer>(), won ? 0 : 1)
        { }

        public EloTeam(EloTeamIdentifier identifier, bool won)
            : this(identifier, new HashSet<EloPlayer>(), won ? 0 : 1)
        { }

        public void AddPlayer(EloPlayer playerToAdd)
        {
            if (!Members.Add(playerToAdd))
                throw new InvalidOperationException($"Player with identifier {playerToAdd.Identifier} already exists in this team."); ;
        }

        public IEnumerable<EloPlayerIdentifier> GetPlayerIdentifiers()
            => Members.Select(x => x.Identifier);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identifier;

            foreach (var member in Members)
            {
                yield return member;
            }
        }
    }
}