using Infrastructure.common;
using Infrastructure.Enumerations;
using Infrastructure.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class ReadWriteRepositoryBase<TEntity> : IReadWriteRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly IDBContext _context;
        public ReadWriteRepositoryBase(IDBContext context)
        {
            _context = context;
        }
        public TEntity? Get(int id)
        {
            TEntity? entity = _context.Set<TEntity>().Where(x => x.id == id && x.deleted == false).FirstOrDefault();
            return entity;
        }

        public List<TEntity> GetAll()
        {
            List<TEntity> entities = _context.Set<TEntity>().Where(x => x.deleted == false).ToList();
            return entities;
        }

        public PagedResult<TEntity> GetPagedResults(QueryOptions<TEntity> option)
        {
            var result = new PagedResult<TEntity>();
            try
            {
                if (option == null)
                {
                    result.success = false;
                    result.messages.Add("Invalid query options.");
                    return result;
                }
                else
                {
                    if (option.Page <= 0)
                        option.Page = 1;

                    int skip = option.Page - 1 * option.RecordPerPage;

                    IQueryable<TEntity> query = _context.Set<TEntity>().Where(x => !x.deleted);
                    if (option.FilterBy != null)
                    {
                        query = query.Where(option.FilterBy);
                    }

                    result.total = query.Count();
                    var datalist = query.ToList();

                    if (option.SortBy != null && option.SortBy.Count > 0)
                    {
                        IOrderedEnumerable<TEntity> orderedData;

                        if (option.SortOrder == SortOrder.ASC)
                        {
                            orderedData = datalist.OrderBy(option.SortBy[0]);
                            for (int i = 1; i < option.SortBy.Count; i++)
                                orderedData = orderedData.ThenBy(option.SortBy[i]);
                        }
                        else
                        {
                            orderedData = datalist.OrderByDescending(option.SortBy[0]);
                            for (int i = 1; i < option.SortBy.Count; i++)
                                orderedData = orderedData.ThenByDescending(option.SortBy[i]);
                        }


                        result.data = option.legth < 0
                            ? orderedData.ToList()
                            : orderedData.Skip(skip).Take(option.RecordPerPage).ToList();
                    }
                    else
                    {

                        result.data = option.legth < 0
                            ? datalist
                            : datalist.Skip(skip).Take(option.RecordPerPage).ToList();
                    }


                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages.Add("Something went wrong while fetching paged data.");
                result.messages.Add(ex.Message);
            }
            return result;

        }

        public CommandResult<TEntity> Remove(TEntity entity)
        {
            CommandResult<TEntity> result = new CommandResult<TEntity>();
            try
            {
                entity.deleted = true;
                _context.Set<TEntity>().Add(entity);
                _context.SetModifedState(entity);
                SaveChanges();
                result.success = true;
                result.messages.Add(Constant.DeleteSuccessMessage);
                result.id = (int)entity.GetType().GetProperty("id").GetValue(entity);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages.Add(ex.Message);
            }

            return result;
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        public CommandResult<TEntity> Save(TEntity entity)
        {
            CommandResult<TEntity> result = new CommandResult<TEntity>();
            try
            {
                entity.deleted = false;
                if (entity.id > 0)
                {
                    entity.modified_date = DateTime.Now;
                    _context.Set<TEntity>().Add(entity);
                    _context.SetModifedState(entity);
                }
                else
                {
                    entity.created_date = DateTime.Now;
                    entity.modified_date = DateTime.Now;
                    _context.Set<TEntity>().Add(entity);
                    _context.SetAddedState(entity);

                }
                SaveChanges();
                result.success = true;
                result.messages.Add(Constant.SaveSucessMessage);
                result.id = (int)entity.GetType().GetProperty("id").GetValue(entity);

            }
            catch (Exception er)
            {
                result.success = false;
                result.messages.Add(er.Message);
            }
            return result;
        }

        public CommandResult<List<TEntity>> SaveList(List<TEntity> entityList)
        {
            CommandResult<List<TEntity>> result = new CommandResult<List<TEntity>>();
            result.entity = new List<TEntity>();

            try
            {
                foreach (var entity in entityList)
                {
                    entity.deleted = false;

                    if ((int)entity.GetType().GetProperty("id").GetValue(entity) > 0)
                    {
                        entity.modified_date = DateTime.Now;
                        _context.Set<TEntity>().Add(entity);
                        _context.SetModifedState(entity);
                    }
                    else
                    {
                        entity.created_date = DateTime.Now;
                        entity.modified_date = DateTime.Now;
                        _context.Set<TEntity>().Add(entity);
                        _context.SetAddedState(entity);
                    }

                    result.entity.Add(entity);
                }

                SaveChanges();
                result.success = true;
                result.messages.Add(Constant.SaveSucessMessage);
                result.id = result.entity.Count > 0
                    ? (int)result.entity[0].GetType().GetProperty("id").GetValue(result.entity[0])
                    : 0;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages.Add(ex.Message);
            }

            return result;
        }
    }
}

