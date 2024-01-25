using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class WorkerSvc : EntityBaseSvc<Worker>, IComboEntitySvc<Worker, Worker, Worker>
    {
        readonly GenderSvc genderSvc;
        readonly UserProfileSvc userProfileSvc;
        public WorkerSvc(ILogger logger)
            : base(logger, "Worker")
        {
            genderSvc = new GenderSvc(logger);
            userProfileSvc = new UserProfileSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Workers
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName + " " + m.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Workers
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName + " " + m.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public Worker GetServiceFilters()
        {
            Worker Worker = new Worker();
            return new Worker();
        }

        public IQueryable<Worker> Get()
        {
            return Db.Workers;
        }

        public Worker GetModel(Guid? userId)
        {
            return new Worker {
                Genders = genderSvc.GetComboItems(),
                Users =  userProfileSvc.GetWorkerComboItems()
            };

        }

        public Worker GetModelByUserId(Guid? userId)
        {
            return new Worker
            {
                Genders = genderSvc.GetComboItems(),
                Users = userProfileSvc.GetWorkerComboItems()
            };

        }

        public Worker GetModel(Guid id)
        {
            var Worker = this.Get(id);
            Worker.Genders = genderSvc.GetComboItems(Db);
            Worker.Users = userProfileSvc.GetWorkerComboItems(Db);
            return Worker;
        }        

        public Worker GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUserId(userId) : this.GetModel(id);
        }

        public Worker Get(Guid id)
        {
            return Db.Workers.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public JsTable<Worker> Get(PageDetail pageDetail)
        {
            var query = Db.Workers.AsQueryable();

            List<Expression<Func<Worker, bool>>> filters = new List<Expression<Func<Worker, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.FirstName.Contains(pageDetail.Search));


            Func<Worker, Worker> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_WorkerList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_WorkerList.AsQueryable();

            List<Expression<Func<vw_WorkerList, bool>>> filters = new List<Expression<Func<vw_WorkerList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.WorkerFullName.Contains(pageDetail.Search));


            Func<vw_WorkerList, vw_WorkerList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(Worker Worker)
        {
            if (string.IsNullOrWhiteSpace(Worker.FirstName))
                return new FailedResponse("Please provide a valid First Name.");
            if ((Worker.FirstName.Any(char.IsNumber)) || (Worker.FirstName.Any(char.IsSymbol)) || (Worker.FirstName.Any(char.IsPunctuation)))
                return new FailedResponse("First Name must be letter.");
            if ((Worker.FirstName.Any(char.IsWhiteSpace)))
                return new FailedResponse("Please remove white space from first name.");

            if (string.IsNullOrWhiteSpace(Worker.FatherName))
                return new FailedResponse("Please provide a valid Father Name.");
            if ((Worker.FatherName.Any(char.IsNumber)) || (Worker.FatherName.Any(char.IsSymbol)) || (Worker.FatherName.Any(char.IsPunctuation)))
                return new FailedResponse("Father Name must be letter.");
            if ((Worker.FatherName.Any(char.IsWhiteSpace)))
                return new FailedResponse("Please remove white space from father name.");

            if (string.IsNullOrWhiteSpace(Worker.GrandFatherName))
                return new FailedResponse("Please provide a valid Grand Father Name.");
            if ((Worker.GrandFatherName.Any(char.IsNumber)) || (Worker.GrandFatherName.Any(char.IsSymbol)) || (Worker.GrandFatherName.Any(char.IsPunctuation)))
                return new FailedResponse("Grand Father Name must be letter.");
            if ((Worker.GrandFatherName.Any(char.IsWhiteSpace)))
                return new FailedResponse("Please remove white space from grand father name.");

            if (string.IsNullOrWhiteSpace(Worker.BirthDate.ToString()))
                return new FailedResponse("Please provide a valid Brith Date.");

            if (string.IsNullOrWhiteSpace(Worker.GenderGuid.ToString()))
                return new FailedResponse("Please provide a valid Brith Date.");

            if (string.IsNullOrWhiteSpace(Worker.Address))
                return new FailedResponse("Please provide a valid Address.");

            if (string.IsNullOrWhiteSpace(Worker.MobilePhoneNumber.ToString()))
                return new FailedResponse("Please provide a valid Mobile Phone Number.");

            if (string.IsNullOrWhiteSpace(Worker.ReservedPhoneNumber.ToString()))
                return new FailedResponse("Please provide a valid Home Phone Number.");

            if (string.IsNullOrWhiteSpace(Worker.Salary.ToString()))
                return new FailedResponse("Please provide a valid Salary.");

            if (string.IsNullOrWhiteSpace(Worker.Experience))
                return new FailedResponse("Please provide a valid Experience.");
            if ((Worker.Experience.Any(char.IsSymbol)) || (Worker.Experience.Any(char.IsPunctuation)))
                return new FailedResponse("Experience must be letter and number.");

            if (string.IsNullOrWhiteSpace(Worker.EngagementDate.ToString()))
                return new FailedResponse("Please provide a valid Engagement Date.");

            if (string.IsNullOrWhiteSpace(Worker.LogInUserId.ToString()))
                return new FailedResponse("Please provide a valid User Name.");

            if (Worker.TableRowGuid == Guid.Empty)
            {
                var queryFirstName = Db.Workers.Where(r => r.FirstName == Worker.FirstName);
                var queryFatherName = Db.Workers.Where(r => r.FatherName == Worker.FatherName);
                var queryGrandFatherName = Db.Workers.Where(r => r.GrandFatherName == Worker.GrandFatherName);
                if ((queryFirstName.ToList().Count > 0) && (queryFatherName.ToList().Count > 0) && (queryGrandFatherName.ToList().Count > 0))
                    return new FailedResponse("A Worker Name provided has already been created. Please provide a different name.");
            }
            else
            {
                var queryFirstName = Db.Workers.Where(r => r.FirstName == Worker.FirstName && r.TableRowGuid != Worker.TableRowGuid);
                var queryFatherName = Db.Workers.Where(r => r.FatherName == Worker.FatherName && r.TableRowGuid != Worker.TableRowGuid);
                var queryGrandFatherName = Db.Workers.Where(r => r.GrandFatherName == Worker.GrandFatherName && r.TableRowGuid != Worker.TableRowGuid);
                if ((queryFirstName.ToList().Count > 0) && (queryFatherName.ToList().Count > 0) && (queryGrandFatherName.ToList().Count > 0))
                    return new FailedResponse("An Worker Name provided has already been created. Please provide a different name.");
            }

            return new SuccessResponse("");
        }

        public Response Add(Worker Worker, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Worker);
            if (!response.Success) return response;

            Worker.TableRowGuid = Guid.NewGuid();
            Worker.UserId = userid;

            return base.Add(Worker);
        }

        public Response Update(Worker Worker, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Worker);
            if (!response.Success) return response;

            Worker.UserId = userid;

            return base.Update(Worker);
        }

        public Response Delete(Worker Worker, Guid? userid)
        {
            Worker.UserId = userid;
            return base.Delete(Worker);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Worker() { TableRowGuid = id }, userid);
        }


    }
}
