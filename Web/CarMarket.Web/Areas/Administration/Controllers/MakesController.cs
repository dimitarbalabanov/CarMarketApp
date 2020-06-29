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
        private readonly IModelsService modelsService;

        public MakesController(IMakesService makesService, IModelsService modelsService)
        {
            this.makesService = makesService;
            this.modelsService = modelsService;
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
            this.TempData["makeId"] = id;
            var viewModel = await this.makesService.GetSingleByIdAsync<DetailsMakeViewModel>(id);
            return this.View(viewModel);
        }

        public IActionResult CreateModel()
        {
            var viewModel = new CreateModelInputModel();
            if (this.TempData.ContainsKey("makeId"))
            {
                viewModel.MakeId = int.Parse(this.TempData["makeId"].ToString());
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(CreateModelInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.modelsService.CreateAsync<CreateModelInputModel>(input);
            return this.RedirectToAction(nameof(this.Details), new { id = input.MakeId });
        }
    }
}
