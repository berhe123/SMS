using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class PaymentCalanderSvc : EntityBaseSvc<PaymentCalander>, IComboEntitySvc<PaymentCalander, PaymentCalander, PaymentCalander>
    {
        readonly MonthSvc monthSvc;
        public PaymentCalanderSvc(ILogger logger)
            : base(logger, "PaymentCalander")
        {
            monthSvc = new MonthSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.PaymentCalanders
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Month.MonthName + " from " + m.RegularPaymentDateFrom + " to " +m.RegularPaymentDateTo
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.PaymentCalanders
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Month.MonthName + " from " + m.RegularPaymentDateFrom + " to " + m.RegularPaymentDateTo
                    };

            return q.AsEnumerable();
        }

        public PaymentCalander GetServiceFilters()
        {
            PaymentCalander PaymentCalander = new PaymentCalander();
            return new PaymentCalander();
        }
        public IQueryable<PaymentCalander> Get()
        {
            return Db.PaymentCalanders;
        }
        public PaymentCalander GetModel(Guid? userId)
        {
            return new PaymentCalander 
            {
                Months = monthSvc.GetComboItems()
            };
        }
        public PaymentCalander GetModelByUser(Guid? userId)
        {
            return new PaymentCalander 
            {
                Months = monthSvc.GetComboItems()
            };
        }
        public PaymentCalander GetModel(Guid id)
        {
            var PaymentCalander = this.Get(id);
            PaymentCalander.Months = monthSvc.GetComboItems(Db);
            return PaymentCalander;
        }
        public PaymentCalander GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public PaymentCalander Get(Guid id)
        {
            return Db.PaymentCalanders.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<PaymentCalander> Get(PageDetail pageDetail)
        {
            var query = Db.PaymentCalanders.AsQueryable();

            List<Expression<Func<PaymentCalander, bool>>> filters = new List<Expression<Func<PaymentCalander, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Month.MonthName.Contains(pageDetail.Search));


            Func<PaymentCalander, PaymentCalander> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_PaymentCalanderList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_PaymentCalanderList.AsQueryable();

            List<Expression<Func<vw_PaymentCalanderList, bool>>> filters = new List<Expression<Func<vw_PaymentCalanderList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.MonthName.Contains(pageDetail.Search));


            Func<vw_PaymentCalanderList, vw_PaymentCalanderList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(PaymentCalander PaymentCalander)
        {
            if (string.IsNullOrWhiteSpace(PaymentCalander.MonthGuid.ToString()))
                return new FailedResponse("Please provide a valid Payment Name.");

            if (string.IsNullOrWhiteSpace(PaymentCalander.RegularPaymentDateFrom.ToString()))
                return new FailedResponse("Please provide a valid Regular Payment Date at From.");

            if (string.IsNullOrWhiteSpace(PaymentCalander.RegularPaymentDateTo.ToString()))
                return new FailedResponse("Please provide a valid Regular Payment Date at To.");           

            if (PaymentCalander.TableRowGuid == Guid.Empty)
            {
                var queryPaymentName = Db.PaymentCalanders.Where(r => r.MonthGuid == PaymentCalander.MonthGuid);
                if ((queryPaymentName.ToList().Count > 0))
                    return new FailedResponse("A Payment Calander Name provided has already been created. Please provide a different Payment Calander Name.");
            }
            else
            {
                var queryPaymentName = Db.PaymentCalanders.Where(r => r.MonthGuid == PaymentCalander.MonthGuid && r.TableRowGuid != PaymentCalander.TableRowGuid);
                if ((queryPaymentName.ToList().Count > 0))
                    return new FailedResponse("An Payment Calander Name provided has already been created. Please provide a different Payment Calander Name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(PaymentCalander PaymentCalander, Guid? userid)
        {
            Response response = new Response();

            response = Validate(PaymentCalander);
            if (!response.Success) return response;

            PaymentCalander.TableRowGuid = Guid.NewGuid();
            PaymentCalander.UserId = userid;

            return base.Add(PaymentCalander);
        }
        public Response Update(PaymentCalander PaymentCalander, Guid? userid)
        {
            Response response = new Response();

            response = Validate(PaymentCalander);
            if (!response.Success) return response;

            PaymentCalander.UserId = userid;
            
             return base.Update(PaymentCalander);            
        }
        public Response Delete(PaymentCalander PaymentCalander, Guid? userid)
        {
            PaymentCalander.UserId = userid;
            return base.Delete(PaymentCalander);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new PaymentCalander() { TableRowGuid = id }, userid);
        }
    }
}
