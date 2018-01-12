using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public interface IParent
    {
        void AddChild(IChild child);
        void RemoveChild(IChild child);
    }
}
