using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public static class AppSettingKeys
    {
        public const int VatPercentage = 1;
        public const int WithHoldingPercentage = 2;

        public const int ShowBatchesWithQuantityEqualToZeroForIssuance = 3;

        public const int ShowFullyIssuedInterStoreTransferOrdersForIssuance = 4;
        public const int ShowFullyReceivedInterStoreTransferOrdersForReceiving = 5;
        public const int ShowFullyReceivedSMSOrdersForReceiving = 6;
        public const int ShowFullyReceivedPurchaseOrdersForReceiving = 7;
        public const int ShowFullyIssuedRawMaterialRequestsForIssuance = 8;
        public const int ShowFullyIssuedSalesInvoicesForIssuance = 9;
        public const int ShowFullyReturnedSalesInvoicesForReturn = 10;
        public const int ShowFullySoldSalesOrdersForSelling = 11;
        public const int ShowFullyReceivedSalesReturnsForReceiving = 12;
        public const int ShowFullyIssuedSuppliesRequisitionsForIssuance = 13;
        public const int ShowFullyReceivedSuppliesRequisitionsForReceiving = 14;

        public const int AutoGenerateReferenceNumberForInterStoreTransferOrder = 15;
        public const int PrefixForGeneratedReferenceNumberForInterStoreTransferOrder = 16;
        public const int SuffixForGeneratedReferenceNumberForInterStoreTransferOrder = 17;
        public const int NumberOfDigitsForGeneratedReferenceNumberForInterStoreTransferOrder = 51;

        public const int AutoGenerateReferenceNumberForSMSOrder = 18;
        public const int PrefixForGeneratedReferenceNumberForSMSOrder = 19;
        public const int SuffixForGeneratedReferenceNumberForSMSOrder = 20;
        public const int NumberOfDigitsForGeneratedReferenceNumberForSMSOrder = 52;

        public const int AutoGenerateReferenceNumberForPurchaseOrder = 21;
        public const int PrefixForGeneratedReferenceNumberForPurchaseOrder = 22;
        public const int SuffixForGeneratedReferenceNumberForPurchaseOrder = 23;
        public const int NumberOfDigitsForGeneratedReferenceNumberForPurchaseOrder = 53;

        public const int AutoGenerateReferenceNumberForRawMaterialRequest = 24;
        public const int PrefixForGeneratedReferenceNumberForRawMaterialRequest = 25;
        public const int SuffixForGeneratedReferenceNumberForRawMaterialRequest = 26;
        public const int NumberOfDigitsForGeneratedReferenceNumberForRawMaterialRequest = 54;

        public const int AutoGenerateReferenceNumberForSalesInvoice = 27;
        public const int PrefixForGeneratedReferenceNumberForSalesInvoice = 28;
        public const int SuffixForGeneratedReferenceNumberForSalesInvoice = 29;
        public const int NumberOfDigitsForGeneratedReferenceNumberForSalesInvoice = 55;

        public const int AutoGenerateReferenceNumberForSalesOrder = 30;
        public const int PrefixForGeneratedReferenceNumberForSalesOrder = 31;
        public const int SuffixForGeneratedReferenceNumberForSalesOrder = 32;
        public const int NumberOfDigitsForGeneratedReferenceNumberForSalesOrder = 56;

        public const int AutoGenerateReferenceNumberForSalesReturn = 33;
        public const int PrefixForGeneratedReferenceNumberForSalesReturn = 34;
        public const int SuffixForGeneratedReferenceNumberForSalesReturn = 35;
        public const int NumberOfDigitsForGeneratedReferenceNumberForSalesReturn = 57;

        public const int AutoGenerateReferenceNumberForStoreTransactionIssuance = 36;
        public const int PrefixForGeneratedReferenceNumberForStoreTransactionIssuance = 37;
        public const int SuffixForGeneratedReferenceNumberForStoreTransactionIssuance = 38;
        public const int NumberOfDigitsForGeneratedReferenceNumberForStoreTransactionIssuance = 58;

        public const int AutoGenerateReferenceNumberForStoreTransactionReceiving = 39;
        public const int PrefixForGeneratedReferenceNumberForStoreTransactionReceiving = 40;
        public const int SuffixForGeneratedReferenceNumberForStoreTransactionReceiving = 41;
        public const int NumberOfDigitsForGeneratedReferenceNumberForStoreTransactionReceiving = 59;

        public const int AutoGenerateReferenceNumberForStoreTransactionLoss = 42;
        public const int PrefixForGeneratedReferenceNumberForStoreTransactionLoss = 43;
        public const int SuffixForGeneratedReferenceNumberForStoreTransactionLoss = 44;
        public const int NumberOfDigitsForGeneratedReferenceNumberForStoreTransactionLoss = 60;

        public const int AutoGenerateReferenceNumberForStoreTransactionDamage = 45;
        public const int PrefixForGeneratedReferenceNumberForStoreTransactionDamage = 46;
        public const int SuffixForGeneratedReferenceNumberForStoreTransactionDamage = 47;
        public const int NumberOfDigitsForGeneratedReferenceNumberForStoreTransactionDamage = 61;

        public const int AutoGenerateReferenceNumberForSuppliesRequisition = 48;
        public const int PrefixForGeneratedReferenceNumberForSuppliesRequisition = 49;
        public const int SuffixForGeneratedReferenceNumberForSuppliesRequisition = 50;
        public const int NumberOfDigitsForGeneratedReferenceNumberForSuppliesRequisition = 62;

        public const int TresholdAmountToApplyWithHolding = 63;
        public const int DueDateLength = 64;
        public const int TinNumber = 65;
        public const int VatNumber = 66;
        public const int OrganizationAddress = 67;

        public const int AutoGenerateReferenceNumberForPaymentRequest = 68;
        public const int PrefixForGeneratedReferenceNumberForPaymentRequest = 69;
        public const int SuffixForGeneratedReferenceNumberForPaymentRequest = 70;
        public const int NumberOfDigitsForGeneratedReferenceNumberForPaymentRequest = 71;

        public const int AutoGenerateSalesIssuance = 72;
        public const int AutoGenerateTransferIssuanceAndReceiving = 73;

        public const int AutoGenerateReferenceNumberForStoreTransactionInventoryAdjustment = 74;
        public const int PrefixForGeneratedReferenceNumberForStoreTransactionInventoryAdjustment = 75;
        public const int SuffixForGeneratedReferenceNumberForStoreTransactionInventoryAdjustment = 76;
        public const int NumberOfDigitsForGeneratedReferenceNumberForStoreTransactionInventoryAdjustment = 77;
    }
    public static class AppSettingBoolValuedKeys
    {
        public const int ShowBatchesWithQuantityEqualToZeroForIssuance = 3;

        public const int ShowFullyIssuedInterStoreTransferOrdersForIssuance = 4;
        public const int ShowFullyReceivedInterStoreTransferOrdersForReceiving = 5;
        public const int ShowFullyReceivedSMSOrdersForReceiving = 6;
        public const int ShowFullyReceivedPurchaseOrdersForReceiving = 7;
        public const int ShowFullyIssuedRawMaterialRequestsForIssuance = 8;
        public const int ShowFullyIssuedSalesInvoicesForIssuance = 9;
        public const int ShowFullyReturnedSalesInvoicesForReturn = 10;
        public const int ShowFullySoldSalesOrdersForSelling = 11;
        public const int ShowFullyReceivedSalesReturnsForReceiving = 12;
        public const int ShowFullyIssuedSuppliesRequisitionsForIssuance = 13;
        public const int ShowFullyReceivedSuppliesRequisitionsForReceiving = 14;

        public const int AutoGenerateReferenceNumberForInterStoreTransferOrder = 15;
        public const int AutoGenerateReferenceNumberForSMSOrder = 18;
        public const int AutoGenerateReferenceNumberForPurchaseOrder = 21;
        public const int AutoGenerateReferenceNumberForRawMaterialRequest = 24;
        public const int AutoGenerateReferenceNumberForSalesInvoice = 27;
        public const int AutoGenerateReferenceNumberForSalesOrder = 30;
        public const int AutoGenerateReferenceNumberForSalesReturn = 33;
        public const int AutoGenerateReferenceNumberForStoreTransactionIssuance = 36;
        public const int AutoGenerateReferenceNumberForStoreTransactionReceiving = 39;
        public const int AutoGenerateReferenceNumberForStoreTransactionLoss = 42;
        public const int AutoGenerateReferenceNumberForStoreTransactionDamage = 45;
        public const int AutoGenerateReferenceNumberForSuppliesRequisition = 48;

        public const int AutoGenerateReferenceNumberForPaymentRequest = 68;

        public const int AutoGenerateSalesIssuance = 72;
        public const int AutoGenerateTransferIssuanceAndReceiving = 73;
    }

}
