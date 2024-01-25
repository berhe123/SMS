using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class PaymentSvc : EntityBaseSvc<Payment>, IComboEntitySvc<Payment, Payment, Payment>
    {       
        readonly ClassRoomSvc classroomSvc;
        readonly PaymentCalanderSvc paymentcalanderSvc;
        readonly FeeSettingSvc feesettingSvc;

        public PaymentSvc(ILogger logger)
            : base(logger, "Payment")
        {
            classroomSvc = new ClassRoomSvc(logger);
            paymentcalanderSvc = new PaymentCalanderSvc(logger);
            feesettingSvc = new FeeSettingSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Payments
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName+" "+m.Student.FatherName+" "+m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Payments
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName 
                    };

            return q.AsEnumerable();
        }

        public Payment GetServiceFilters()
        {
            Payment Payment = new Payment();
            return new Payment();
        }
        public IQueryable<Payment> Get()
        {
            return Db.Payments;
        }
        public Payment GetModel(Guid? userId)
        {
            return new Payment() {
                ClassRooms = classroomSvc.GetComboItems(),
                //Students=GetComboItemsOfStudents(),
                PaymentCalanders = paymentcalanderSvc.GetComboItems(),   
                //FeeSettings=feesettingSvc.GetComboItems()
            };

        }
        public Payment GetModelByUser(Guid? userId)
        {
            return new Payment()
            {
                ClassRooms = classroomSvc.GetComboItems(),
                PaymentCalanders = paymentcalanderSvc.GetComboItems(),
            };
        }
        public Payment GetModel(Guid id)
        {
            var Payment = this.Get(id);

            Payment.ClassRooms = classroomSvc.GetComboItems(Db);
            Payment.PaymentCalanders = paymentcalanderSvc.GetComboItems(Db);

            return Payment;
        }
        public Payment GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public Payment Get(Guid id)
        {
            return Db.Payments.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<Payment> Get(PageDetail pageDetail)
        {
            var query = Db.Payments.AsQueryable();

            List<Expression<Func<Payment, bool>>> filters = new List<Expression<Func<Payment, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName+m.Student.FatherName+m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<Payment, Payment> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_PaymentList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_PaymentList.AsQueryable();

            List<Expression<Func<vw_PaymentList, bool>>> filters = new List<Expression<Func<vw_PaymentList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Payment.Contains(pageDetail.Search));


            Func<vw_PaymentList, vw_PaymentList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(Payment Payment)
        {
            if (string.IsNullOrWhiteSpace(Payment.StudentGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(Payment.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(Payment.PaymentDate.ToString()))
            //    return new FailedResponse("Please provide a valid Payment Date.");

            //if (string.IsNullOrWhiteSpace(Payment.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (Payment.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.Payments.Where(r => r.StudentGuid == Payment.StudentGuid);
            //    var queryMonth = Db.Payments.Where(r => r.MonthGuid == Payment.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A Payment provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.Payments.Where(r => r.StudentGuid == Payment.StudentGuid && r.TableRowGuid != Payment.TableRowGuid);
            //    var queryMonth = Db.Payments.Where(r => r.MonthGuid == Payment.MonthGuid && r.TableRowGuid != Payment.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An Payment provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }
        public Response Add(Payment Payment, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Payment);
            if (!response.Success) return response;

            Payment.TableRowGuid = Guid.NewGuid();
            Payment.UserId = userid;

            return base.Add(Payment);
        }
        public Response Update(Payment Payment, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Payment);
            if (!response.Success) return response;

            Payment.UserId = userid;

            return base.Update(Payment);
        }
        public Response Delete(Payment Payment, Guid? userid)
        {
            Payment.UserId = userid;
            return base.Delete(Payment);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Payment() { TableRowGuid = id }, userid);
        }
    }
}
