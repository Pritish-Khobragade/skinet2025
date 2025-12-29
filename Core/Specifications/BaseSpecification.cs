using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification() : this(null) { }

        public Expression<Func<T, bool>>? Criteria { get; }

        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool IsDistinct { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        public bool isPagingEnabled {get; private set;}

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if(Criteria != null)
            {
                query = query.Where(Criteria);
            }
            return query;
        }
    }

    public class BaseSpecification<T, TResult>
        : BaseSpecification<T>, ISpecification<T, TResult>
    {
        protected BaseSpecification(Expression<Func<T, bool>>? criteria)
            : base(criteria)
        {
        }

        protected BaseSpecification() { }

        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }
}
