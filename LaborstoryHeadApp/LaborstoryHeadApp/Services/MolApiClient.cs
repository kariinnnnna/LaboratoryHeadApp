using System.Net.Http.Json;
using MolServiceContracts.BindingModels;
using MolServiceContracts.SearchModels;
using MolServiceContracts.ViewModels;

namespace MOLServiceWebClient
{
    public class MolApiClient : IMolApiClient
    {
        private readonly HttpClient _httpClient;

        public MolApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClassroomViewModel>?> GetClassroomsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ClassroomViewModel>>("api/Classroom/GetAll");
        }

        public async Task<ClassroomViewModel?> GetClassroomAsync(int id)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Classroom/GetElement", new ClassroomSearchModel
            {
                Id = id
            });

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ClassroomViewModel>();
        }

        public async Task<bool> CreateClassroomAsync(ClassroomBindingModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Classroom/Create", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClassroomAsync(ClassroomBindingModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Classroom/Update", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClassroomAsync(int id)
        {
            var response = await _httpClient.PostAsync($"api/Classroom/Delete?id={id}", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка API при удалении аудитории: {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }


        public async Task<List<MaterialResponsiblePersonViewModel>?> GetMaterialResponsiblePersonsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MaterialResponsiblePersonViewModel>>(
                "api/MaterialResponsiblePerson/GetAll");
        }

        public async Task<MaterialResponsiblePersonViewModel?> GetMaterialResponsiblePersonAsync(int id)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/MaterialResponsiblePerson/GetElement",
                new MaterialResponsiblePersonSearchModel { Id = id });

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<MaterialResponsiblePersonViewModel>();
        }

        public async Task<bool> CreateMaterialResponsiblePersonAsync(MaterialResponsiblePersonBindingModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/MaterialResponsiblePerson/Create",
                model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка API при создании МОЛ: {error}");
            }

            return true;
        }

        public async Task<bool> UpdateMaterialResponsiblePersonAsync(MaterialResponsiblePersonBindingModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/MaterialResponsiblePerson/Update",
                model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка API при обновлении МОЛ: {error}");
            }

            return true;
        }

        public async Task<bool> DeleteMaterialResponsiblePersonAsync(int id)
        {
            var response = await _httpClient.PostAsync(
                $"api/MaterialResponsiblePerson/Delete?id={id}",
                null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка API при удалении МОЛ: {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }
        public async Task<List<SoftwareViewModel>?> GetSoftwaresAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SoftwareViewModel>>("api/Software/GetAll");
        }

        // Получить ПО по ID
        public async Task<SoftwareViewModel?> GetSoftwareAsync(int id)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Software/GetElement", new SoftwareSearchModel
            {
                Id = id
            });

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<SoftwareViewModel>();
        }

        // Создать новое ПО
        public async Task<bool> CreateSoftwareAsync(SoftwareBindingModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Software/Create", model);
            return response.IsSuccessStatusCode;
        }

        // Обновить данные ПО
        public async Task<bool> UpdateSoftwareAsync(SoftwareBindingModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Software/Update", model);
            return response.IsSuccessStatusCode;
        }

        // Удалить ПО
        public async Task<bool> DeleteSoftwareAsync(int id)
        {
            var response = await _httpClient.PostAsync($"api/Software/Delete?id={id}", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка API при удалении ПО: {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }

    }
}