using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Core
{
    public interface IEntityBaseSvc<in T> where T : class
    {
        Response Add(T entity);
        Response Update(T entity);
        Response Delete(T entity);
    }
}
