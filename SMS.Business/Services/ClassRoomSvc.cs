using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class ClassRoomSvc : EntityBaseSvc<ClassRoom>, IComboEntitySvc<ClassRoom, ClassRoom, ClassRoom>
    {

        public ClassRoomSvc(ILogger logger)
            : base(logger, "ClassRoom")
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.ClassRooms
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoomName
                    };

            return q.AsEnumerable();
        }      
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.ClassRooms
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoomName
                    };

            return q.AsEnumerable();
        }      

        public ClassRoom GetServiceFilters()
        {
            ClassRoom ClassRoom = new ClassRoom();
            return new ClassRoom();
        }
        public IQueryable<ClassRoom> Get()
        {
            return Db.ClassRooms;
        }
        public ClassRoom GetModel(Guid? userId)
        {
            return new ClassRoom();

        }
        public ClassRoom GetModelByUser(Guid? userId)
        {
            return new ClassRoom();

        }
        public ClassRoom GetModel(Guid id)
        {
            var ClassRoom = this.Get(id);
            return ClassRoom;
        }
        public ClassRoom GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public ClassRoom Get(Guid id)
        {
            return Db.ClassRooms.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public JsTable<ClassRoom> Get(PageDetail pageDetail)
        {
            var query = Db.ClassRooms.AsQueryable();

            List<Expression<Func<ClassRoom, bool>>> filters = new List<Expression<Func<ClassRoom, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.ClassRoomName.Contains(pageDetail.Search));


            Func<ClassRoom, ClassRoom> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }


        public Response Validate(ClassRoom ClassRoom)
        {
            if (string.IsNullOrWhiteSpace(ClassRoom.ClassRoomName))
                return new FailedResponse("Please provide a valid Class Room Name.");
            if ((ClassRoom.ClassRoomName.Any(char.IsSymbol)) || (ClassRoom.ClassRoomName.Any(char.IsPunctuation)))
                return new FailedResponse("Class Room Name must be letter and number.");
            if (ClassRoom.TableRowGuid == Guid.Empty)
            {
                var query = Db.ClassRooms.Where(r => r.ClassRoomName == ClassRoom.ClassRoomName);
                if (query.ToList().Count > 0)
                    return new FailedResponse("A Class Room Name provided has already been created. Please provide a different name.");
            }
            else
            {
                var query = Db.ClassRooms.Where(r => r.ClassRoomName == ClassRoom.ClassRoomName && r.TableRowGuid != ClassRoom.TableRowGuid);
                if (query.ToList().Count > 0)
                    return new FailedResponse("An Class Room Name provided has already been created. Please provide a different name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(ClassRoom ClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ClassRoom);
            if (!response.Success) return response;

            ClassRoom.TableRowGuid = Guid.NewGuid();
            ClassRoom.UserId = userid;

            return base.Add(ClassRoom);
        }
        public Response Update(ClassRoom ClassRoom, Guid? userid)
        {
            Response response = new Response();

            response = Validate(ClassRoom);
            if (!response.Success) return response;

            ClassRoom.UserId = userid;

            return base.Update(ClassRoom);
        }
        public Response Delete(ClassRoom ClassRoom, Guid? userid)
        {
            ClassRoom.UserId = userid;
            return base.Delete(ClassRoom);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new ClassRoom() { TableRowGuid = id }, userid);
        }


    }
}
