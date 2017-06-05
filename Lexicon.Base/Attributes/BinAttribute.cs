using System;
using System.Runtime.CompilerServices;

namespace Lexicon.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BinAttribute : Attribute
    {
        public string[] BinCatagories;
        public string PropertyName;

        public BinAttribute([CallerMemberName] string propertyName = null, params string[] binNames)
        {
            PropertyName = propertyName;
            BinCatagories = binNames;
        }
    }
}
