var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};

// TODO: I'm having a hard time understanding this.
// Seek out explaination of mapping.
SalesOrderItemMapping = {
    "SalesOrderItems": {
        key: function(salesOrderItem) {
            return ko.utils.unwrapObservable(salesOrderItem.SalesOrderItemId);
        },
        create: function (options) {
            console.log(options.data);
            return new SalesOrderItemViewModel(options.data);
        }
    }
};

SalesOrderItemViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, SalesOrderItemMapping, self);
};

SalesOrderViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, SalesOrderItemMapping, self);

    self.save = function () {
        $.ajax({
            url: "/Sales/Save/",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {

                if (data.salesOrderViewModel != null) {
                    ko.mapping.fromJS(data.salesOrderViewModel, {}, self);
                }

                if (data.newLocation != null) {
                    window.location = data.newLocation;
                }
            }
        });
    }

    self.flagSalesOrderAsEdited = function () {
        if (self.ObjectState() !== ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }
        return true;
    }

    self.addSalesOrderItem = function() {
        var salesOrderItem = new SalesOrderItemViewModel({ SalesOrderItemId: 0, ProductCode: "", Quantity: 1, UnitPrice: 0, ObjectState: ObjectState.Added });
        self.SalesOrderItems.push(salesOrderItem);
    }
};