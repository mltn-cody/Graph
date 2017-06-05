using System;

namespace Lexicon.Base.CQRS
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}