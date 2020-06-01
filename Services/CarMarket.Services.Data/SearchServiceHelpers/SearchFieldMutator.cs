namespace CarMarket.Services.Data.SearchServiceHelpers
{
    using System;
    using System.Linq;

    internal class SearchFieldMutator<TItem, TSearch>
    {
        public SearchFieldMutator(Predicate<TSearch> condition, Func<IQueryable<TItem>, TSearch, IQueryable<TItem>> queryMutator)
        {
            this.Condition = condition;
            this.QueryMutator = queryMutator;
        }

        public Predicate<TSearch> Condition { get; set; }

        public Func<IQueryable<TItem>, TSearch, IQueryable<TItem>> QueryMutator { get; set; }

        public IQueryable<TItem> Apply(TSearch search, IQueryable<TItem> query)
        {
            return this.Condition(search) ? this.QueryMutator(query, search) : query;
        }
    }
}
