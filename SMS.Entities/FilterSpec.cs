using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public class FilterSpec
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool ShowAll { get; set; }
        public int Approved { get; set; }
        public int AlreadyUsed { get; set; }
        public bool ShowByDueDate { get; set; }

        public Guid PurchaseOrderStatusTypeGuid { get; set; }
        public Guid SalesInvoiceTypeGuid { get; set; }
        public Guid PaymentTypeGuid { get; set; }
        public Guid RawMaterialRequestTypeGuid { get; set; }

        public Guid DeliveryStatusTypeGuid { get; set; }
        public Guid DeliveryTypeGuid { get; set; }
        public Guid SalesReturnReasonGuid { get; set; }

        public Guid LocationGuid { get; set; }
        public Guid StoreGuid { get; set; }
        public Guid EntityUnitGuid { get; set; }
        public Guid SupplierGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public Guid ItemGuid { get; set; }


        public Guid TransactionTypeGuid { get; set; }
        public Guid ToStoreGuid { get; set; }
        public Guid SalesPersonGuid { get; set; }


        public IEnumerable<ComboItem> PurchaseOrderStatusTypes { get; set; }
        public IEnumerable<ComboItem> SalesInvoiceTypes { get; set; }
        public IEnumerable<ComboItem> PaymentTypes { get; set; }
        public IEnumerable<ComboItem> RawMaterialRequestTypes { get; set; }
        
        public IEnumerable<ComboItem> DeliveryStatusTypes { get; set; }
        public IEnumerable<ComboItem> DeliveryTypes { get; set; }
        public IEnumerable<ComboItem> SalesReturnReasons { get; set; }

        public IEnumerable<ComboItem> Locations { get; set; }
        public IEnumerable<ComboItem> Stores { get; set; }
        public IEnumerable<ComboItem> ToStores { get; set; }
        public IEnumerable<ComboItem> EntityUnits { get; set; }
        public IEnumerable<ComboItem> Suppliers { get; set; }
        public IEnumerable<ComboItem> Customers { get; set; }
        public IEnumerable<ComboItem> Items { get; set; }
        public IEnumerable<ComboItemInt> YesNos { get; set; }
               

        public IEnumerable<ComboItem> SalesPersons { get; set; }
        public IEnumerable<ComboItem> SalesTargets { get; set; }
        public IEnumerable<ComboItem> SalesVisitTypes { get; set; }

        public IEnumerable<ComboItem> TransactionTypes { get; set; }
        
    }
}
