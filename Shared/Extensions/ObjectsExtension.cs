using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
namespace Shared.Extensions;

public static class ObjectsExtension
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
        
    public static IQueryable<T> Sort<T>
        (this IQueryable<T> query, string? sortColumn, string sortDirection)
    {
        if (sortColumn is null) return query;

        var propertyInfo = typeof(T).GetProperties();   
            
        if(propertyInfo.Any(x => x.Name == sortColumn))
        {
            if (sortDirection.EqualIgnoreCase("DESC")
                || sortDirection.EqualIgnoreCase("DESENDING"))
            {
                query = query.OrderBy($"{sortColumn} desc");
            }
            else
            {
                query = query.OrderBy($"{sortColumn}");
            }
        }

        return query;
    }
}