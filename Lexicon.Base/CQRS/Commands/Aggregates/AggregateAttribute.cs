using System;

namespace Lexicon.Base.CQRS
{
    public class AggregateAttribute : Attribute
    {
        public bool IsSingleton { get; internal set; }
        public bool AllowNonSingletonEmptyGuidId { get; internal set; }
        public AggregateCachingOptions CachingOptions { get; internal set; }

        public AggregateAttribute(bool isSingleton = false, bool allowNonSingletonEmptyGuidId = false,
            AggregateCachingOptions cachingOptions = AggregateCachingOptions.EnableForSingleton)
        {
            IsSingleton = isSingleton;
            AllowNonSingletonEmptyGuidId = allowNonSingletonEmptyGuidId;
            CachingOptions = cachingOptions;
        }
    }

    public enum AggregateCachingOptions
    {
        EnableForSingleton = 0,
        Disable = 1
    }
}
