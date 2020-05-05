using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class StackOnlyJsonCollectionAttribute : Attribute
    {
        public StackOnlyJsonCollectionAttribute(params JsonCollectionKind[] collectionKind)
        { }
    }
}
