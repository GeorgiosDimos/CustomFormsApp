namespace CustomFormsApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using CustomFormsApp.Services;
    using CustomFormsApp.Models;

    [Route("api/forms")]
    [ApiController]
    public class CustomFormsController : ControllerBase
    {
        private readonly ICustomFormService _formService;

        public CustomFormsController(ICustomFormService formService)
        {
            _formService = formService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(CustomForm form)
        {
            var createdForm = await _formService.CreateFormAsync(form);
            return CreatedAtAction(nameof(GetFormById), new { id = createdForm.Id }, createdForm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormById(int id)
        {
            var form = await _formService.GetFormByIdAsync(id);
            if (form == null)
                return NotFound();
            return Ok(form);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            var forms = await _formService.GetAllFormsAsync();
            return Ok(forms);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm(int id, CustomForm form)
        {   
            if (id != form.Id)
                return BadRequest();

            var updatedForm = await _formService.UpdateFormAsync(form);
            if (updatedForm == null)
                return NotFound();
            return Ok(updatedForm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var success = await _formService.DeleteFormAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
