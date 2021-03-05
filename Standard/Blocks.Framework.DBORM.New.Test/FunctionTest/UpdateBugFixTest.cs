using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using Blocks.Framework.Domain.Uow;
using EntityFramework.Test.Model;
using Xunit;

namespace EntityFramework.Test.FunctionTest
{
    public class UpdateBugFixTest: BlocksTestBase
    {
       
        public UpdateBugFixTest()
        {
          
        }
        [Fact]
        public void update_multColumn_calculation_shouldnot_throw_exception()
        {
            var rep = Resolve<ITestRepository>();
             
          
            //var trans = rep.Context.Database.BeginTransaction();执行时间


            var inputData = new inputData() {input1 = 1, input2 = 2};

            Dictionary<string, string> dic = new Dictionary<string, string>();
           
            rep.Update(
                t => t.Id == ""&& ((t.ISACTIVE + inputData.input1) < t.ISACTIVE) &&
                     ((t.COLNUMINT + inputData.input2) < t.COLNUMINT_NULLABLE) , t => new TESTENTITY()
                {
                    ISACTIVE = t.ISACTIVE + inputData.input1,
                    COLNUMINT = t.COLNUMINT + inputData.input2

                });
             
            
            //trans.Commit();
        }
        
        [Fact]
        public void update_oncurrent()
        {
            
            var rep = Resolve<ITestRepository>();
             
            var unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            var uow =  unitOfWorkManager.Begin();
            
            //var trans = rep.Context.Database.BeginTransaction();执行时间
            var a = rep.FirstOrDefault(t => t.Id == "109649d7-f0b1-4518-8991-9fe3c1dde6ce");

            var rows = rep.Update(t => t.Id == "109649d7-f0b1-4518-8991-9fe3c1dde6ce" && t.COLNUMINT > 600, t => new TESTENTITY()
            {

                COLNUMINT = t.COLNUMINT - 600

            });
            var a1 = rep.FirstOrDefault(t => t.Id == "109649d7-f0b1-4518-8991-9fe3c1dde6ce");
            uow.Complete();
            //trans.Commit();
        }

        [Fact]
        public void update_oncurrent1()
        {
            var unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            var rep = Resolve<ITestRepository>();

            var uow =  unitOfWorkManager.Begin();
          

            //var trans = rep.Context.Database.BeginTransaction();执行时间

            var rows = rep.Update(t => t.Id == "109649d7-f0b1-4518-8991-9fe3c1dde6ce"  && t.COLNUMINT > 600, t => new TESTENTITY()
            {

                COLNUMINT = t.COLNUMINT - 600

            });
            var a = rep.FirstOrDefault(t => t.Id == "109649d7-f0b1-4518-8991-9fe3c1dde6ce");
            
            uow.Complete();
            //trans.Commit();
        }


        [Fact]
        public void update_lostParams()
        {
            var unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            var rep = Resolve<ITestRepository>();


            decimal value = 600.001M;

            var list = new List<decimal>() { 1M, 2M };
            if(1==1)
            {
                list.ForEach(a =>
                {
                    value += a;
                });
                //var trans = rep.Context.Database.BeginTransaction();执行时间

                var rows = rep.Update(t => t.Id == "109649d7-f0b1-4518-8991-9fe3c1dde6ce" && t.COLNUMINT - t.COLNUMINT_NULLABLE > value, t => new TESTENTITY()
                {

                    COLNUMINT = t.COLNUMINT - value

                });
            }
            
           
            //trans.Commit();
        }
    }

    class inputData
    {
        public int input1 { get; set; }
        public int input2 { get; set; }

    }
}