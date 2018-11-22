//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using Abp.Dependency;
//using Abp.Domain.Uow;
//using Abp.EntityFramework;
//using Abp.EntityFramework.Uow;
//using Blocks.Framework.Utility.SafeConvert;
//using System.Linq;
//using Z.EntityFramework.Plus;
//
//namespace Blocks.Framework.DBORM.TransactionStrategy
//{
//    public class DefaultTransactionStrategy : TransactionScopeEfTransactionStrategy
//    {
//        List<DataBaseTransaction> dataBaseTransactions;
//        protected override bool isTransactionScope { get { return false; } }
//        bool isBeginTransaction = false;    
//        
//        public DefaultTransactionStrategy() : base()
//        {
//            dataBaseTransactions = new List<DataBaseTransaction>();
//        }
//
//        public override void InitOptions(UnitOfWorkOptions options)
//        {
//            isBeginTransaction = true;
//            base.InitOptions(options);
//        }
//
//
//        public override DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver)
//        {
//
//            var dtx = base.CreateDbContext<TDbContext>(connectionString, dbContextResolver);
//
//            var currentIsolation = (IsolationLevel)SafeConvert.ToEnum(typeof(IsolationLevel), (object)Options.IsolationLevel, IsolationLevel.ReadUncommitted);
//            if(Options.IsTransactional == true && isBeginTransaction)
//            {
//                dataBaseTransactions.Add(new DataBaseTransaction()
//                {
//                   Transaction = dtx.Database.BeginTransaction(currentIsolation),
//                    dbContext = dtx,
//                    isCommit = false   
//                });
//            }
//            return dtx;
//        }
//
//        public override void Commit()
//        {
//            
//            
//            DbContexts.ForEach(dtx =>
//                {
//                    var CurrentTransaction = dataBaseTransactions.FirstOrDefault(t => t.dbContext == dtx);
//                    if(CurrentTransaction != null)
//                    {
//                        CurrentTransaction.Transaction.Commit();
//                        CurrentTransaction.isCommit = true;
//                        CurrentTransaction.Transaction.Dispose();
//
//                    }
//                }
//            );
//        }
//
//        public override void Dispose(IIocResolver iocResolver)
//        {
//
//            foreach (var dtx in DbContexts)
//            {
//                var CurrentTransaction = dataBaseTransactions.FirstOrDefault(t => t.dbContext == dtx);
//                if (CurrentTransaction != null && !CurrentTransaction.isCommit && CurrentTransaction.Transaction != null)
//                {
//
//                    CurrentTransaction.Transaction.Rollback();
//                    CurrentTransaction.Transaction.Dispose();
//                    dataBaseTransactions.Remove(CurrentTransaction);
//                }
//            }
//            base.Dispose(iocResolver);
//        }
//    }
//
//
//   class DataBaseTransaction
//    {
//        public DbContext dbContext { get; set; }
//
//        public DbContextTransaction Transaction { get; set; }
//
//        public bool isCommit { get; set; }
//
//    }
//}