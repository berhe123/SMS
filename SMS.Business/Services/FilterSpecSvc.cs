using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class FilterSpecSvc
    {
        //private readonly PurchaseOrderStatusTypeSvc PurchaseOrderStatusTypeSvc;
        
       



        public FilterSpecSvc(ILogger logger)
        {
            //PurchaseOrderStatusTypeSvc = new PurchaseOrderStatusTypeSvc(logger);
            //SalesInvoiceTypeSvc = new SalesInvoiceTypeSvc(logger);
            //PaymentTypeSvc = new PaymentTypeSvc(logger);
            //RawMaterialRequestTypeSvc = new RawMaterialRequestTypeSvc(logger);

            //DeliveryStatusTypeSvc = new DeliveryStatusTypeSvc(logger);
            //DeliveryTypeSvc = new DeliveryTypeSvc(logger);
            //SalesReturnReasonSvc = new SalesReturnReasonSvc(logger);

            //LocationSvc = new LocationSvc(logger);
            //StoreSvc = new StoreSvc(logger);
            //ToStoreSvc = new StoreSvc(logger);

            //EntityUnitSvc = new EntityUnitSvc(logger);
            //SupplierSvc = new SupplierSvc(logger);
            //CustomerSvc = new CustomerSvc(logger);
            //ItemSvc = new ItemSvc(logger);

            //SalesPersonSvc = new SalesPersonSvc(logger);
            //SalesTargetSvc = new SalesTargetSvc(logger);
            //SalesVisitTypeSvc = new SalesVisitTypeSvc(logger);

            //storeTransactionTypeSvc = new StoreTransactionTypeSvc(logger);
           


        }

        public FilterSpec GetFilterSpec(Guid userId)
        {
            return new FilterSpec 
            { 
                DateFrom = DateTime.Today.Date, 
                DateTo = DateTime.Today.Date, 
                ShowAll = false,
                Approved = (int)YesNo.ALL,
                AlreadyUsed = (int)YesNo.ALL,
                ShowByDueDate = true,

                PurchaseOrderStatusTypeGuid = Guid.Empty,
                SalesInvoiceTypeGuid = Guid.Empty,
                PaymentTypeGuid = Guid.Empty,
                RawMaterialRequestTypeGuid = Guid.Empty,

                DeliveryStatusTypeGuid = Guid.Empty,
                DeliveryTypeGuid = Guid.Empty,
                SalesReturnReasonGuid = Guid.Empty,

                LocationGuid = Guid.Empty,
                StoreGuid = Guid.Empty,
                ToStoreGuid = Guid.Empty,
                EntityUnitGuid = Guid.Empty,
                SupplierGuid = Guid.Empty,
                CustomerGuid = Guid.Empty,
                ItemGuid = Guid.Empty,

                TransactionTypeGuid = Guid.Empty,

                //PurchaseOrderStatusTypes = PurchaseOrderStatusTypeSvc.GetComboItems(),
                //SalesInvoiceTypes = SalesInvoiceTypeSvc.GetComboItems(),
                //PaymentTypes = PaymentTypeSvc.GetComboItems(),
                //RawMaterialRequestTypes = RawMaterialRequestTypeSvc.GetComboItems(),

                //DeliveryStatusTypes = DeliveryStatusTypeSvc.GetComboItems(),
                //DeliveryTypes = DeliveryTypeSvc.GetComboItems(),
                //SalesReturnReasons = SalesReturnReasonSvc.GetComboItems(),

                //Locations = LocationSvc.GetComboItems(userId),
                //Stores = StoreSvc.GetComboItems(userId),
                //ToStores = StoreSvc.GetComboItems(userId),

                //EntityUnits = EntityUnitSvc.GetComboItems(userId),
                //Suppliers = SupplierSvc.GetComboItems(),
                //Customers = CustomerSvc.GetComboItems(),
                //Items = ItemSvc.GetComboItems(),
                //YesNos = GetYesNoComboItems(),

                //SalesPersons = SalesPersonSvc.GetComboItems(),
                //SalesTargets = SalesTargetSvc.GetComboItems(),
                //SalesVisitTypes = SalesVisitTypeSvc.GetComboItems(),

                //TransactionTypes = storeTransactionTypeSvc.GetComboItems()

            };
        }
        
        public IEnumerable<ComboItemInt> GetYesNoComboItems()
        {
            List<ComboItemInt> itemList = new List<ComboItemInt>();
            ComboItemInt item;

            //item = new ComboItemInt();
            //item.Value = (int)YesNo.ALL;
            //item.Text = "<All>";
            //itemList.Add(item);

            item = new ComboItemInt();
            item.Value = (int)YesNo.YES;
            item.Text = "Yes";
            itemList.Add(item);

            item = new ComboItemInt();
            item.Value = (int)YesNo.NO;
            item.Text = "No";
            itemList.Add(item);

            return itemList.AsEnumerable();
        }

    }
}
