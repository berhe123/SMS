using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{

    public interface IReadOnlyEntitySvc<T, TD, TR> where T : class
    {
        IEnumerable<ComboItem> GetComboItems();
        IQueryable<TR> Get();
        T Get(Guid id);
        JsTable<TD> Get(PageDetail pageDetail);
    }
}