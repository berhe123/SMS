using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{

    public interface IPageSvc : IEntityBaseSvc<Page>
    {
        IQueryable<Page> Get();
        Page Get(Guid pageCode);
        IEnumerable<ComboItem<Guid, int>> GetComboItems(Guid deFormCode);
    }
}