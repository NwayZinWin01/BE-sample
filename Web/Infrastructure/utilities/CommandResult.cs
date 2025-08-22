using Infrastructure.common;

namespace Infrastructure.utilities
{
    public class CommandResult<TEntity> 
    {
        public int id { get; set; } = 0;
        public int code { get; set; } = 0;
        public bool success { get; set; } = false;
        public TEntity? entity { get; set; }
        public List<string> messages { get; set; }
        public CommandResult()
        {
            messages = new List<string>();
        }
    }
}