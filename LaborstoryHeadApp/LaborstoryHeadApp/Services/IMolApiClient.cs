using MolServiceContracts.BindingModels;
using MolServiceContracts.ViewModels;

namespace MOLServiceWebClient
{
    public interface IMolApiClient
    {
        Task<List<ClassroomViewModel>?> GetClassroomsAsync();

        Task<ClassroomViewModel?> GetClassroomAsync(int id);

        Task<bool> CreateClassroomAsync(ClassroomBindingModel model);

        Task<bool> UpdateClassroomAsync(ClassroomBindingModel model);

        Task<bool> DeleteClassroomAsync(int id);

        Task<List<MaterialResponsiblePersonViewModel>?> GetMaterialResponsiblePersonsAsync();

        Task<MaterialResponsiblePersonViewModel?> GetMaterialResponsiblePersonAsync(int id);

        Task<bool> CreateMaterialResponsiblePersonAsync(MaterialResponsiblePersonBindingModel model);

        Task<bool> UpdateMaterialResponsiblePersonAsync(MaterialResponsiblePersonBindingModel model);

        Task<bool> DeleteMaterialResponsiblePersonAsync(int id);

        Task<List<SoftwareViewModel>> GetSoftwaresAsync();
        Task<bool> CreateSoftwareAsync(SoftwareBindingModel model);
        Task<SoftwareViewModel> GetSoftwareAsync(int id);
        Task<bool> UpdateSoftwareAsync(SoftwareBindingModel model);
        Task<bool> DeleteSoftwareAsync(int id);
    }
}