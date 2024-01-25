using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SMS.Core
{
    public enum PaymentModeValues
    {
        [Description("e26773b0-4531-4031-8c58-b5b58350bd89")] // cash
        Cash = 0,
        [Description("9a30a58d-514d-4c32-a542-2ed8ecee855a")] // check
        Check = 1,
        [Description("e85fa335-bf6f-43b1-86a7-a6f8ccf6d706")] //cpo
        CPO = 2
    }

    public enum PaymentSettlementStatusValues
    {
        NotSettled = 0,
        Settled = 1
    }

    public enum CheckStatusValues
    {
        NotCashed = 0,
        Cashed = 1,
        Bounced = 2
    }

    public enum YesNo
    {
        ALL = -1,
        NO = 0,
        YES = 1
    }

    public enum BatchIssuanceOrderTypeEnums
    {
        [Description("218928c2-bbf9-42e6-97a9-0de08286599f")]
        NearestExpiryFirst = 1,
        [Description("72704882-fe6d-4488-8648-e6860bcc3603")]
        OldestManufacturedFirst,
        [Description("dd9fe6d6-99f4-47a5-b3b2-edc13bfe9d0b")]
        SmallestQuantityFirst
    }
    public enum DeliveryRatingTypeEnums
    {
        [Description("a621d32f-9edd-45f5-8462-c6df1a32bfe2")]
        Unacceptable = 0,
        [Description("ee717462-37f5-4510-b57a-d8beab0a04bb")]
        Acceptable,
        [Description("88bfb7aa-5605-4039-b314-419a37cd1547")]
        Good,
        [Description("79707db2-466b-41a9-bae7-289baa0ab28b")]
        VeryGood,
        [Description("a18b7ff2-c747-4f24-9d7d-44826b2c56ec")]
        Exeptional
    }
    public enum DeliveryStatusTypeEnums
    {
        [Description("555eed69-9c37-44c6-ba84-9801f66a5f37")]
        Pending = 1,	
        [Description("15a4d4b8-bc73-4a9e-bd17-15d6a49d5227")]
        Delivered,
        [Description("3d1f4486-0e75-4593-8027-170907e4de2b")]
        PickedUp
    }
    public enum DeliveryTypeEnums
    {
        [Description("cbb9ccbd-087f-4340-959b-f73d0737778f")]
        Deliver = 1,
        [Description("24af95d4-546b-4936-98b3-3cd042e00727")]
        PickUp
    }
    public enum EntityUnitTypeValues
    {
        [Description("3eef912c-2d25-4c5e-b2b6-5ca1c4faf99a")]
        Shop = 1,
        [Description("4d5550ba-463f-407d-8719-6d006e653ce3")]
        Factory
    }
    public enum PaymentTypeEnums
    {
        [Description("6442a239-7e97-40a4-b4af-f8eac99384ab")]
        Cash = 1,	
        [Description("5e0696d4-87c4-4816-bf34-d79d6cfa7693")]
        Check,
        [Description("dc7733f5-325f-4d55-b3a6-62d51b4058e3")]
        PostDatedCheck,
        [Description("6ccfb1bc-379e-4a51-8b60-9abf31d8466b")]
        Credit	
    }
    public enum SMSOrderTypeEnums
    {
        [Description("7323e514-4173-454d-a8e7-bb7dff9a5826")]
        Planned = 1,
        [Description("6a856c7e-627d-43f9-afff-d6c19b83c071")]
        CustomerOrder,
        [Description("853f91dc-135f-4387-9c95-0f89d9a2b350")]
        Other
    }
    public enum PurchaseOrderStatusTypeEnums
    {
        [Description("b1408888-38c9-48b3-a92b-f87bf782df40")]
        OnOrder = 1,
        [Description("a6e10d30-3e88-40f3-861e-42ecf8daf2bf")]
        AwaitingDelivery
    }
    public enum PurchaseOrderTypeEnums
    {
        [Description("1d041dc9-34c3-41c4-be2c-166197754a90")]
        Regular = 1,
        [Description("39651cb2-cc97-4f69-b33c-20fc97e9c98b")]
        Urgent
    }
    public enum RawMaterialRequestTypeEnums
    {
        [Description("808f23df-b46c-46b6-9c0c-3836ef2b06db")]
        Normal = 1,
        [Description("0d610f3d-6b83-420e-8a46-c093a6145a68")]
        Additional
    }
    public enum SalesInvoiceTypeEnums
    {
        [Description("ae18b0f5-21f0-4061-b217-68f013fa6738")]
        CashSales = 1,
        [Description("a2ccfc43-1b91-42ae-94b5-33b9671b6e17")]
        CreditSales,
        [Description("96b94567-c6a9-4112-9ba2-4b0af703c2f0")]
        FreeOfCharge
    }
    public enum SalesVisitTypeEnums
    {
        [Description("f6a9d6c6-0c9e-4f0c-8f2c-e3a1c06dd7ea")]
        FirstTime = 1,
        [Description("ca4392ce-2411-4b69-89ae-901d13514e32")]
        Revisit
    }
    public enum StoreTransactionTypeValues
    {
        [Description("f9e57d61-1b89-4323-ac87-4f0e291b90f7")]
        PurchaseReceiving = 1,
        [Description("7f4c804b-2510-44ad-9a66-44cd7f3d6370")]
        SuppliesReceiving = 2,
        [Description("e8d332cf-7254-4c04-8a92-0aec3d834ea7")]
        SMSReceiving = 3,
        [Description("9b585f05-1453-49aa-b93e-437406449357")]
        SalesReturnReceiving = 4,
        [Description("dc5d2f7d-d96c-420b-ac37-1746319d6a99")]
        SalesIssuance = 5,
        [Description("4bed2c8b-7d03-445a-b753-ee0be709f956")]
        SuppliesIssuance = 6,
        [Description("7c098d24-66e9-49b2-9f62-2efcc519cd62")]
        RawMaterialIssuance = 7,
        [Description("b4db6d55-a8c7-4fa3-b1c6-b9945da42c66")]
        TransferIssuance = 8,
        [Description("9eb07ceb-6790-4443-aab6-dd26b3e93430")]
        TransferReceiving = 9,
        [Description("5c9cd2c3-bee6-43ed-84ae-46ab1df532a9")]
        Damage = 10,
        [Description("4bc7044f-69cd-43f7-b32c-453fa6f64ac0")]
        Loss = 11,
        [Description("75d3de28-1650-4cce-a5da-620a402aaeea")]
        StoreIssuance = 12,
        [Description("1e2fc273-d202-4b84-9394-6060038bf031")]
        InventoryAdjustment = 13,
        [Description("3be8b50f-1db9-43f2-9f12-e62ff73cb24d")]
        BeginningBalance = 14
        

    }
    public enum StoreTypeEnums
    {
        [Description("20f11c51-8fc3-4005-a749-2705457900e6")]
        RawMaterial = 1,
        [Description("2d4be9d0-5553-46fc-a5e6-b2e44348c7c2")]
        FinishedGoodsRawMaterial,
        [Description("6d7a683e-4239-4ac9-a5ab-c5363261bad6")]
        FinishedGoods
    }
    public enum SupplierTypeEnums
    {
        [Description("faad8ac8-b863-402b-a09d-7e41ded5120a")]
        Local = 1,
        [Description("f12044e5-562c-41c5-9ddc-949dfcf4fc5b")]
        NonLocal
    }

    // resource: html-color-codes  http://html-color-codes.info/
    // left part stores color, right part stores className for html rendering
    public enum ScheduleStatus
    {
        [Description("#01DF3A:PASSEDVISITED")] // green
        PassedVisited = 0,
        [Description("#FF8000:NEARNOTVISITED")] // orange
        NearNotVisited,
        [Description("#FF0000:PASSEDNOTVISITED")] // red
        PassedNotVisited

    }

    public static class Enums
    {
        /// Get all values
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// Get all the names
        public static IEnumerable<T> GetNames<T>()
        {
            return Enum.GetNames(typeof(T)).Cast<T>();
        }

        /// Get the name for the enum value
        public static string GetName<T>(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        /// Get the underlying value for the Enum string
        public static int GetValue<T>(string enumString)
        {
            return (int)Enum.Parse(typeof(T), enumString.Trim());
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }
}
