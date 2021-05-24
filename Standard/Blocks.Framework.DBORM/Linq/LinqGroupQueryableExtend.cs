//using Blocks.Framework.AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Text;


//namespace Blocks.Framework.DBORM.Linq
//{
//    public static class LinqGroupQueryableExtend
//    {


//        //public static List<TDto> SelectToList<TKey, TDto>(this ILinqGroupQueryable<TKey> dbLinqGroupQueryable, Expression<Func<TKey, TDto>> selector)  
//        //{
//        //    return dbLinqGroupQueryable.SelectToList1((LambdaExpression)selector).AutoMapTo<List<TDto>>();
//        //}

//        public static List<TDto> SelectToList<T1, TDto>(this ILinqGroupQueryable<TKey> dbLinqGroupQueryable, Blocks.Framework.DBORM.Linq.SelectBuilder<TKey>.Selectc<T1, TDto> selector) where T1 : IEnumerable<Data.Entity.Entity>
//        {
//            throw new Exception();
//            return dbLinqGroupQueryable.SelectToList1((LambdaExpression)selector(new SelectBuilder<TKey>())).AutoMapTo<List<TDto>>();
//        }


//    }

 
//}
