using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolServiceContracts.BindingModels;
using MOLServiceWebClient;

namespace LaboratoryHeadApp.Controllers
{
    public class MaterialTechnicalValueController : Controller
    {
        private readonly IMolApiClient _client;

        public MaterialTechnicalValueController(IMolApiClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _client.GetMaterialTechnicalValuesAsync();
            return View(result ?? new List<MolServiceContracts.ViewModels.MaterialTechnicalValueViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var element = await _client.GetMaterialTechnicalValueAsync(id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDictionariesAsync();
            return View(new MaterialTechnicalValueBindingModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaterialTechnicalValueBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDictionariesAsync();
                return View(model);
            }

            var result = await _client.CreateMaterialTechnicalValueAsync(model);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Не удалось создать МТЦ");
                await LoadDictionariesAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var element = await _client.GetMaterialTechnicalValueAsync(id);
            if (element == null)
            {
                return NotFound();
            }

            var model = new MaterialTechnicalValueBindingModel
            {
                Id = element.Id,
                InventoryNumber = element.InventoryNumber,
                ClassroomId = element.ClassroomId,
                FullName = element.FullName,
                Quantity = element.Quantity,
                Description = element.Description,
                Location = element.Location,
                Cost = element.Cost,
                MaterialResponsiblePersonId = element.MaterialResponsiblePersonId
            };

            await LoadDictionariesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MaterialTechnicalValueBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDictionariesAsync();
                return View(model);
            }

            var result = await _client.UpdateMaterialTechnicalValueAsync(model);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Не удалось обновить МТЦ");
                await LoadDictionariesAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _client.DeleteMaterialTechnicalValueAsync(id);
            if (!result)
            {
                TempData["ErrorMessage"] = "Не удалось удалить МТЦ";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDictionariesAsync()
        {
            var classrooms = await _client.GetClassroomsAsync() ?? new();
            var responsiblePersons = await _client.GetMaterialResponsiblePersonsAsync() ?? new();

            ViewBag.Classrooms = classrooms
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Number
                })
                .ToList();

            ViewBag.MaterialResponsiblePersons = responsiblePersons
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FullName
                })
                .ToList();
        }
    }
}
