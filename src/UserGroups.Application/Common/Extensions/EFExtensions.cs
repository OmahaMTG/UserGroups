using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace UserGroups.Application.Common.Extensions
{
    public static class EfExtensions
    {
        public static void RemoveWhere<T>(this DbSet<T> dbSet, Func<T, bool> condition) where T : class
        {
            var range = dbSet.Where(condition);
            dbSet.RemoveRange(range);
            return;
        }
    }
}
