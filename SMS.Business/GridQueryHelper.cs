using System;
using System.Linq;
using System.Linq.Expressions;
using SMS.Core;
using System.Collections;
using System.Collections.Generic;
using SMS.Entities;

namespace SMS.Business
{
    public class GridQueryHelper
    {
        public static JsTable<TR> JsTable<T, TR>(IQueryable<T> query, PageDetail pageDetail, List<Expression<Func<T, bool>>> filters, Func<T, TR> result)
        {
            int iTotalRecords = query.Count();
            int iTotalDisplayRecords = iTotalRecords;

            filters.ForEach(filter => query = query.Where(filter));

            iTotalDisplayRecords = query.Count();
            if(!string.IsNullOrEmpty(pageDetail.SortColumn))
                query = query.OrderBy(pageDetail.SortColumn, pageDetail.IsAsc);
            query = query.Skip(pageDetail.DisplayStart).Take(pageDetail.DisplayLength);

            return new JsTable<TR>()
            {
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = query.Select(result).ToList<TR>()
            };
        }

        public static JsTable<TR> JsTable<T, TR>(List<T> query, PageDetail pageDetail, Func<T, TR> result)
        {
            int iTotalRecords = query.Count();
            int iTotalDisplayRecords = iTotalRecords;


            iTotalDisplayRecords = query.Count();

            //query = query.OrderBy(pageDetail.SortColumn, pageDetail.IsAsc)
            //    .Skip(pageDetail.DisplayStart).Take(pageDetail.DisplayLength);

            return new JsTable<TR>()
            {
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = query.Select(result).ToList<TR>()
            };
        }
    }
}