using System;

namespace Lexicon.Base.CQRS
{
    /// <summary>
    /// Yes I will use CQRS and Event Sourcing.
    /// </summary>
    public abstract class Aggregate
    {
        public Guid AggregateId { get; internal set; }
        public int AggregateVersion { get; internal set; }
    }
}
