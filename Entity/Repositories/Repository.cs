using Core.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class Repository<T> : IRepositoryEntity<T> where T : class
    {
        protected readonly TemplateDbContext _dbContext;

        public Repository(TemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            T? t = await _dbContext.Set<T>().FindAsync(id);
            return t;
        }

        public async virtual Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async virtual Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async virtual Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async virtual Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }

        public async Task<IReadOnlyList<T>> GetFilteredAsync(QueryFilter filter)
        {
            // Create a query that selects all entities of type T
            var query = _dbContext.Set<T>().AsQueryable();

            // Apply the filter conditions to the query
            if (filter.Conditions != null)
            {
                foreach (var condition in filter.Conditions)
                {
                    // Use reflection to get the property with the name specified in the condition
                    var prop = typeof(T).GetProperty(condition.Column);

                    // Create a lambda expression that represents accessing the property
                    var param = Expression.Parameter(typeof(T));
                    var propAccess = Expression.MakeMemberAccess(param, prop);

                    // Create a constant expression that represents the value to compare with
                    var val = Expression.Constant(condition.Value);

                    // Create an expression that represents the comparison specified in the condition
                    Expression comparison;
                    switch (condition.Operator)
                    {
                        case "=":
                            comparison = Expression.Equal(propAccess, val);
                            break;
                        case ">":
                            comparison = Expression.GreaterThan(propAccess, val);
                            break;
                        case ">=":
                            comparison = Expression.GreaterThanOrEqual(propAccess, val);
                            break;
                        case "<":
                            comparison = Expression.LessThan(propAccess, val);
                            break;
                        case "<=":
                            comparison = Expression.LessThanOrEqual(propAccess, val);
                            break;
                        default:
                            throw new ArgumentException($"Invalid operator: {condition.Operator}");
                    }

                    // Create a lambda expression that represents the comparison and pass it to the Where method
                    var lambda = Expression.Lambda<Func<T, bool>>(comparison, param);
                    query = query.Where(lambda);
                }
            }

            // Apply the ordering specified in the filter to the query
            if (filter.OrderByColumns != null)
            {
                foreach (var orderByColumn in filter.OrderByColumns)
                {
                    // Use reflection to get the property with the name specified in the OrderByColumn
                    var prop = typeof(T).GetProperty(orderByColumn.Column);

                    // Create a lambda expression that represents accessing the property
                    var param = Expression.Parameter(typeof(T));
                    var propAccess = Expression.MakeMemberAccess(param, prop);

                    // Create a lambda expression that represents the order by and pass it to the OrderBy or OrderByDescending method
                    var lambda = Expression.Lambda<Func<T, object>>(propAccess, param);
                    if (orderByColumn.Direction == "asc")
                    {
                        query = query.OrderBy(lambda);
                    }
                    else
                    {
                        query = query.OrderByDescending(lambda);
                    }
                    // Apply paging to the query
                    if (filter.Limit.HasValue && filter.Offset.HasValue)
                    {
                        query = query.Skip(filter.Offset.Value).Take(filter.Limit.Value);
                        //query = query.Skip((page - 1) * size).Take(size);
                    }
                }
            }

            // Execute the query and return the results
            return await query.ToListAsync();
        }

    }
}

//private readonly DatabaseContext _context;
//private readonly DbSet<T> _db;

//public GenericRepository(DatabaseContext context)
//{
//    _context = context;
//    _db = _context.Set<T>();
//}

//public async Task Delete(int id)
//{
//    var entity = await _db.FindAsync(id);
//    _db.Remove(entity);
//}

//public void DeleteRange(IEnumerable<T> entities)
//{
//    _db.RemoveRange(entities);
//}

//public async Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
//{
//    IQueryable<T> query = _db;
//    if (include != null)
//    {
//        query = include(query);
//    }


//    return await query.AsNoTracking().FirstOrDefaultAsync(expression);
//}


//public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
//    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
//    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
//{
//    IQueryable<T> query = _db;

//    if (expression != null)
//    {
//        query = query.Where(expression);
//    }

//    if (include != null)
//    {
//        query = include(query);
//    }

//    if (orderBy != null)
//    {
//        query = orderBy(query);
//    }

//    return await query.AsNoTracking().ToListAsync();
//}



//public async Task Insert(T entity)
//{
//    var result = await _db.AddAsync(entity);
//}

//public async Task InsertRange(IEnumerable<T> entities)
//{
//    await _db.AddRangeAsync(entities);
//}

//public void Update(T entity)
//{
//    _db.Attach(entity);
//    _context.Entry(entity).State = EntityState.Modified;
//}
