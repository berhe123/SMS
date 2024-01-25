using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SMS.Core
{
    public class PageDetail
    {
        // For datatable
        public int DisplayStart { get; set; }
        public int DisplayLength { get; set; }
        public string SortColumn { get; set; }
        public bool IsAsc { get; set; }
        public string Search { get; set; }
        public bool IncludeInactive { get; set; }

        // Specific to this project
        public Guid? Id { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool ShowAll { get; set; }
        public int Approved { get; set; }
        public int AlreadyUsed { get; set; }
        public bool ShowByDueDate { get; set; }
        public int SettlementStatus { get; set; }
        public int CheckCashingStatus { get; set; }


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
        public Guid ToStoreGuid { get; set; }

        public bool? IsSalesSummary { get; set; }

        public Guid UserId { get; set; }
        public Guid IndividualWorkScheduleGuid { get; set; }
        public Guid ClassRoomGuid { get; set; }
        public Guid QuizScheduleGuid { get; set; }
        public Guid TestScheduleGuid { get; set; }
        public Guid SubjectTeacherClassRoomGuid { get; set; }
        public Guid ExamScheduleGuid { get; set; }
        public Guid ExerciseBookGuid { get; set; }
    }
}
