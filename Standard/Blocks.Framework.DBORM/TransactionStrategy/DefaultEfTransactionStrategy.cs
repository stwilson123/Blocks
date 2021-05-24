using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
 
using Blocks.Framework.Utility.SafeConvert;
using System.Linq;
using Blocks.Framework.Domain.Uow;
using Blocks.Framework.DBORM.DBContext;
using Microsoft.EntityFrameworkCore.Storage;
using Blocks.Framework.Utility.Extensions;
using System.Transactions;
using Abp.Transactions.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blocks.Framework.DBORM.TransactionStrategy
{
    public class DefaultEfTransactionStrategy : TransactionScopeEfTransactionStrategy, ITransientDependency
    {
       // List<DataBaseTransaction> dataBaseTransactions;
        protected IDictionary<string, ActiveTransactionInfo> ActiveTransactions { get; }

        protected override bool isTransactionScope { get { return false; } }
        bool isBeginTransaction = false;

        public DefaultEfTransactionStrategy() : base()
        {
            //dataBaseTransactions = new List<DataBaseTransaction>();
            ActiveTransactions = new Dictionary<string, ActiveTransactionInfo>();

        }

        public override void InitOptions(UnitOfWorkOptions options)
        {
            isBeginTransaction = true;
            base.InitOptions(options);
        }


        public override DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver, string moduleName)
        {

            //var dtx = base.CreateDbContext<TDbContext>(connectionString, dbContextResolver, moduleName);

            //var currentIsolation = (IsolationLevel)SafeConvert.ToEnum(typeof(IsolationLevel), (object)Options.IsolationLevel, IsolationLevel.ReadUncommitted);
            //if (Options.IsTransactional == true && isBeginTransaction)
            //{
            //    dataBaseTransactions.Add(new DataBaseTransaction()
            //    {
            //        Transaction = dtx.Database.BeginTransaction(currentIsolation),
            //        dbContext = dtx,
            //        isCommit = false
            //    });
            //}
            //return dtx;
            DbContext dbContext;

            var activeTransaction = ActiveTransactions.GetOrDefault(connectionString);
            if (activeTransaction == null)
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(connectionString, moduleName);
               
                var dbtransaction = dbContext.Database.BeginTransaction((Options.IsolationLevel ?? IsolationLevel.ReadCommitted).ToSystemDataIsolationLevel());
                activeTransaction = new ActiveTransactionInfo(dbtransaction, dbContext);
                ActiveTransactions[connectionString] = activeTransaction;
            }
            else
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(
                    activeTransaction.DbContextTransaction.GetDbTransaction().Connection,
                    moduleName,
                    true
                );

                if (dbContext.HasRelationalTransactionManager())
                {
                    dbContext.Database.UseTransaction(activeTransaction.DbContextTransaction.GetDbTransaction());
                }
                else
                {
                    dbContext.Database.BeginTransaction();
                }

                activeTransaction.AttendedDbContexts.Add(dbContext);
            }

            return dbContext;
        }

        public override void Commit()
        {


            //DbContexts.ForEach(dtx =>
            //    {
            //        var CurrentTransaction = dataBaseTransactions.FirstOrDefault(t => t.dbContext == dtx);
            //        if (CurrentTransaction != null)
            //        {
            //            CurrentTransaction.Transaction.Commit();
            //            CurrentTransaction.isCommit = true;
            //            CurrentTransaction.Transaction.Dispose();

            //        }
            //    }
            //);
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbContextTransaction.Commit();

                foreach (var dbContext in activeTransaction.AttendedDbContexts)
                {
                    if (dbContext.HasRelationalTransactionManager())
                    {
                        continue; //Relational databases use the shared transaction
                    }

                    dbContext.Database.CommitTransaction();
                }
            }
        }

        public override void Dispose(IIocResolver iocResolver)
        {

            //foreach (var dtx in DbContexts)
            //{
            //    var CurrentTransaction = dataBaseTransactions.FirstOrDefault(t => t.dbContext == dtx);
            //    if (CurrentTransaction != null && !CurrentTransaction.isCommit && CurrentTransaction.Transaction != null)
            //    {

            //        CurrentTransaction.Transaction.Rollback();
            //        CurrentTransaction.Transaction.Dispose();
            //        dataBaseTransactions.Remove(CurrentTransaction);
            //    }
            //}
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbContextTransaction.Dispose();

                foreach (var attendedDbContext in activeTransaction.AttendedDbContexts)
                {
                    iocResolver.Release(attendedDbContext);
                }

                iocResolver.Release(activeTransaction.StarterDbContext);
            }

            ActiveTransactions.Clear();
            base.Dispose(iocResolver);
        }
    }


    //class DataBaseTransaction
    //{
    //    public DbContext dbContext { get; set; }

    //    public IDbContextTransaction Transaction { get; set; }

    //    public bool isCommit { get; set; }

    //}

   public  class ActiveTransactionInfo
    {
        public IDbContextTransaction DbContextTransaction { get; }

        public DbContext StarterDbContext { get; }

        public List<DbContext> AttendedDbContexts { get; }

        public ActiveTransactionInfo(IDbContextTransaction dbContextTransaction, DbContext starterDbContext)
        {
            DbContextTransaction = dbContextTransaction;
            StarterDbContext = starterDbContext;

            AttendedDbContexts = new List<DbContext>();
        }
    }

    internal static class DbContextExtensions
    {
        public static bool HasRelationalTransactionManager(this DbContext dbContext)
        {
            return dbContext.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager;
        }
    }
}