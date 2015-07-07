using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParentChild.Web.ViewModels
{
    using ParentChild.Model;

    public class SalesOrderViewModel : IObjectWithState
    {
        public SalesOrderViewModel()
        {
            SalesOrderItems = new List<SalesOrderItemViewModel>();
        }

        public int SalesOrderId { get; set; }

        public string CustomerName { get; set; }

        public string PoNumber { get; set; }

        public string MessageToClient { get; set; }

        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; }

        public ObjectState ObjectState { get; set; }
    }
}