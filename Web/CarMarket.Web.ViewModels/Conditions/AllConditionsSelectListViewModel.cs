namespace CarMarket.Web.ViewModels.Conditions
{
    using System.Collections.Generic;

    public class AllConditionsSelectListViewModel
    {
        public IEnumerable<ConditionSelectListViewModel> Conditions { get; set; }

        public int SelectedConditionId { get; set; }
    }
}
