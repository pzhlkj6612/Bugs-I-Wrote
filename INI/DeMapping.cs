using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    //unused.
    public abstract class DeMapping : IParent
    {
        private HashSet<IChild> children;
        protected DeMapping()
        {
            children = new HashSet<IChild>();
        }
        public void AddChild(IChild child)
        {
            children.Add(child);
            child.SetParent(this);
        }
        public void RemoveChild(IChild child)
        {
            children.Remove(child);
            child.SetParent(null);
        }
    }
}
