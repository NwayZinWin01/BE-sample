﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class DatabaseContext : DbContext, IDBContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options) { }
        public DatabaseFacade GetDatabase()
        {
            return base.Database;
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public void SetAddedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void SetDeletedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void SetModifedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }
        // configure models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure model from Assembly
            //using EntityConfiguratuion.IEntityTypeConfiguration<TEntity>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
