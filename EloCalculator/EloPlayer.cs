using EloCalculator.Internal;
using System;
using System.Collections.Generic;

namespace EloCalculator
{
    public class EloPlayer : ValueObject
    {
        public EloPlayerIdentifier Identifier { get; }
        public EloRating Rating { get; private set; }

        public EloPlayer(EloPlayerIdentifier identifier, EloRating rating)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        }

        public EloPlayer(EloPlayerIdentifier identifier, int rating)
            : this(identifier, new EloRating(rating))
        { }

        public EloPlayer(int rating)
            : this(new EloPlayerIdentifier(), new EloRating(rating))
        { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identifier;
        }
    }
}