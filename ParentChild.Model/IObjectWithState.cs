using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentChild.Model
{
    public interface IObjectWithState
    {
        ObjectState ObjectState { get; set; }

    }
    public enum ObjectState
    {
        Unchanged = 0,
        Added,
        Modified,
        Deleted
    }
}
