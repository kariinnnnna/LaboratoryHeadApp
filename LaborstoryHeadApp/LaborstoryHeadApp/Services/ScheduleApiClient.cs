using System.Net.Http.Json;
using ScheduleServiceContracts.BindingModels;
using ScheduleServiceContracts.SearchModels;
using ScheduleServiceContracts.ViewModels;

public class ScheduleApiClient : IScheduleApiClient
{
    private readonly HttpClient _client;

    public ScheduleApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<ScheduleItemViewModel>?> GetScheduleAsync()
    {
        return await _client.GetFromJsonAsync<List<ScheduleItemViewModel>>(
            "api/ScheduleItem/GetAll");
    }

    public async Task<List<LessonTimeViewModel>?> GetLessonTimesAsync()
    {
        return await _client.GetFromJsonAsync<List<LessonTimeViewModel>>(
            "api/LessonTime/GetAll");
    }

    public async Task<LessonTimeViewModel?> GetLessonTimeByIdAsync(int id)
    {
        var response = await _client.PostAsJsonAsync(
            "api/LessonTime/GetElement",
            new LessonTimeSearchModel { Id = id });

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LessonTimeViewModel>();
    }

    public async Task<LessonTimeViewModel?> CreateLessonTimeAsync(LessonTimeBindingModel model)
    {
        var response = await _client.PostAsJsonAsync("api/LessonTime/Create", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LessonTimeViewModel>();
    }

    public async Task<LessonTimeViewModel?> UpdateLessonTimeAsync(LessonTimeBindingModel model)
    {
        var response = await _client.PostAsJsonAsync("api/LessonTime/Update", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LessonTimeViewModel>();
    }

    public async Task<bool> DeleteLessonTimeAsync(int id)
    {
        var response = await _client.PostAsJsonAsync("api/LessonTime/Delete", id);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<List<DutyScheduleViewModel>?> GetDutyScheduleAsync()
    {
        return await _client.GetFromJsonAsync<List<DutyScheduleViewModel>>(
            "api/DutySchedule/GetAll");
    }

    public async Task<List<DutyPersonViewModel>?> GetDutyPersonsAsync()
    {
        return await _client.GetFromJsonAsync<List<DutyPersonViewModel>>(
            "api/DutyPerson/GetAll");
    }

    public async Task<DutyPersonViewModel?> GetDutyPersonByIdAsync(int id)
    {
        var response = await _client.PostAsJsonAsync(
            "api/DutyPerson/GetElement",
            new DutyPersonSearchModel { Id = id });

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<DutyPersonViewModel>();
    }

    public async Task<DutyPersonViewModel?> CreateDutyPersonAsync(DutyPersonBindingModel model)
    {
        var response = await _client.PostAsJsonAsync("api/DutyPerson/Create", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DutyPersonViewModel>();
    }

    public async Task<DutyPersonViewModel?> UpdateDutyPersonAsync(DutyPersonBindingModel model)
    {
        var response = await _client.PostAsJsonAsync("api/DutyPerson/Update", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DutyPersonViewModel>();
    }

    public async Task<bool> DeleteDutyPersonAsync(int id)
    {
        var response = await _client.PostAsJsonAsync("api/DutyPerson/Delete", id);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }
    public async Task<DutyScheduleViewModel?> GetDutyScheduleByIdAsync(int id)
    {
        var response = await _client.PostAsJsonAsync(
            "api/DutySchedule/GetElement",
            new DutyScheduleSearchModel { Id = id });

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<DutyScheduleViewModel>();
    }

    public async Task<DutyScheduleViewModel?> CreateDutyScheduleAsync(DutyScheduleBindingModel model)
    {
        var response = await _client.PostAsJsonAsync("api/DutySchedule/Create", model);

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка API при создании графика дежурств: {errorText}");
        }

        return await response.Content.ReadFromJsonAsync<DutyScheduleViewModel>();
    }

    public async Task<DutyScheduleViewModel?> UpdateDutyScheduleAsync(DutyScheduleBindingModel model)
    {
        var response = await _client.PostAsJsonAsync("api/DutySchedule/Update", model);

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка API при обновлении графика дежурств: {errorText}");
        }

        return await response.Content.ReadFromJsonAsync<DutyScheduleViewModel>();
    }

    public async Task<bool> DeleteDutyScheduleAsync(int id)
    {
        var response = await _client.PostAsJsonAsync(
            "api/DutySchedule/Delete",
            new DutyScheduleBindingModel { Id = id });

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка API при удалении графика дежурств: {errorText}");
        }

        return await response.Content.ReadFromJsonAsync<bool>();
    }

}