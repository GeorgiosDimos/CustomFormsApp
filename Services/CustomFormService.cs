using CustomFormsApp.Data;
using CustomFormsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomFormsApp.Services
{
    public class CustomFormService : ICustomFormService
    {
        private readonly ApplicationDbContext _context;

        public CustomFormService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomForm?> CreateFormAsync(CustomForm form)
        {
            form.DateCreated = DateTime.UtcNow;
            form.DateUpdated = DateTime.UtcNow;
            _context.CustomForms.Add(form);
            await _context.SaveChangesAsync();
            return form; // No change needed for this method
        }

        public async Task<CustomForm?> GetFormByIdAsync(int id)
        {
            return await _context.CustomForms
                .Include(f => f.Fields)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<CustomForm>> GetAllFormsAsync()
        {
            return await _context.CustomForms
                .Include(f => f.Fields)
                .ToListAsync();
        }

        public async Task<CustomForm?> UpdateFormAsync(CustomForm form)
        {
            var existingForm = await _context.CustomForms
                .Include(f => f.Fields)
                .FirstOrDefaultAsync(f => f.Id == form.Id);

            if (existingForm == null)
                return null;

            existingForm.Title = form.Title;
            existingForm.Description = form.Description;
            existingForm.DateUpdated = DateTime.UtcNow;
            existingForm.Fields = form.Fields;

            await _context.SaveChangesAsync();
            return existingForm;
        }

        public async Task<bool> DeleteFormAsync(int id)
        {
            var form = await _context.CustomForms.Include(f => f.Fields).FirstOrDefaultAsync(f => f.Id == id);

            if (form == null)
                return false;

            if (form.Fields.Any()){
                _context.CustomFields.RemoveRange(form.Fields);
            }   


            _context.CustomForms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
