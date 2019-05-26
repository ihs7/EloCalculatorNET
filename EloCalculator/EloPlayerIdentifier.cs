using System;

namespace EloCalculator
{
    public class EloPlayerIdentifier
    {
        public Guid Value { get; }

        public EloPlayerIdentifier(Guid value)
        {
            if (value == default)
                throw new ArgumentException(nameof(value) + " is default Guid.");

            Value = value;
        }

        public static implicit operator Guid(EloPlayerIdentifier x)
            => x.Value;

        public override string ToString()
            => Value.ToString();
    }
}