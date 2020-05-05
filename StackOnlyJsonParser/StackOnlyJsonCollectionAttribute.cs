using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser
{
    public class StackOnlyJsonCollectionAttribute : Attribute
    {
        public StackOnlyJsonCollectionAttribute(params StackOnlyCollectionKind[] collectionKind)
        { }
    }
}
