using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class AcademicCalanderSvc : EntityBaseSvc<AcademicCalander>, IComboEntitySvc<AcademicCalander, AcademicCalander, AcademicCalander>
    {
        public AcademicCalanderSvc(ILogger logger)
            : base(logger, "AcademicCalander")
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.AcademicCalanders
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.AcademicName+" from "+m.FromDate.ToString()+" to "+m.ToDate.ToString()
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.AcademicCalanders
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.AcademicName + " from " + m.FromDate.ToString() + " to " + m.ToDate.ToString()
                    };

            return q.AsEnumerable();
        }

        public AcademicCalander GetServiceFilters()
        {
            AcademicCalander AcademicCalander = new AcademicCalander();
            return new AcademicCalander();
        }
        public IQueryable<AcademicCalander> Get()
        {
            return Db.AcademicCalanders;
        }
        public AcademicCalander GetModel(Guid? userId)
        {
            return new AcademicCalander { };
        }
        public AcademicCalander GetModelByUser(Guid? userId)
        {
            return new AcademicCalander { };
        }
        public AcademicCalander GetModel(Guid id)
        {
            var AcademicCalander = this.Get(id);
            return AcademicCalander;
        }
        public AcademicCalander GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public AcademicCalander Get(Guid id)
        {
            return Db.AcademicCalanders.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<AcademicCalander> Get(PageDetail pageDetail)
        {
            var query = Db.AcademicCalanders.AsQueryable();

            List<Expression<Func<AcademicCalander, bool>>> filters = new List<Expression<Func<AcademicCalander, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.AcademicName+m.FromDate+m.ToDate).Contains(pageDetail.Search));


            Func<AcademicCalander, AcademicCalander> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }     
        public Response Validate(AcademicCalander AcademicCalander)
        {
            if (string.IsNullOrWhiteSpace(AcademicCalander.AcademicName))
                return new FailedResponse("Please provide a valid Academic Name.");

            if ((AcademicCalander.AcademicName.Any(char.IsNumber)) || (AcademicCalander.AcademicName.Any(char.IsSymbol)) || (AcademicCalander.AcademicName.Any(char.IsPunctuation)))
                return new FailedResponse("Academic Name must be letter.");

            if (string.IsNullOrWhiteSpace(AcademicCalander.FromDate.ToString()))
                return new FailedResponse("Please provide a valid From Date.");

            if (string.IsNullOrWhiteSpace(AcademicCalander.ToDate.ToString()))
                return new FailedResponse("Please provide a valid To Date.");

            if (AcademicCalander.TableRowGuid == Guid.Empty)
            {
                var queryAcademicName = Db.AcademicCalanders.Where(r => r.AcademicName == AcademicCalander.AcademicName);
                if ((queryAcademicName.ToList().Count > 0))
                    return new FailedResponse("A Academic Calander Name provided has already been created. Please provide a different Academic Calander Name.");
            }
            else
            {
                var queryAcademicName = Db.AcademicCalanders.Where(r => r.AcademicName == AcademicCalander.AcademicName && r.TableRowGuid != AcademicCalander.TableRowGuid);
                if ((queryAcademicName.ToList().Count > 0))
                    return new FailedResponse("An Academic Calander Name provided has already been created. Please provide a different Academic Calander Name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(AcademicCalander AcademicCalander, Guid? userid)
        {
            Response response = new Response();

            response = Validate(AcademicCalander);
            if (!response.Success) return response;

            AcademicCalander.TableRowGuid = Guid.NewGuid();
            AcademicCalander.UserId = userid;            

            return base.Add(AcademicCalander);
        }
        public Response Update(AcademicCalander AcademicCalander, Guid? userid)
        {
            Response response = new Response();

            response = Validate(AcademicCalander);
            if (!response.Success) return response;

            AcademicCalander.UserId = userid;
            
             return base.Update(AcademicCalander);            
        }
        public Response Delete(AcademicCalander AcademicCalander, Guid? userid)
        {
            AcademicCalander.UserId = userid;
            return base.Delete(AcademicCalander);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new AcademicCalander() { TableRowGuid = id }, userid);
        }
    }
}
