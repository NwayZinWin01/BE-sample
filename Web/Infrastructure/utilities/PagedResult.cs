using Infrastructure.common;

namespace Infrastructure.utilities
{
    public class PagedResult<TEntity> 
    {
        public List<TEntity> data { get; set; }
        public int total { get; set; }
        public string? filterBy { get; set; }
        public bool success { get; set; }
        public List<string> messages { get; set; }
        public PagedResult()
        {
            data = new List<TEntity>();
            messages = new List<string>();
        }
    }
}