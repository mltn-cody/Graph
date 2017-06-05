using System;
using Newtonsoft.Json.Linq;

namespace Lexicon.Base.CQRS
{
    public  class Event : IEvent
    {
        public Guid AggregateId { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid CausationId { get; set; }
        public JObject Data { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int Version { get; set; }
    }
}