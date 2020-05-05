using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser
{
    public class StackOnlyJsonTypeAttribute : Attribute
    {
        public StackOnlyJsonTypeAttribute(string parserTypeName = null, string parserNamespace = null)
        { }
    }
}
