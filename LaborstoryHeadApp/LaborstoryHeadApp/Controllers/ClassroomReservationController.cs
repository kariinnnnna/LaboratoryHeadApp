using LaboratoryHeadApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScheduleServiceContracts.BindingModels;
using ScheduleServiceDataModels.Enums;

namespace LaboratoryHeadApp.Controllers
{
    public class ClassroomReservationController : Controller
    {
        private readonly IScheduleApiClient _scheduleApiClient;

        public ClassroomReservationController(IScheduleApiClient scheduleApiClient)
        {
            _scheduleApiClient = scheduleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Create(DateTime? date, string? classroomNumber)
        {
            var lessonTimes = await _scheduleApiClient.GetLessonTimesAsync() ?? new();

            var model = new ClassroomReservationCreateViewModel
            {
                Date = date ?? DateTime.Today,
                ClassroomNumber = classroomNumber ?? string.Empty,
                LessonTimes = lessonTimes
                    .OrderBy(x => x.PairNumber)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.PairNumber} пара ({x.StartTime:hh\\:mm}-{x.EndTime:hh\\:mm})"
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassroomReservationCreateViewModel model)
        {
            var lessonTimes = await _scheduleApiClient.GetLessonTimesAsync() ?? new();
            model.LessonTimes = lessonTimes
                .OrderBy(x => x.PairNumber)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.PairNumber} пара ({x.StartTime:hh\\:mm}-{x.EndTime:hh\\:mm})"
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.LessonTimeId.HasValue && (!model.StartTime.HasValue || !model.EndTime.HasValue))
            {
                ModelState.AddModelError("", "Укажите либо пару, либо время начала и окончания.");
                return View(model);
            }

            try
            {
                var binding = new ScheduleItemBindingModel
                {
                    Type = ScheduleItemType.Consultation,
                    Date = DateTime.SpecifyKind(model.Date.Date, DateTimeKind.Utc),
                    LessonTimeId = model.LessonTimeId,
                    StartTime = model.LessonTimeId.HasValue ? null : model.StartTime,
                    EndTime = model.LessonTimeId.HasValue ? null : model.EndTime,
                    ClassroomId = model.ClassroomId,
                    ClassroomNumber = model.ClassroomNumber,
                    TeacherId = model.TeacherId,
                    TeacherName = string.IsNullOrWhiteSpace(model.TeacherName) ? "Не указан" : model.TeacherName,
                    GroupId = model.GroupId,
                    GroupName = string.IsNullOrWhiteSpace(model.GroupName) ? "Не указана" : model.GroupName,
                    Subject = model.Subject,
                    Comment = model.Comment,
                    IsImported = false
                };

                await _scheduleApiClient.CreateScheduleItemAsync(binding);

                TempData["SuccessMessage"] = "Бронирование аудитории успешно создано.";

                return RedirectToAction("LessonsRooms", "Schedule", new
                {
                    date = model.Date.ToString("yyyy-MM-dd")
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}
