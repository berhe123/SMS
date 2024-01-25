using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{

    public interface IReadOnlyEntityIntSvc<T, TD, TR> where T : class
    {
        IEnumerable<ComboItemInt> GetComboItems();
        IQueryable<TR> Get();
        T Get(long id);
        JsTable<TD> Get(PageDetail pageDetail);
    }
}