using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public abstract class ININode : InMapping
    {
        public string value { get; set; }
        public string comt { get; set; }
        public bool isComt { get; set; }
        public ININode()
        {
            value = "";
            comt = "";
            isComt = false;
        }
        public ININode(string value, string comt, bool isComt)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (comt == null)
                throw new ArgumentNullException(nameof(comt));

            this.value = value;
            this.comt = comt;
            this.isComt = isComt;
        }
        public ININode(ININode anotherNode)
        {
            if (anotherNode == null)
                throw new ArgumentNullException(nameof(anotherNode));

            value = anotherNode.value;
            comt = anotherNode.comt;
            isComt = anotherNode.isComt;
        }
    }
}
