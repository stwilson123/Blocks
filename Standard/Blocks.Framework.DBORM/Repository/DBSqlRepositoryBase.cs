using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Data;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Entity;
using Blocks.Framework.DBORM.Linq;
using Blocks.Framework.Security;
using Blocks.Framework.Services;
using Z.EntityFramework.Plus;
using Blocks.Framework.Reflection.Extensions;
using Blocks.Framework.Data;
using Blocks.Framework.Data.Entity;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using DynamicQueryableExtensions = System.Linq.Dynamic.Core.DynamicQueryableExtensions;
using Blocks.Framework.Environment.Extensions;

namespace Blocks.Framework.DBORM.Repository
{
    public class DBSqlRepositoryBase<TEntity> : DBSqlRepositoryBase<BlocksDbContext, TEntity, string>
        where TEntity : Data.Entity.Entity
    {
        protected DbSetContext<BlocksDbContext> Tables {
            get {
                if(tables == null)
                    tables = new DbSetContext<BlocksDbContext>(this.Context);
                return tables;
            }
        }

        DbSetContext<BlocksDbContext> tables;

        public IUserContext UserContext { set; get; }
        public IClock Clock { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DBSqlRepositoryBase(DBContext.IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {

        }

        public IDbLinqQueryable<TEntity> GetContextTable()
        {
            return GetContextTableIncluding();
        }

        public IDbLinqQueryable<TEntity> GetContextTableIncluding(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return new DefaultLinqQueryable<TEntity>(query, Context) { };
        }


        public override TEntity Insert(TEntity entity)
        {
            var EntityObj = (Data.Entity.Entity) entity;
            EntityObj.CREATER = string.IsNullOrEmpty(EntityObj.CREATER)
                ? UserContext.GetCurrentUser()?.UserId
                : EntityObj.CREATER;
            EntityObj.UPDATER = string.IsNullOrEmpty(EntityObj.UPDATER)
                ? UserContext.GetCurrentUser()?.UserId
                : EntityObj.UPDATER;
            EntityObj.CREATEDATE = Clock.Now;
            EntityObj.UPDATEDATE = Clock.Now;

            return base.Insert(entity);
        }

        public override TEntity Update(TEntity entity)
        {
            var EntityObj = (Data.Entity.Entity)entity;
       
            EntityObj.UPDATER = string.IsNullOrEmpty(EntityObj.UPDATER)
                ? UserContext.GetCurrentUser()?.UserId
                : EntityObj.UPDATER;
            EntityObj.UPDATEDATE = Clock.Now;

            return base.Update(entity);
        }

        public override IList<TEntity> Insert(IList<TEntity> entities)
        {
            if (entities == null)
                return entities;
            foreach (var entity in entities)
            {
                var EntityObj = (Data.Entity.Entity) entity;
                EntityObj.CREATER = string.IsNullOrEmpty(EntityObj.CREATER)
                    ? UserContext.GetCurrentUser().UserId
                    : EntityObj.CREATER;
                EntityObj.UPDATER = string.IsNullOrEmpty(EntityObj.UPDATER)
                    ? UserContext.GetCurrentUser().UserId
                    : EntityObj.UPDATER;
                EntityObj.CREATEDATE = Clock.Now;
                EntityObj.UPDATEDATE = Clock.Now;
            }


            return base.Insert(entities);
        }

        public override int Update(Expression<Func<TEntity, bool>> wherePredicate,
            Expression<Func<TEntity, TEntity>> updateFactory)
        {
            var updateExpressionBody = updateFactory.Body;

            while (updateExpressionBody.NodeType == ExpressionType.Convert ||
                   updateExpressionBody.NodeType == ExpressionType.ConvertChecked)
            {
                updateExpressionBody = ((UnaryExpression) updateExpressionBody).Operand;
            }

            var entityType = typeof(TEntity);

            // ENSURE: new T() { MemberInitExpression }
            var memberInitExpression = updateExpressionBody as MemberInitExpression;
            if (memberInitExpression == null)
            {
                throw new Exception("Invalid Cast. The update expression must be of type MemberInitExpression.");
            }

            var MemberBindings = new List<MemberBinding>();
            MemberBindings.AddRange(memberInitExpression.Bindings);
            if (!MemberBindings.Any(t => t.Member.Name == "UPDATER"))
            {
                MemberBindings.Add(Expression.Bind(typeof(TEntity).GetMember("UPDATER")[0],
                    Expression.Constant(UserContext.GetCurrentUser().UserId)));
            }

            if (!MemberBindings.Any(t => t.Member.Name == "UPDATEDATE"))
            {
                MemberBindings.Add(Expression.Bind(typeof(TEntity).GetMember("UPDATEDATE")[0],
                    Expression.Constant(Clock.Now)));
            }

            if (!MemberBindings.Any(t => t.Member.Name == "DATAVERSION") && updateFactory.Parameters.Any())
            {
                var lambdaParam = updateFactory.Parameters.FirstOrDefault();
                MemberBindings.Add(Expression.Bind(typeof(TEntity).GetMember("DATAVERSION")[0],
                    Expression.Add( Expression.PropertyOrField(lambdaParam,"DATAVERSION"), Expression.Constant((long)1))));
            }
            var updateMemberInit = memberInitExpression.Update(memberInitExpression.NewExpression, MemberBindings);

            Expression<Func<TEntity, TEntity>> UpdateExpression = Expression.Lambda<Func<TEntity, TEntity>>(
                updateMemberInit, updateFactory.Parameters
            );

            return GetAllCode().Where(wherePredicate).Update(UpdateExpression);
        }
    }


    public class DbSetContext<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private ConcurrentDictionary<Type, object> dbSetCache;

        public DbSetContext(TDbContext context)
        {
            _context = context;
            this.dbSetCache = new ConcurrentDictionary<Type, object>();
        }

        public DbSet<TEntity> GetTable<TEntity>() where TEntity : Data.Entity.Entity
        {
            return (DbSet<TEntity>) dbSetCache.GetOrAdd(typeof(TEntity), type =>
                _context.Set<TEntity>()
            );
        }
    }

    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <typeparamref name="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class DBSqlRepositoryBase<TDbContext, TEntity, TPrimaryKey> :
        //  AbpRepositoryBase<TEntity, TPrimaryKey>,
        IRepository<TEntity, TPrimaryKey>,
        ISupportsExplicitLoading<TEntity, TPrimaryKey>,
        IRepositoryWithDbContext
        where TEntity : Data.Entity.Entity<TPrimaryKey>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual TDbContext Context => _dbContextProvider.GetDbContext<TDbContext, TEntity>(MultiTenancySide);

        public static MultiTenancySides? MultiTenancySide { get; private set; }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>

        public virtual DbSet<TEntity> Table
        {
            get
            {
                Trace.TraceInformation($"Thread ID {Thread.CurrentThread.ManagedThreadId}, ContextObject {Context.GetHashCode()}");
                return Context.Set<TEntity>();
            }
        }

        public virtual DbTransaction Transaction
        {
            get
            {
                return (DbTransaction) TransactionProvider?.GetActiveTransaction(new ActiveTransactionProviderArgs
                {
                    {"ContextType", typeof(TDbContext)},
                    {"MultiTenancySide", MultiTenancySide}
                });
            }
        }

        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        public IActiveTransactionProvider TransactionProvider { private get; set; }

        private readonly DBContext.IDbContextProvider _dbContextProvider;

        private string moduleName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DBSqlRepositoryBase(DBContext.IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            Type type = typeof(TEntity);
            var attr = type.GetSingleAttributeOfTypeOrBaseTypesOrNull<MultiTenancySideAttribute>();
        
            if (attr != null)
            {
                MultiTenancySide = attr.Side;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotSupportedException("This Method is not supported");
        }


        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotSupportedException("This Method is not supported");
        }


        protected internal IQueryable<TEntity> GetAllCode()
        {
            return GetAllIncludingCode();
        }

        private IQueryable<TEntity> GetAllIncludingCode(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query.AsNoTracking();
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public List<TEntity> GetAllList()
        {
            return GetAllCode().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllCode().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
            // return GetAllCode().Where(CreateEqualityExpressionForId(id)).Skip(0).Take(1).FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllCode().FirstOrDefault(predicate);
            //return GetAllCode().Where(predicate).Skip(0).Take(1).ToArray().FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }


        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }


        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
//            return GetAllCode().Single(predicate);
            return GetAllCode().Where(predicate).Skip(0).Take(2).ToArray().Single();
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity Insert(TEntity entity)
        {
            var EntityEntry = Table.Add(entity);
            Context.SaveChanges();
            return EntityEntry.Entity;
        }


        /// Inserts new entitites.
        /// </summary>
        /// <param name="entitites">Inserted entitites</param>
        public virtual IList<TEntity> Insert(IList<TEntity> entitites)
        {
            Table.AddRange(entitites);
            Context.SaveChanges();

            return entitites;
        }

        /// <summary>
        /// Inserts new entitites.
        /// </summary>
        /// <param name="entitites">Inserted entitites</param>
        public virtual Task<IList<TEntity>> InsertAsync(IList<TEntity> entity)
        {
            return Task.FromResult(Insert(entity));
        }


        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }


        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            var entryEntity = Context.Entry(entity);

            entryEntity.State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public virtual Int32 Update(Expression<Func<TEntity, bool>> wherePredicate,
            Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return GetAllCode().Where(wherePredicate).Update(updateFactory);
        }

        public virtual Task<Int32> UpdateAsync(Expression<Func<TEntity, bool>> wherePredicate,
            Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return GetAllCode().Where(wherePredicate).UpdateAsync(updateFactory);
        }

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            //Could not found the entity, do nothing.
        }

        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public long Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllCode().Where(predicate).Delete();
        }


        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Delete(predicate));
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllCode().Where(predicate).Count();
        }


        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllCode().Where(predicate).LongCount();
        }
        
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllCode().Any(predicate);
        }

        public List<TElement> SqlQuery<TElement>(string sql, params object[] paramters)
            where TElement : class, IQueryEntity
        {
           
            return this.Context.Query<TElement>()
                .FromSql(sql, paramters).ToList();
        }

        public  int ExecuteSqlCommand(string sql, params object[] paramters)
        {
            return  this.Context.Database.ExecuteSqlCommand(sql, paramters);
        }
        
       
        public PageList<TElement> SqlQueryPaging<TElement>(Page page, string sql, params object[] paramters)
            where TElement : class, IQueryEntity
        {
            //var cmd= this.Context.Database.Connection.CreateCommand();

            //var parametersnew = paramters?.Select(t =>
            //{
            //    var param = cmd.CreateParameter();
            //    param.ParameterName = nameof(t);
            //    param.Value = t;
            //    param.Size = Math.Max((t as string).Length + 1, 4000);
            //    return param;
            //}).ToList();


            var sqlQuery = this.Context.Query<TElement>()
                .FromSql(sql, paramters);


            if (page.filters != null && page.filters.rules != null && page.filters.rules.Any())
            {
                var whereString = PageDynamicSearch.getStringForGroup(page.filters, null);
                sqlQuery = DynamicQueryableExtensions.Where(sqlQuery, whereString);
            }
            if (!string.IsNullOrEmpty(page.OrderBy))
                sqlQuery = DynamicQueryableExtensions.OrderBy(sqlQuery, page.OrderBy);

            if (!page.pageSize.HasValue  || page.pageSize.Value <= 0)
            {
                var rows = sqlQuery.ToList();
                var pagelist = new PageList<TElement>()
                {
                    Rows = rows,
                    PagerInfo = new Page()
                    {
                        page = page.page,
                        pageSize = page.pageSize,
                        records = rows.Count,
                        sortColumn = page.sortColumn,
                        sortOrder = page.sortOrder
                    }
                };

                return pagelist;
            }
            else
            {

                var pageResult = DynamicQueryableExtensions.PageResult<TElement>(sqlQuery, page.page, page.pageSize.Value);
                var pagelist = new PageList<TElement>()
                {
                    Rows = pageResult.Queryable.ToList(),
                    PagerInfo = new Page()
                    {
                        page = pageResult.CurrentPage,
                        pageSize = pageResult.PageSize,
                        records = pageResult.RowCount,
                        sortColumn = page.sortColumn,
                        sortOrder = page.sortOrder
                    }
                };


                return pagelist;
            }


//            var sqlQueryResult = DynamicEnumableExtensions.PageResult(sqlQuery, page.page, page.pageSize);
//             
//            return new PageList<TElement>()
//            {
//                Rows = sqlQuery.ToList(),
//                PagerInfo = new Page()
//                {
//                    page = sqlQueryResult.CurrentPage,
//                    pageSize = sqlQueryResult.PageSize,
//                    records = sqlQueryResult.RowCount,
//                    sortColumn = page.sortColumn,
//                    sortOrder = page.sortOrder,
//                }
//            };  
        }


        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);

            if (entry != null)
            {
                return;
            }

            var entry1 = Context.Set<TEntity>().Local.FirstOrDefault(t => t.Id.ToString() == entity.Id.ToString());
            if (entry1 != null)
            {
                return;
            }

            Table.Attach(entity);
        }


        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            throw new NotSupportedException("This Method is not supported");
        }


        public virtual int Count()
        {
            return GetAllCode().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }


        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual long LongCount()
        {
            return GetAllCode().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }


        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        public DbContext GetDbContext()
        {
            return Context;
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return Context.Entry(entity).Collection(collectionExpression).LoadAsync(cancellationToken);
        }

        public Task EnsurePropertyLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return Context.Entry(entity).Reference(propertyExpression).LoadAsync(cancellationToken);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}