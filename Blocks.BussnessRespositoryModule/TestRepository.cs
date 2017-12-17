using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blocks.BussnessEntityModule;

namespace Blocks.BussnessRespositoryModule
{
    public class TestRepository : ITestRepository
    {
        public string GetValue(string value)
        {
            return value;
        }

        public IQueryable<TestEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TestEntity> GetAllIncluding(params Expression<Func<TestEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public List<TestEntity> GetAllList()
        {
            throw new NotImplementedException();
        }

        public Task<List<TestEntity>> GetAllListAsync()
        {
            throw new NotImplementedException();
        }

        public List<TestEntity> GetAllList(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<TestEntity>> GetAllListAsync(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T Query<T>(Func<IQueryable<TestEntity>, T> queryMethod)
        {
            throw new NotImplementedException();
        }

        public TestEntity Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TestEntity Single(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> SingleAsync(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TestEntity FirstOrDefault(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> FirstOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TestEntity FirstOrDefault(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> FirstOrDefaultAsync(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TestEntity Load(Guid id)
        {
            throw new NotImplementedException();
        }

        public TestEntity Insert(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> InsertAsync(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Guid InsertAndGetId(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> InsertAndGetIdAsync(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public TestEntity InsertOrUpdate(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> InsertOrUpdateAsync(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Guid InsertOrUpdateAndGetId(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> InsertOrUpdateAndGetIdAsync(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public TestEntity Update(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> UpdateAsync(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public TestEntity Update(Guid id, Action<TestEntity> updateAction)
        {
            throw new NotImplementedException();
        }

        public Task<TestEntity> UpdateAsync(Guid id, Func<TestEntity, Task> updateAction)
        {
            throw new NotImplementedException();
        }

        public void Delete(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public long LongCount()
        {
            throw new NotImplementedException();
        }

        public Task<long> LongCountAsync()
        {
            throw new NotImplementedException();
        }

        public long LongCount(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> LongCountAsync(Expression<Func<TestEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}