using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{
    public interface IComboEntitySvc<T, TD, TR> : IEntitySvc<T, TD, TR> where T : class
    {
        IEnumerable<ComboItem> GetComboItems();
    }
}