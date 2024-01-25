using System;
using System.Collections.Generic;
using System.Linq;
using SMS.Entities;

namespace SMS.Core
{
    public interface IComboEntityIntSvc<T, TD, TR> : IEntityIntSvc<T, TD, TR> where T : class
    {
        IEnumerable<ComboItemInt> GetComboItems();
    }
}