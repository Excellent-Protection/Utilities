
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;
using Z.EntityFramework.Plus;

namespace Utilities.DataAccess.Labor
{
    class EntityBaseRepository<T> : IEntityBaseRepository<T>
       where T : class, IEntityBase, new()
    {


        #region Properties and Attributes

        private LaborDbContext dbContext;

        protected virtual IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected virtual LaborDbContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }
        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion


        public virtual T Add(T entity)
        {
            if (entity.Id == null || entity.Id == new Guid())
            {
                entity.Id = Guid.NewGuid();
            }
            entity.ModifiedOn = entity.CreatedOn = DateTime.Now;
            entity.IsDeactivated = false;
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);

            ////to get sequences if exist 
            //var sequences = DbContext.Sequences.Where(a => a.EntityID == entity.classID);
            //if (sequences != null)
            //{

            //    foreach (var seq in sequences)
            //    {
            //        var prop = seq.FieldName;
            //        PropertyInfo pi = entity.GetType().GetProperty(prop);
            //        //set value of seq
            //        pi.SetValue(entity, seq.Prefix + seq.nextValue + seq.Postfix);
            //        //increase next value
            //        seq.nextValue = (int.Parse(seq.nextValue) + seq.increasing).ToString().PadLeft(seq.nextValue.Length, '0');
            //    }
            //}
            dbEntityEntry.State = EntityState.Added;
            return DbContext.Set<T>().Add(entity);
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entites)
        {
            foreach (var item in entites)
            {
                if (item.Id == new Guid())
                {
                    item.Id = Guid.NewGuid();
                }
            }
            return DbContext.Set<T>().AddRange(entites);
        }

        public virtual void Delete(object id)
        {
            T entitiyToDelete = DbContext.Set<T>().Find(new Guid(id.ToString()));
            if (entitiyToDelete != null)
                Delete(entitiyToDelete);
        }

        public virtual void Delete(T entity)
        {
            //DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            //var dbSet = DbContext.Set<T>();
            //if (dbEntityEntry.State == EntityState.Detached)
            //{
            //    dbSet.Attach(entity);
            //}
            //dbSet.Remove(entity);
            //dbEntityEntry.State = EntityState.Deleted;

            var dbSet = DbContext.Set<T>();
            dbSet.Attach(entity);
            entity.IsDeleted = true;
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Entry<T>(entity).Property(a => a.IsDeleted).IsModified = true;
            DbContext.SaveChanges();

        }

        public virtual void Activate(Guid id)
        {
            var entity = new T() { Id = id, IsDeactivated = false };
            var dbSet = DbContext.Set<T>();
            dbSet.Attach(entity);
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Entry<T>(entity).Property(a => a.IsDeactivated).IsModified = true;
            DbContext.SaveChanges();
        }

        public virtual void Deactivate(Guid id)
        {
            var entity = new T() { Id = id, IsDeactivated = true };
            var dbSet = DbContext.Set<T>();
            dbSet.Attach(entity);
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Entry<T>(entity).Property(a => a.IsDeactivated).IsModified = true;
            DbContext.SaveChanges();
        }

        public virtual void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            entity.ModifiedOn = DateTime.Now;
            DbContext.Set<T>().Attach(entity);
            //Ensure only modified fields are updated.
            var dbEntityEntry = DbContext.Entry(entity);
            if (updatedProperties.Any())
            {
                //update explicitly mentioned properties
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                //no items mentioned, so find out the updated entries
                foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
                {
                    var original = dbEntityEntry.GetDatabaseValues().GetValue<object>(property);
                    var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                    if (current != null && original != current)
                        dbEntityEntry.Property(property).IsModified = true;
                }
            }
        }

        // ================================================ //

        public virtual IEnumerable<T> List
        {
            get
            {
                return GetAll().ToList();
            }
        }

        //change parameter from id to guid
        public virtual T Single(Guid id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual T Single(Expression<Func<T, bool>> predicate)
        {
            T t = DbContext.Set<T>().Where(a => a.IsDeleted == false).FirstOrDefault(predicate);
            return t;
        }

        public virtual T Single(Guid id, Expression<Func<T, object>>[] includeProperties)
        {
            T t = Find(m => m.Id == id, includeProperties).Where(a => a.IsDeleted == false).FirstOrDefault();
            return t;
        }

        public virtual T Single(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeProperties)
        {
            T t = Find(predicate, includeProperties).Where(a => a.IsDeleted == false).FirstOrDefault();
            return t;

        }


        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate).Where(a=>a.IsDeleted==false);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.IncludeFilter(includeProperty);
                }
            }
            return query;
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            //add condition is not deleted
            return Find(null, includeProperties).Where(a => a.IsDeleted == false);
        }

        public virtual IQueryable<T> GetBySqlQuery(string query, params object[] parameters)
        {
            return DbContext.Set<T>().SqlQuery(query, parameters).AsQueryable<T>().Where(a => a.IsDeleted == false);
        }




    }

}
