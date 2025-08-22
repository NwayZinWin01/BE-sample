using Infrastructure.common;
using Infrastructure.Enumerations;
using System.Linq.Expressions;

namespace Infrastructure.utilities
{
    public class QueryOptions<TEntity> where TEntity : BaseEntity
    {
        public int legth { get; set; } = 0;
        public int Page { get; set; } = 0;
        public int RecordPerPage { get; set; } = 10;
        public string? SearchValue { get; set; }
        public string? SortColumnName { get; set; }
        public List<string>? SortColumnsName { get; set; }
        public QueryOptions()
        {
            SortOrder = SortOrder.ASC;
            SortBy = new List<Func<TEntity, object>>();
        }

        public Expression<Func<TEntity, bool>>? FilterBy { get; set; }
        public List<Func<TEntity, object>> SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}