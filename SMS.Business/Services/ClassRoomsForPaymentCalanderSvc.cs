using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class ClassRoomsForPaymentCalanderSvc : ReadOnlyEntityBaseSvc<ClassRoomsForPaymentCalander>, IReadOnlyEntitySvc<ClassRoomsForPaymentCalander,ClassRoomsForPaymentCalander,ClassRoomsForPaymentCalander>
    {
        //SMSEntities db = new SMSEntities();
        public ClassRoomsForPaymentCalanderSvc(ILogger logger)
            : base(logger)
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.ClassRoomsForPaymentCalanders.OrderBy(m => m.ClassRoomSort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoomName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.ClassRoomsForPaymentCalanders.OrderBy(m => m.ClassRoomSort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoomName
                    };

            return q.AsEnumerable();
        }

        public ClassRoomsForPaymentCalander GetServiceFilters()
        {
            ClassRoomsForPaymentCalander ClassRoomsForPaymentCalander = new ClassRoomsForPaymentCalander();
            return new ClassRoomsForPaymentCalander();
        }

        public IQueryable<ClassRoomsForPaymentCalander> Get()
        {
            return Db.ClassRoomsForPaymentCalanders;
        }

        public ClassRoomsForPaymentCalander GetModel()
        {
            return new ClassRoomsForPaymentCalander();

        }

        public ClassRoomsForPaymentCalander GetModel(Guid id)
        {
            var ClassRoomsForPaymentCalander = this.Get(id);
            return ClassRoomsForPaymentCalander;
        }

        public ClassRoomsForPaymentCalander GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public ClassRoomsForPaymentCalander Get(Guid id)
        {
            return Db.ClassRoomsForPaymentCalanders.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<ClassRoomsForPaymentCalander> Get(PageDetail pageDetail)
        {
            var query = Db.ClassRoomsForPaymentCalanders.AsQueryable();

            List<Expression<Func<ClassRoomsForPaymentCalander, bool>>> filters = new List<Expression<Func<ClassRoomsForPaymentCalander, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.ClassRoomName.Contains(pageDetail.Search));


            Func<ClassRoomsForPaymentCalander, ClassRoomsForPaymentCalander> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }    

    }
}
