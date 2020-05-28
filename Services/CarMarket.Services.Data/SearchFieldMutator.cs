namespace CarMarket.Services.Data
{
    using System;
    using System.Linq;

    public class SearchFieldMutator<TItem, TSearch>
    {
        public SearchFieldMutator(Predicate<TSearch> condition, QueryMutator<TItem, TSearch> mutator)
        {
            this.Condition = condition;
            this.Mutator = mutator;
        }

        public delegate IQueryable<TItem> QueryMutator<TItem, TSearch>(IQueryable<TItem> items, TSearch search);

        public Predicate<TSearch> Condition { get; set; }

        public QueryMutator<TItem, TSearch> Mutator { get; set; }

        public IQueryable<TItem> Apply(TSearch search, IQueryable<TItem> query)
        {
            return this.Condition(search) ? this.Mutator(query, search) : query;
        }
    }
}
