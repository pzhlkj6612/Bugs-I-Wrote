using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public class INILine : IChild
    {
        public string value { get; set; }
        public string comt { get; set; }
        public bool isComt { get; set; }
        public INILine()
        {
            value = "";
            comt = "";
            isComt = false;
        }
        public INILine(string value, string comt, bool isComt)
        {
            this.value = value;
            this.comt = comt;
            this.isComt = isComt;
        }
        public INILine(INILine line)
        {
            value = line.value;
            comt = line.comt;
            isComt = line.isComt;
        }

        private IParent parent;
        public void SetParent(IParent parent)
        {
            this.parent = parent;
        }
        public IParent GetParent()
        {
            return parent;
        }
    }
}
