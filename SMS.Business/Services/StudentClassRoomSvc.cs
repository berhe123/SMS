using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class StudentClassRoomSvc : EntityBaseSvc<StudentClassRoom>, IComboEntitySvc<StudentClassRoom, StudentClassRoom, StudentClassRoom>
    {
        readonly StudentSvc studentSvc;
        readonly ClassRoomSvc classroomSvc;
        public StudentClassRoomSvc(ILogger logger)
            : base(logger, "StudentClassRoom")
        {
            studentSvc = new StudentSvc(logger);
            classroomSvc = new ClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.StudentClassRooms
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoom.ClassRoomName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.StudentClassRooms
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoom.ClassRoomName
                    };

            return q.AsEnumerable();
        }       

        public StudentClassRoom GetServiceFilters()
        {
            StudentClassRoom StudentClassRoom = new StudentClassRoom();
            return new StudentClassRoom();
        }
        public IQueryable<StudentClassRoom> Get()
        {
            return Db.StudentClassRooms;
        }
        public StudentClassRoom GetModel(Guid? userId)
        {
            return new StudentClassRoom
            {
                Students = studentSvc.GetComboItems(),
                ClassRooms = classroomSvc.GetComboItems()
            };
        }
        public StudentClassRoom GetModelByUser(Guid? userId)
        {
            return new StudentClassRoom
            {
                Students = studentSvc.GetComboItems(),
                ClassRooms = classroomSvc.GetComboItems()
            };
        }
        public StudentClassRoom GetModel(Guid id)
        {
            var StudentClassRoom = this.Get(id);

            StudentClassRoom.Students = studentSvc.GetComboItems(Db);
            StudentClassRoom.ClassRooms = classroomSvc.GetComboItems(Db);

            return StudentClassRoom;
        }
        public StudentClassRoom GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(id) : this.GetModel(id);
        }
        public StudentClassRoom Get(Guid id)
        {
            return Db.StudentClassRooms.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<StudentClassRoom> Get(PageDetail pageDetail)
        {
            var query = Db.StudentClassRooms.AsQueryable();

            List<Expression<Func<StudentClassRoom, bool>>> filters = new List<Expression<Func<StudentClassRoom, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.ClassRoom.ClassRoomName+m.Student.FirstName+m.Student.FatherName+m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<StudentClassRoom, StudentClassRoom> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_StudentClassRoomList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_StudentClassRoomList.AsQueryable();

            List<Expression<Func<vw_StudentClassRoomList, bool>>> filters = new List<Expression<Func<vw_StudentClassRoomList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.StudentClassRoom.Contains(pageDetail.Search));


            Func<vw_StudentClassRoomList, vw_StudentClassRoomList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(StudentClassRoom StudentClassRoom)
        {
            if (string.IsNullOrWhiteSpace(StudentClassRoom.ClassRoomGuid.ToString()))
                return new FailedResponse("Please provide a valid class room name.");

            if (string.IsNullOrWhiteSpace(StudentClassRoom.StudentGuid.ToString()))
                return new FailedResponse("Please provide a valid student name.");

            //if (char.MaxValue.Equals(5))
            //{
            //    return new FailedResponse("Please provide a letter only for First Name.");
            //}

            //if (StudentClassRoom.TableRowGuid == Guid.Empty)
            //{
            //    var query = Db.StudentClassRooms.Where(r => r.StudentClassRoomName == StudentClassRoom.StudentClassRoomName);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("A StudentClassRoom Name provided has already been created. Please provide a different name.");
            //}
            //else
            //{
            //    var query = Db.StudentClassRooms.Where(r => r.StudentClassRoomName == StudentClassRoom.StudentClassRoomName && r.TableRowGuid != StudentClassRoom.TableRowGuid);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("An StudentClassRoom Name provided has already been created. Please provide a different name.");
            //}

            return new SuccessResponse("");
        }
        public Response Add(StudentClassRoom StudentClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(StudentClassRoom);
            if (!response.Success) return response;

            StudentClassRoom.TableRowGuid = Guid.NewGuid();
            StudentClassRoom.UserId = userid;            

            return base.Add(StudentClassRoom);
        }
        public Response Update(StudentClassRoom StudentClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(StudentClassRoom);
            if (!response.Success) return response;

            StudentClassRoom.UserId = userid;
            
             return base.Update(StudentClassRoom);            
        }
        public Response Delete(StudentClassRoom StudentClassRoom, Guid? userid)
        {
            StudentClassRoom.UserId = userid;
            return base.Delete(StudentClassRoom);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new StudentClassRoom() { TableRowGuid = id }, userid);
        }


    }
}
