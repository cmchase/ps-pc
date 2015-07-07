using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentChild.Model
{
    public class SalesOrder : IObjectWithState
    {

        public SalesOrder()
        {
            SalesOrderItems = new List<SalesOrderItem>();
        }

        public int SalesOrderId { get; set; }

        public string CustomerName { get; set; }

        public string PoNumber { get; set; }

        public virtual List<SalesOrderItem> SalesOrderItems { get; set; }

        public ObjectState ObjectState
        {
            get;
            set;
        }
    }
}
