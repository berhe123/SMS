using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{

    public interface IEntityIntSvc<T, TD, TR> : IEntityBaseSvc<T> where T : class
    {
        IQueryable<TR> Get();
        T Get(long id);
        JsTable<TD> Get(PageDetail pageDetail);
        T GetModel();
        T GetModel(long id);
        Response Add(T entity, Guid? userId);
        Response Update(T entity, Guid? userId);
        Response Delete(T entity, Guid? userId);
        Response Delete(long id, Guid? userId);
    }
}