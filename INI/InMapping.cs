using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public class InMapping : IChild
    {
        private IParent parent;
        protected InMapping()
        {
            parent = null;
        }
        public IParent GetParent()
        {
            return parent;
        }
        public void SetParent(IParent parent)
        {
            this.parent = parent;
        }
    }
}
