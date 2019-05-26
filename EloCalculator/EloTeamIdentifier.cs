using EloCalculator.Internal;
using System;
using System.Collections.Generic;

namespace EloCalculator
{
    public class EloTeamIdentifier : ValueObject
    {
        public Guid Value { get; }

        public EloTeamIdentifier(Guid value)
        {
            if (value == default)
                throw new ArgumentException(nameof(value) + " is default Guid.");

            Value = value;
        }

        public EloTeamIdentifier()
            : this(Guid.NewGuid())
        { }

        public static implicit operator Guid(EloTeamIdentifier x)
            => x.Value;

        public override string ToString()
            => Value.ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}