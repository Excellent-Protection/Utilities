using Models.Labor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace Utilities.DataAccess.Labor
{

        /// <summary>
        /// Generic Unit Of Work
        /// Now with the generic Repository, we will create a generic Unit of work class 
        /// that will work with this generic repository.
        /// This unit of work class will check if the repository class for a particular type has been create already,
        /// same instance will be returned. else a new instance will be returned.
        /// </summary>
        public class UnitOfWork : Disposable, IUnitOfWork
        {
            private readonly IDbFactory dbFactory;
            private LaborDbContext dbContext;

            public UnitOfWork(IDbFactory dbFactory)
            {
                this.dbFactory = dbFactory;
            }

            public LaborDbContext DbContext
            {
                get { return dbContext ?? (dbContext = dbFactory.Init()); }
            }

            /// <summary>
            /// I made it for not Making Another Instance like Session i wanna Save the Instance state 
            /// this Dictionary have all Repo Instances in king Of List I must check if this instance is Already Created 
            /// or i should Create new Instance of the Repo 
            /// and Return it To Repo Caller 
            /// </summary>
            protected Dictionary<Type, object> repositories = new Dictionary<Type, object>();
            public virtual IEntityBaseRepository<T> Repository<T>() where T : class, IEntityBase, new()
            {
                object repository;
                if (!repositories.TryGetValue(typeof(T), out repository))
                {
                    repository = new EntityBaseRepository<T>(dbFactory); // ToDo >> make it with dependency injection
                    repositories.Add(typeof(T), repository);

                }
                return (repository as IEntityBaseRepository<T>);
            }
            public DataTable ExcuteSqlQuery(string query, params object[] parameters)
            {
                var connection = ConfigurationManager.ConnectionStrings["LaborDbContext"].ConnectionString;
                var dt = new DataTable();

                using (var com = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                {
                    var sqlParams = (IEnumerable<SqlParameter>)parameters.ToList().First();
                    foreach (var param in sqlParams)
                    {
                        com.SelectCommand.Parameters.Add(param);

                    }
                    com.Fill(dt);
                }
                return dt;
            }



            public virtual int SaveChanges()
            {
                return DbContext.SaveChanges();
            }

            protected override void DisposeCore()
            {
                if (DbContext != null)
                    DbContext.Dispose();

                base.DisposeCore();
            }

        }
}
