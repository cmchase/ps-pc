using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ParentChild.Model;

namespace ParentChild.DataLibrary
{

    public static class Helpers
    {
        public static EntityState ConvertState(ObjectState objectState)
        {
            switch (objectState)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                case ObjectState.Modified:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }

        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = ConvertState(stateInfo.ObjectState);
            }
        }
    }
}
