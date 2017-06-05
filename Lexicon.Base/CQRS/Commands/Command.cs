using System;

namespace Lexicon.Base.CQRS
{
    public class Command : ICommand
    {
        public string CommandName { get; set; }
        public Guid CommandId { get; set; }
        public int CommandVersion { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid? CausationId { get; set; }
    }
}