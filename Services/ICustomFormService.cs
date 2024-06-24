namespace CustomFormsApp.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CustomFormsApp.Models;

    public interface ICustomFormService
    {
        Task<CustomForm> CreateFormAsync(CustomForm form);
        Task<CustomForm> GetFormByIdAsync(int id);
        Task<IEnumerable<CustomForm>> GetAllFormsAsync();
        Task<CustomForm> UpdateFormAsync(CustomForm form);
        Task<bool> DeleteFormAsync(int id);
    }
}
