using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{

    public interface IEntitySvc<T, TD, TR> : IEntityBaseSvc<T> where T : class
    {
        IQueryable<TR> Get();
        T Get(Guid id);
        JsTable<TD> Get(PageDetail pageDetail);
        T GetModel(Guid? userId);
        T GetModel(Guid id);
        T GetModel(Guid id, Guid? userId);
        Response Add(T entity, Guid? userId);
        Response Update(T entity, Guid? userId);
        Response Delete(T entity, Guid? userId);
        Response Delete(Guid id, Guid? userId);
    }
}