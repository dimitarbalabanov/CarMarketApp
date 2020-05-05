namespace CarMarket.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly IModelsService modelsService;

        public ModelsController(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ModelResponseModel>> GetModelsByMakeId(int id)
        {
            var models = this.modelsService.GetAllByMakeId<ModelResponseModel>(id).ToList();
            if (models == null)
            {
                return this.NotFound();
            }

            return models;
        }
    }
}
