using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LaboratoryHeadApp.Models
{
    public class ClassroomReservationCreateViewModel
    {
        [Required(ErrorMessage = "Дата обязательна")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Today;

        [Display(Name = "ID аудитории")]
        public int? ClassroomId { get; set; }

        [Required(ErrorMessage = "Аудитория обязательна")]
        [Display(Name = "Аудитория")]
        public string ClassroomNumber { get; set; } = string.Empty;

        [Display(Name = "ID преподавателя")]
        public int? TeacherId { get; set; }

        [Display(Name = "Преподаватель")]
        public string TeacherName { get; set; } = string.Empty;

        [Display(Name = "ID группы")]
        public int? GroupId { get; set; }

        [Display(Name = "Группа")]
        public string GroupName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тема/цель бронирования обязательна")]
        [Display(Name = "Тема")]
        public string Subject { get; set; } = string.Empty;

        [Display(Name = "Пара")]
        public int? LessonTimeId { get; set; }

        [Display(Name = "Время начала")]
        public TimeSpan? StartTime { get; set; }

        [Display(Name = "Время окончания")]
        public TimeSpan? EndTime { get; set; }

        [Display(Name = "Комментарий")]
        public string? Comment { get; set; }

        public List<SelectListItem> LessonTimes { get; set; } = new();
    }
}
