using ScheduleServiceContracts.BindingModels;
using ScheduleServiceContracts.ViewModels;

public interface IScheduleApiClient
{
    Task<List<ScheduleItemViewModel>?> GetScheduleAsync();

    Task<List<LessonTimeViewModel>?> GetLessonTimesAsync();
    Task<LessonTimeViewModel?> GetLessonTimeByIdAsync(int id);
    Task<LessonTimeViewModel?> CreateLessonTimeAsync(LessonTimeBindingModel model);
    Task<LessonTimeViewModel?> UpdateLessonTimeAsync(LessonTimeBindingModel model);
    Task<bool> DeleteLessonTimeAsync(int id);

    Task<List<DutyScheduleViewModel>?> GetDutyScheduleAsync();

    Task<List<DutyPersonViewModel>?> GetDutyPersonsAsync();
    Task<DutyPersonViewModel?> GetDutyPersonByIdAsync(int id);
    Task<DutyPersonViewModel?> CreateDutyPersonAsync(DutyPersonBindingModel model);
    Task<DutyPersonViewModel?> UpdateDutyPersonAsync(DutyPersonBindingModel model);
    Task<bool> DeleteDutyPersonAsync(int id);

    Task<DutyScheduleViewModel?> CreateDutyScheduleAsync(DutyScheduleBindingModel model);
    Task<bool> DeleteDutyScheduleAsync(int id);
    Task<DutyScheduleViewModel?> GetDutyScheduleByIdAsync(int id);
    Task<DutyScheduleViewModel?> UpdateDutyScheduleAsync(DutyScheduleBindingModel model);
}