using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public interface IChild
    {
        void SetParent(IParent parent);
        IParent GetParent();
    }
}
