using ParentChild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParentChild.Web.ViewModels
{
    public static class Helpers
    {
        public static SalesOrderViewModel CreateSalesOrderViewModelFromSalesOrder(SalesOrder salesOrder)
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel()
                                                          {
                                                              CustomerName = salesOrder.CustomerName,
                                                              PoNumber = salesOrder.PoNumber,
                                                              SalesOrderId = salesOrder.SalesOrderId,
                                                              ObjectState = ObjectState.Unchanged
                                                          };


            foreach (SalesOrderItem salesOrderItem in salesOrder.SalesOrderItems)
            {
                SalesOrderItemViewModel salesOrderItemViewModel = new SalesOrderItemViewModel();
                salesOrderItemViewModel.SalesOrderItemId = salesOrderItem.SalesOrderItemId;
                salesOrderItemViewModel.ProductCode = salesOrderItem.ProductCode;
                salesOrderItemViewModel.Quantity = salesOrderItem.Quantity;
                salesOrderItemViewModel.UnitPrice = salesOrderItem.UnitPrice;
                salesOrderItemViewModel.ObjectState = ObjectState.Unchanged;
                salesOrderItemViewModel.SalesOrderId = salesOrderItem.SalesOrderId;

                salesOrderViewModel.SalesOrderItems.Add(salesOrderItemViewModel);
            }


            return salesOrderViewModel;
        }

        public static SalesOrder CreateSalesOrderFromSalesOrderViewModel(SalesOrderViewModel salesOrderViewModel)
        {
            SalesOrder salesOrder = new SalesOrder()
            {
                CustomerName = salesOrderViewModel.CustomerName,
                PoNumber = salesOrderViewModel.PoNumber,
                SalesOrderId = salesOrderViewModel.SalesOrderId,
                ObjectState = salesOrderViewModel.ObjectState
            };

            int temporarySalesOrderItemId = -1;

            foreach (SalesOrderItemViewModel salesOrderItemViewModel in salesOrderViewModel.SalesOrderItems)
            {
                SalesOrderItem salesOrderItem = new SalesOrderItem();
                salesOrderItem.SalesOrderItemId = salesOrderItemViewModel.SalesOrderItemId;
                salesOrderItem.ProductCode = salesOrderItemViewModel.ProductCode;
                salesOrderItem.Quantity = salesOrderItemViewModel.Quantity;
                salesOrderItem.UnitPrice = salesOrderItemViewModel.UnitPrice;
                salesOrderItem.ObjectState = salesOrderItemViewModel.ObjectState;

                if (salesOrderItemViewModel.ObjectState != ObjectState.Added)
                {
                    salesOrderItem.SalesOrderItemId = salesOrderItemViewModel.SalesOrderItemId;
                } else {
                    salesOrderItem.SalesOrderItemId = temporarySalesOrderItemId;
                    temporarySalesOrderItemId--;
                }


                salesOrderItem.SalesOrderId = salesOrderViewModel.SalesOrderId;

                salesOrder.SalesOrderItems.Add(salesOrderItem);
            }

            return salesOrder;
        }

        public static string GetMessageToClient(ObjectState objectState, string customerName)
        {
            string messageToClient = string.Empty;

            switch (objectState)
            {
                case ObjectState.Added:
                    messageToClient = string.Format("Saving new entry for {0}", customerName);
                    break;
                case ObjectState.Modified:
                    messageToClient = string.Format("Saving changes to {0}", customerName);
                    break;
            }

            return messageToClient;
        }
    }
}