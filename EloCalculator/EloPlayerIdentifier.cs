using EloCalculator.Internal;
using System;
using System.Collections.Generic;

namespace EloCalculator
{
    public class EloPlayerIdentifier : ValueObject
    {
        public Guid Value { get; }

        public EloPlayerIdentifier(Guid value)
        {
            if (value == default)
                throw new ArgumentException(nameof(value) + " is default Guid.");

            Value = value;
        }

        public EloPlayerIdentifier()
            : this(Guid.NewGuid())
        { }

        public static implicit operator Guid(EloPlayerIdentifier x)
            => x.Value;

        public override string ToString()
            => Value.ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}