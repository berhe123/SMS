using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Core;
using SMS.Entities;
using NLog;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class EntityBaseIntSvc<T> : IEntityBaseSvc<T> where T : class
    {
        protected readonly SMSEntities Db = new SMSEntities(false);
        protected readonly ILogger Logger;
        protected readonly string TypeName;

        public EntityBaseIntSvc(ILogger logger)
        {
            this.Logger = logger;
            this.TypeName = typeof(T).Name;
        }

        public EntityBaseIntSvc(ILogger logger, SMSEntities db)
        {
            this.Logger = logger;
            this.TypeName = typeof(T).Name;
            this.Db = db;
        }

        public EntityBaseIntSvc(ILogger logger, string typeNameCaption)
        {
            this.Logger = logger;
            this.TypeName = typeNameCaption; // typeof(T).Name;
        }

        public EntityBaseIntSvc(ILogger logger, SMSEntities db, string typeNameCaption)
        {
            this.Logger = logger;
            this.TypeName = typeNameCaption; // typeof(T).Name;
            this.Db = db;
        }

        public virtual Response Add(T entity)
        {
            try
            {
                Db.Set<T>().Add(entity);
                Db.SaveChanges();
                return new SuccessResponse(TypeName + " Registration Succeded!");
            }
            catch(Exception ex)
            {
                Logger.Error(TypeName + " Registration Failed", ex);
                return new FailedResponse(TypeName + " Registration Failed. " + ex.Message);
            }
        }

        public virtual Response Update(T entity)
        {
            try
            {
                Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return new SuccessResponse("Updating " + TypeName + " Succeded!");
            }
            catch (Exception ex)
            {
                Logger.Error("Updating " + TypeName + " Failed", ex);
                return new FailedResponse("Updating " + TypeName + " Failed. " + ex.Message);
            }
        }

        public virtual Response Delete(T entity)
        {
            try
            {
                
               Db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;

                Db.SaveChanges();
                return new SuccessResponse("Delete" + TypeName + " Succeded!");
            }
            catch (Exception ex)
            {
                Logger.Error("Delete " + TypeName + " Failed", ex);
                return new FailedResponse("Delete " + TypeName + " Failed. " + ex.Message);
            }
        }
        public virtual Response Delete(List<T> entity)
        {
            try
            {
                for (int i = 0; i < entity.Count;i++ )
                    Db.Entry(entity[i]).State = System.Data.Entity.EntityState.Deleted;
                Db.SaveChanges();
                return new SuccessResponse("Delete" + TypeName + " Succeded!");
            }
            catch (Exception ex)
            {
                Logger.Error("Delete " + TypeName + " Failed", ex);
                return new FailedResponse("Delete " + TypeName + " Failed. " + ex.Message);
            }
        }
    }
}
