using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Data;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Entity;

namespace Blocks.Framework.DBORM.Repository
{

    public class DBSqlRepositoryBase<TEntity> : DBSqlRepositoryBase<BlocksDbContext<TEntity>, TEntity, Guid>
        where TEntity : Entity<Guid>

    {
        protected readonly DbSetContext<BlocksDbContext<TEntity>> Tables;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DBSqlRepositoryBase(DBContext.IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
            Tables = new DbSetContext<BlocksDbContext<TEntity>>(this.Context);
        }
        
        
    }


   public class DbSetContext<TDbContext>  where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private ConcurrentDictionary<Type, object> dbSetCache;

        public DbSetContext(TDbContext context)
        {
            _context = context;
            this.dbSetCache = new ConcurrentDictionary<Type, object>();
        }

        public DbSet<TEntity> GetTable<TEntity>() where TEntity : Entity<Guid>
        {
            return (DbSet<TEntity>)dbSetCache.GetOrAdd(typeof(TEntity), type =>
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
        AbpRepositoryBase<TEntity, TPrimaryKey>,
        ISupportsExplicitLoading<TEntity, TPrimaryKey>,
        IRepositoryWithDbContext
        
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual TDbContext Context => _dbContextProvider.GetDbContext<TDbContext>(MultiTenancySide);

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public virtual DbTransaction Transaction
        {
            get
            {
                return (DbTransaction) TransactionProvider?.GetActiveTransaction(new ActiveTransactionProviderArgs
                {
                    {"ContextType", typeof(TDbContext) },
                    {"MultiTenancySide", MultiTenancySide }
                });
            }
        }

        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        public IActiveTransactionProvider TransactionProvider { private get; set; }
        
        private readonly DBContext.IDbContextProvider _dbContextProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DBSqlRepositoryBase(DBContext.IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override IQueryable<TEntity> GetCurrent()
        {
            return GetAllIncluding();
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetCurrent().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetCurrent().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetCurrent().SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetCurrent().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetCurrent().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public override void Delete(TPrimaryKey id)
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

        public override async Task<int> CountAsync()
        {
            return await GetCurrent().CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetCurrent().Where(predicate).CountAsync();
        }

        public override async Task<long> LongCountAsync()
        {
            return await GetCurrent().LongCountAsync();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetCurrent().Where(predicate).LongCountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public DbContext GetDbContext()
        {
            return Context;
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, ICollection<TProperty>>> collectionExpression, 
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
    }
}