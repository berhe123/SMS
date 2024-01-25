using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class PaymentCalandersToClassRoomSvc : EntityBaseSvc<PaymentCalandersToClassRoom>, IComboEntitySvc<PaymentCalandersToClassRoom, PaymentCalandersToClassRoom, PaymentCalandersToClassRoom>
    {
        readonly MonthSvc monthSvc;
        readonly ClassRoomsForPaymentCalanderSvc classroomforpaymentcalanderSvc;
        public PaymentCalandersToClassRoomSvc(ILogger logger)
            : base(logger, "PaymentCalandersToClassRoom")
        {
            monthSvc = new MonthSvc(logger);
            classroomforpaymentcalanderSvc = new ClassRoomsForPaymentCalanderSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.PaymentCalandersToClassRooms
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoomsForPaymentCalander.ClassRoomName + " => " + m.Month.MonthName + " from " + m.RegularPaymentDateFrom + " to " +m.RegularPaymentDateTo
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.PaymentCalandersToClassRooms
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoomsForPaymentCalander.ClassRoomName + " => " + m.Month.MonthName + " from " + m.RegularPaymentDateFrom + " to " + m.RegularPaymentDateTo
                    };

            return q.AsEnumerable();
        }

        public PaymentCalandersToClassRoom GetServiceFilters()
        {
            PaymentCalandersToClassRoom PaymentCalandersToClassRoom = new PaymentCalandersToClassRoom();
            return new PaymentCalandersToClassRoom();
        }
        public IQueryable<PaymentCalandersToClassRoom> Get()
        {
            return Db.PaymentCalandersToClassRooms;
        }
        public PaymentCalandersToClassRoom GetModel(Guid? userId)
        {
            return new PaymentCalandersToClassRoom 
            {
                    Months = monthSvc.GetComboItems(),
                    ClassRooms = classroomforpaymentcalanderSvc.GetComboItems()
            };
        }
        public PaymentCalandersToClassRoom GetModelByUser(Guid? userId)
        {
            return new PaymentCalandersToClassRoom 
            { 
                Months = monthSvc.GetComboItems(),
                ClassRooms = classroomforpaymentcalanderSvc.GetComboItems()            
            };
        }
        public PaymentCalandersToClassRoom GetModel(Guid id)
        {
            var PaymentCalandersToClassRoom = this.Get(id);
            PaymentCalandersToClassRoom.Months = monthSvc.GetComboItems(Db);
            PaymentCalandersToClassRoom.ClassRooms = classroomforpaymentcalanderSvc.GetComboItems(Db);
            return PaymentCalandersToClassRoom;
        }
        public PaymentCalandersToClassRoom GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public PaymentCalandersToClassRoom Get(Guid id)
        {
            return Db.PaymentCalandersToClassRooms.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<PaymentCalandersToClassRoom> Get(PageDetail pageDetail)
        {
            var query = Db.PaymentCalandersToClassRooms.AsQueryable();

            List<Expression<Func<PaymentCalandersToClassRoom, bool>>> filters = new List<Expression<Func<PaymentCalandersToClassRoom, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Month.MonthName + m.ClassRoomsForPaymentCalander.ComputerName).Contains(pageDetail.Search));

            Func<PaymentCalandersToClassRoom, PaymentCalandersToClassRoom> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_PaymentCalanderToClassRoomList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_PaymentCalanderToClassRoomList.AsQueryable();

            List<Expression<Func<vw_PaymentCalanderToClassRoomList, bool>>> filters = new List<Expression<Func<vw_PaymentCalanderToClassRoomList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.MonthName + m.ClassRoomName).Contains(pageDetail.Search));

            Func<vw_PaymentCalanderToClassRoomList, vw_PaymentCalanderToClassRoomList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(PaymentCalandersToClassRoom PaymentCalandersToClassRoom)
        {
            if (string.IsNullOrWhiteSpace(PaymentCalandersToClassRoom.MonthGuid.ToString()))
                return new FailedResponse("Please provide a valid Month Name.");

            if (string.IsNullOrWhiteSpace(PaymentCalandersToClassRoom.ClassRoomGuid.ToString()))
                return new FailedResponse("Please provide a valid Class Room Name.");

            if (string.IsNullOrWhiteSpace(PaymentCalandersToClassRoom.RegularPaymentDateFrom.ToString()))
                return new FailedResponse("Please provide a valid Regular Payment Date at From.");

            if (string.IsNullOrWhiteSpace(PaymentCalandersToClassRoom.RegularPaymentDateTo.ToString()))
                return new FailedResponse("Please provide a valid Regular Payment Date at To.");           

            if (PaymentCalandersToClassRoom.TableRowGuid == Guid.Empty)
            {
                var queryPaymentName = Db.PaymentCalandersToClassRooms.Where(r => r.MonthGuid == PaymentCalandersToClassRoom.MonthGuid);
                var queryClassRoomName = Db.PaymentCalandersToClassRooms.Where(r => r.ClassRoomGuid == PaymentCalandersToClassRoom.ClassRoomGuid);
                if ((queryPaymentName.ToList().Count > 0) && (queryClassRoomName.ToList().Count > 0))
                    return new FailedResponse("A Payment Calander Name provided has already been created. Please provide a different Payment Calander Name.");
            }
            else
            {
                var queryPaymentName = Db.PaymentCalandersToClassRooms.Where(r => r.MonthGuid == PaymentCalandersToClassRoom.MonthGuid && r.TableRowGuid != PaymentCalandersToClassRoom.TableRowGuid);
                var queryClassRoomName = Db.PaymentCalandersToClassRooms.Where(r => r.ClassRoomGuid == PaymentCalandersToClassRoom.ClassRoomGuid && r.TableRowGuid != PaymentCalandersToClassRoom.TableRowGuid);
                if ((queryPaymentName.ToList().Count > 0) && (queryClassRoomName.ToList().Count > 0))
                    return new FailedResponse("An Payment Calander Name provided has already been created. Please provide a different Payment Calander Name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(PaymentCalandersToClassRoom PaymentCalandersToClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(PaymentCalandersToClassRoom);
            if (!response.Success) return response;

            PaymentCalandersToClassRoom.TableRowGuid = Guid.NewGuid();
            PaymentCalandersToClassRoom.UserId = userid;

            return base.Add(PaymentCalandersToClassRoom);
        }
        public Response Update(PaymentCalandersToClassRoom PaymentCalandersToClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(PaymentCalandersToClassRoom);
            if (!response.Success) return response;

            PaymentCalandersToClassRoom.UserId = userid;
            
             return base.Update(PaymentCalandersToClassRoom);            
        }
        public Response Delete(PaymentCalandersToClassRoom PaymentCalandersToClassRoom, Guid? userid)
        {
            PaymentCalandersToClassRoom.UserId = userid;
            return base.Delete(PaymentCalandersToClassRoom);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new PaymentCalandersToClassRoom() { TableRowGuid = id }, userid);
        }
    }
}
