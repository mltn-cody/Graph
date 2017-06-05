using System;
using Newtonsoft.Json.Linq;

namespace Lexicon.Base.CQRS
{
    public interface IEvent
    {
        int Version { get; set; }
        Guid AggregateId { get; set; }
        Guid EventId { get;  set; }
        JObject Data { get; set; }
    }
}