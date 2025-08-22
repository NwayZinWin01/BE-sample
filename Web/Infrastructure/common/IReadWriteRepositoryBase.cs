using Infrastructure.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.common
{
    public interface IReadWriteRepositoryBase<TEntity> where TEntity : BaseEntity
    {

        TEntity? Get(int id);
        List<TEntity> GetAll();
        PagedResult<TEntity> GetPagedResults(QueryOptions<TEntity> option);
        CommandResult<TEntity> Save(TEntity entity);
        CommandResult<List<TEntity>> SaveList(List<TEntity> entity);
        CommandResult<TEntity> Remove(TEntity entity);
    }
}
