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
                throw new ArgumentNullException("Argument \"value\" NULL.");
            if (comt == null)
                throw new ArgumentNullException("Argument \"comt\" NULL.");

            this.value = value;
            this.comt = comt;
            this.isComt = isComt;
        }
        public ININode(ININode n)
        {
            if (n == null)
                throw new ArgumentNullException("Argument \"line\" NULL.");

            value = n.value;
            comt = n.comt;
            isComt = n.isComt;
        }
    }
}
