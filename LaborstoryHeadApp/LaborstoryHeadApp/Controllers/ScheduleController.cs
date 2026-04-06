using LaboratoryHeadApp.Models;
using Microsoft.AspNetCore.Mvc;
using ScheduleServiceContracts.BindingModels;
using ScheduleServiceContracts.ViewModels;

namespace LaboratoryHeadApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleApiClient _scheduleApiClient;
        private readonly IConfiguration _configuration;

        public ScheduleController(IScheduleApiClient scheduleApiClient, IConfiguration configuration)
        {
            _scheduleApiClient = scheduleApiClient;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LessonsRooms(DateTime? date)
        {
            var selectedDate = (date ?? DateTime.Today).Date;

            var items = await _scheduleApiClient.GetScheduleAsync() ?? new List<ScheduleServiceContracts.ViewModels.ScheduleItemViewModel>();

            var dayItems = items
                .Where(x => x.Date.Date == selectedDate)
                .Where(x => !string.IsNullOrWhiteSpace(x.ClassroomNumber))
                .Where(x => IsTargetClassroom(x.ClassroomNumber!))
                .ToList();

            var model = new LaboratoryHeadApp.Models.LessonsRoomsPageViewModel
            {
                SelectedDate = selectedDate.ToString("yyyy-MM-dd"),
                Classrooms = dayItems
                    .Select(x => x.ClassroomNumber)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Teachers = dayItems
                    .Select(x => x.TeacherName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Groups = dayItems
                    .Select(x => x.GroupName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Lessons = dayItems
                    .Select(x => new LaboratoryHeadApp.Models.LessonsRoomsItemViewModel
                    {
                        ClassroomNumber = x.ClassroomNumber ?? string.Empty,
                        PairNumber = x.PairNumber,
                        TypeName = x.TypeName,
                        Subject = x.Subject,
                        TeacherName = x.TeacherName ?? string.Empty,
                        GroupName = x.GroupName ?? string.Empty,
                        Subgroup = x.Comment ?? string.Empty,
                        IsImported = x.IsImported
                    })
                    .OrderBy(x => x.ClassroomNumber)
                    .ThenBy(x => x.PairNumber)
                    .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> LessonsGroups(DateTime? date)
        {
            var selectedDate = (date ?? DateTime.Today).Date;

            var items = await _scheduleApiClient.GetScheduleAsync() ?? new List<ScheduleItemViewModel>();

            var dayItems = items
                .Where(x => x.Date.Date == selectedDate)
                .ToList();

            var model = new LessonsGroupsPageViewModel
            {
                SelectedDate = selectedDate.ToString("yyyy-MM-dd"),
                Groups = dayItems
                    .Select(x => x.GroupName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Teachers = dayItems
                    .Select(x => x.TeacherName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Classrooms = dayItems
                    .Select(x => x.ClassroomNumber)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Lessons = dayItems
                    .Select(x => new LessonsGroupsItemViewModel
                    {
                        GroupName = x.GroupName ?? string.Empty,
                        PairNumber = x.PairNumber,
                        TypeName = x.TypeName,
                        Subject = x.Subject,
                        ClassroomNumber = x.ClassroomNumber ?? string.Empty,
                        TeacherName = x.TeacherName ?? string.Empty,
                        StartTime = x.StartTime?.ToString(@"hh\:mm") ?? string.Empty,
                        EndTime = x.EndTime?.ToString(@"hh\:mm") ?? string.Empty,
                        Subgroup = x.Comment ?? string.Empty,
                        IsImported = x.IsImported
                    })
                    .OrderBy(x => x.GroupName)
                    .ThenBy(x => x.PairNumber)
                    .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> SyncLessons(string? returnAction, DateTime? date)
        {
            try
            {
                var folderPath = _configuration["ApiSettings:ScheduleImportFolderPath"];

                if (string.IsNullOrWhiteSpace(folderPath))
                {
                    TempData["ErrorMessage"] = "Не указан путь к папке для синхронизации расписания.";
                    return RedirectToAction(returnAction ?? nameof(LessonsGroups), new { date });
                }

                await _scheduleApiClient.ImportGroupSchedulesFromFolderAsync(
                    new UniversityScheduleImportFolderBindingModel
                    {
                        FolderPath = folderPath
                    });

                TempData["SuccessMessage"] = "Синхронизация расписания завершена.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(returnAction ?? nameof(LessonsGroups), new { date });
        }

        public async Task<IActionResult> LessonsTeachers(DateTime? date)
        {
            var selectedDate = (date ?? DateTime.Today).Date;

            var items = await _scheduleApiClient.GetScheduleAsync() ?? new List<ScheduleServiceContracts.ViewModels.ScheduleItemViewModel>();

            var dayItems = items
                .Where(x => x.Date.Date == selectedDate)
                .ToList();

            var model = new LaboratoryHeadApp.Models.LessonsTeachersPageViewModel
            {
                SelectedDate = selectedDate.ToString("yyyy-MM-dd"),
                Teachers = dayItems
                    .Select(x => x.TeacherName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Groups = dayItems
                    .Select(x => x.GroupName)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Classrooms = dayItems
                    .Select(x => x.ClassroomNumber)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList()!,
                Lessons = dayItems
                    .Select(x => new LaboratoryHeadApp.Models.LessonsTeachersItemViewModel
                    {
                        TeacherName = x.TeacherName ?? string.Empty,
                        PairNumber = x.PairNumber,
                        TypeName = x.TypeName,
                        Subject = x.Subject,
                        ClassroomNumber = x.ClassroomNumber ?? string.Empty,
                        GroupName = x.GroupName ?? string.Empty,
                        Subgroup = x.Comment ?? string.Empty,
                        IsImported = x.IsImported
                    })
                    .OrderBy(x => x.TeacherName)
                    .ThenBy(x => x.PairNumber)
                    .ToList()
            };

            return View(model);
        }

        private static bool IsTargetClassroom(string classroom)
        {
            var value = classroom.Trim().Replace('–', '-');

            return System.Text.RegularExpressions.Regex.IsMatch(
                value,
                @"^(3[-_]4.+)$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        public IActionResult Duty() => View();
    }
}