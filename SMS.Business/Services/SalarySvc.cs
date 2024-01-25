using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class SalarySvc : EntityBaseSvc<Salary>, IComboEntitySvc<Salary, Salary, Salary>
    {
        readonly TeacherSvc teacherSvc;
        readonly MonthSvc monthSvc;
        public SalarySvc(ILogger logger)
            : base(logger, "Salary")
        {
            teacherSvc = new TeacherSvc(logger);
            monthSvc = new MonthSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Salarys
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Teacher.FirstName+" "+m.Teacher.FatherName+" "+m.Teacher.GrandFatherName+" takes "+m.Month.MonthName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Salarys
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Teacher.FirstName + " " + m.Teacher.FatherName + " " + m.Teacher.GrandFatherName + " takes " + m.Month.MonthName
                    };

            return q.AsEnumerable();
        }

        public Salary GetServiceFilters()
        {
            Salary Salary = new Salary();
            return new Salary();
        }

        public IQueryable<Salary> Get()
        {
            return Db.Salarys;
        }

        public Salary GetModel()
        {
            return new Salary
            {
                Teachers = teacherSvc.GetComboItems(),
                Months=monthSvc.GetComboItems()
            };

        }

        public Salary GetModel(Guid id)
        {
            var Salary = this.Get(id);

            Salary.Teachers = teacherSvc.GetComboItems(Db);
            Salary.Months = monthSvc.GetComboItems(Db);

            return Salary;
        }

        public Salary GetModel(Guid id, Guid userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public Salary Get(Guid id)
        {
            return Db.Salarys.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Salary> Get(PageDetail pageDetail)
        {
            var query = Db.Salarys.AsQueryable();

            List<Expression<Func<Salary, bool>>> filters = new List<Expression<Func<Salary, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Teacher.FirstName+m.Teacher.FatherName+m.Teacher.GrandFatherName).Contains(pageDetail.Search));


            Func<Salary, Salary> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_SalaryList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_SalaryList.AsQueryable();

            List<Expression<Func<vw_SalaryList, bool>>> filters = new List<Expression<Func<vw_SalaryList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Salary.Contains(pageDetail.Search));


            Func<vw_SalaryList, vw_SalaryList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public Response Validate(Salary Salary)
        {
            if (string.IsNullOrWhiteSpace(Salary.TeacherGuid.ToString()))
                return new FailedResponse("Please provide a valid Teacher Name.");

            if (string.IsNullOrWhiteSpace(Salary.MonthGuid.ToString()))
                return new FailedResponse("Please provide a valid Month Name.");

            if (string.IsNullOrWhiteSpace(Salary.RecieveDate.ToString()))
                return new FailedResponse("Please provide a valid Date.");

            //if (char.MaxValue.Equals(5))
            //{
            //    return new FailedResponse("Please provide a letter only for First Name.");
            //}

            if (Salary.TableRowGuid == Guid.Empty)
            {
                var queryTeacher = Db.Salarys.Where(r => r.TeacherGuid == Salary.TeacherGuid);
                var queryMonth = Db.Salarys.Where(r => r.MonthGuid == Salary.MonthGuid);
                var queryRecieve = Db.Salarys.Where(r => r.RecieveDate == Salary.RecieveDate);
                if ((queryTeacher.ToList().Count > 0) && (queryMonth.ToList().Count > 0) && (queryRecieve.ToList().Count > 0))
                    return new FailedResponse("A Salary provided has already been created. Please provide a different Salary File.");
            }
            else
            {
                var queryTeacher = Db.Salarys.Where(r => r.TeacherGuid == Salary.TeacherGuid && r.TableRowGuid != Salary.TableRowGuid);
                var queryMonth = Db.Salarys.Where(r => r.MonthGuid == Salary.MonthGuid && r.TableRowGuid != Salary.TableRowGuid);
                var queryRecieve = Db.Salarys.Where(r => r.RecieveDate == Salary.RecieveDate && r.TableRowGuid != Salary.TableRowGuid);
                if ((queryTeacher.ToList().Count > 0) && (queryMonth.ToList().Count > 0) && (queryRecieve.ToList().Count > 0))
                    return new FailedResponse("A Salary provided has already been created. Please provide a different Salary File.");
            }

            return new SuccessResponse("");
        }

        public Response Add(Salary Salary, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Salary);
            if (!response.Success) return response;

            Salary.TableRowGuid = Guid.NewGuid();
            Salary.UserId = userid;           

            return base.Add(Salary);
        }

        public Response Update(Salary Salary, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Salary);
            if (!response.Success) return response;

            Salary.UserId = userid;
            
             return base.Update(Salary);            
        }

        public Response Delete(Salary Salary, Guid? userid)
        {
            Salary.UserId = userid;
            return base.Delete(Salary);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Salary() { TableRowGuid = id }, userid);
        }


    }
}
