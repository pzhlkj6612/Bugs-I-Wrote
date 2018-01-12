﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public class BiMapping : IParent, IChild
    {
        private IParent parent;
        private HashSet<IChild> children;
        protected BiMapping()
        {
            parent = null;
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
