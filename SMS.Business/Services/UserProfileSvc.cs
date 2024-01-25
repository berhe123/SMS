using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class UserProfileSvc : ReadOnlyEntityBaseSvc<UserProfile>, IReadOnlyEntitySvc<UserProfile, UserProfile, UserProfile>
    {

        public UserProfileSvc(ILogger logger)
            : base(logger)
        {

        }

        
        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.UserProfiles
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.UserProfiles
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetTeacherComboItems()
        {

            Guid TeacherRoleGuid = Guid.Parse("65468a1d-f9f9-4377-85ab-0e0f47df3266"); // Hard coded Teacher role Guid
            var q = from m in Db.UserProfiles.Include("security_Roles")
                    where m.security_Roles.Select(r => r.RoleId).Contains(TeacherRoleGuid) 
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetTeacherComboItems(SMSEntities db)
        {

            Guid TeacherRoleGuid = Guid.Parse("65468a1d-f9f9-4377-85ab-0e0f47df3266");  // Hard coded Teacher role Guid
            var q = from m in db.UserProfiles.Include("security_Roles")
                    where m.security_Roles.Select(r => r.RoleId).Contains(TeacherRoleGuid)
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetWorkerComboItems()
        {

            Guid WorkerRoleGuid = Guid.Parse("59e2884b-b3b4-4b16-824e-af933003eb0a"); // Hard coded Worker role Guid
            var q = from m in Db.UserProfiles.Include("security_Roles")
                    where m.security_Roles.Select(r => r.RoleId).Contains(WorkerRoleGuid)
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetWorkerComboItems(SMSEntities db)
        {

            Guid WorkerRoleGuid = Guid.Parse("59e2884b-b3b4-4b16-824e-af933003eb0a"); // Hard coded Worker role Guid
            var q = from m in Db.UserProfiles.Include("security_Roles")
                    where m.security_Roles.Select(r => r.RoleId).Contains(WorkerRoleGuid)
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetStudentComboItems()
        {

            Guid StudentRoleGuid = Guid.Parse("fd89759a-c6e8-43e9-b4c5-ed28b66345ed"); // Hard coded Student role Guid
            var q = from m in Db.UserProfiles.Include("security_Roles")
                    where m.security_Roles.Select(r => r.RoleId).Contains(StudentRoleGuid)
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetStudentComboItems(SMSEntities db)
        {

            Guid StudentRoleGuid = Guid.Parse("fd89759a-c6e8-43e9-b4c5-ed28b66345ed");  // Hard coded Student role Guid
            var q = from m in db.UserProfiles.Include("security_Roles")
                    where m.security_Roles.Select(r => r.RoleId).Contains(StudentRoleGuid)
                    select new ComboItem()
                    {
                        Value = m.UserId,
                        Text = m.UserName
                    };

            return q.AsEnumerable();
        }        

        public UserProfile GetServiceFilters()
        {
            UserProfile UserProfile = new UserProfile();
            return new UserProfile();
        }

        public IQueryable<UserProfile> Get()
        {
            return Db.UserProfiles;
        }

        public UserProfile GetModel()
        {
            return new UserProfile();

        }

        public UserProfile GetModel(Guid id)
        {
            var UserProfile = this.Get(id);           
            return UserProfile;
            
        }

        public UserProfile GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public UserProfile Get(Guid id)
        {
            return Db.UserProfiles.SingleOrDefault(e => e.UserId == id);
        }

        //public UserProfile Get(string userName)
        //{
        //    return Db.UserProfiles.SingleOrDefault(e => e.UserName == userName);
        //}

        public JsTable<UserProfile> Get(PageDetail pageDetail)
        {
            var query = Db.UserProfiles.AsQueryable();

            List<Expression<Func<UserProfile, bool>>> filters = new List<Expression<Func<UserProfile, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.UserName.Contains(pageDetail.Search));


            Func<UserProfile, UserProfile> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public List<security_Roles> GetUserRoles(Guid userId)
        {
            UserProfile user = Db.UserProfiles.Include("security_Roles").Where(u => u.UserId == userId).SingleOrDefault();
            return user.security_Roles.ToList();
        }

        public List<security_Roles> GetUserRoles(string userName)
        {
            UserProfile user = Db.UserProfiles.Include("security_Roles").Where(u => u.UserName == userName).SingleOrDefault();
            return user.security_Roles.ToList();
        }

        public Response SaveUserRoles(UserRole userRole)
        {
            UserProfile currentUser = (from c in Db.UserProfiles.Include("security_Roles")
                                       where c.UserId == userRole.UserId
                                       select c).SingleOrDefault();

            var currentUserRoles = (from r in currentUser.security_Roles
                                    select r).ToArray();

            var currentUserRoleIds = (from r in currentUser.security_Roles
                                    select r.RoleId).ToArray();

            try
            {
                //Roles to delete
                for (int i = 0; i < currentUserRoles.Count(); i++)
                {
                    if (!userRole.SelectedUserRoles.Contains(currentUserRoles[i].RoleId))
                    {
                        currentUser.security_Roles.Remove(currentUserRoles[i]);
                        Db.SaveChanges();
                    }

                }
                // Roles to save
                for (int i = 0; i < userRole.SelectedUserRoles.Count(); i++)
                {
                    if (!currentUserRoleIds.Contains(userRole.SelectedUserRoles[i]))
                    {
                        Guid roleguid = userRole.SelectedUserRoles[i];

                        security_Roles role = (from r in Db.security_Roles
                                               where r.RoleId == roleguid
                                           select r).SingleOrDefault();

                        currentUser.security_Roles.Add(role);
                        Db.SaveChanges();
                        
                    }
                }

                return new SuccessResponse("Role Data Saved Successfully");
            }
            catch (Exception ex)
            {
                return new FailedResponse(ex.Message);
            }
        }
    }
}
