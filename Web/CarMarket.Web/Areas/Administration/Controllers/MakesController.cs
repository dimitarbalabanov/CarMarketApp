namespace CarMarket.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CarMarket.Common;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Administration.Makes;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class MakesController : Controller
    {
        private readonly IMakesService makesService;

        public MakesController(IMakesService makesService)
        {
            this.makesService = makesService;
        }

        public async Task<IActionResult> Index()
        {
            var makes = await this.makesService.GetAllAsync<MakeAdminViewModel>();
            var viewModel = new MakesAdminViewModel
            {
                Makes = makes,
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateMakeInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMakeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var makeId = await this.makesService.CreateAsync<CreateMakeInputModel>(input);

            return this.RedirectToAction(nameof(this.Details), new { id = makeId });
        }

        public async Task<IActionResult> Details(int id)
        {

            return this.View();
        }
    }
}
