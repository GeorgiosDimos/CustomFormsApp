using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CustomFormsApp.Controllers;
using CustomFormsApp.Services;
using CustomFormsApp.Models;

namespace Tests
{
    public class CustomFormsControllerTests
    {
        [Fact]
        public async Task CreateForm_ValidForm_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockFormService = new Mock<ICustomFormService>();
            var controller = new CustomFormsController(mockFormService.Object);
            var form = new CustomForm
            {
                Id = 1,
                Title = "Sample Form",
                Description = "Test Form",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Fields = new List<CustomField>()
            };

            mockFormService.Setup(service => service.CreateFormAsync(It.IsAny<CustomForm>())).ReturnsAsync(form);

            // Act
            var result = await controller.CreateForm(form);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdForm = Assert.IsType<CustomForm>(createdAtActionResult.Value);
            Assert.Equal(form.Id, createdForm.Id);
        }

        [Fact]
        public async Task GetFormById_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var mockFormService = new Mock<ICustomFormService>();
            var controller = new CustomFormsController(mockFormService.Object);
            var form = new CustomForm
            {
                Id = 1,
                Title = "Sample Form",
                Description = "Test Form",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Fields = new List<CustomField>()
            };

            mockFormService.Setup(service => service.GetFormByIdAsync(1)).ReturnsAsync(form);

            // Act
            var result = await controller.GetFormById(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedForm = Assert.IsType<CustomForm>(okObjectResult.Value);
            Assert.Equal(form.Id, returnedForm.Id);
        }

        [Fact]
        public async Task GetAllForms_ReturnsOkObjectResult()
        {
            // Arrange
            var mockFormService = new Mock<ICustomFormService>();
            var controller = new CustomFormsController(mockFormService.Object);
            var forms = new List<CustomForm>
            {
                new CustomForm { Id = 1, Title = "Form 1", Description = "Test Form 1", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow, Fields = new List<CustomField>() },
                new CustomForm { Id = 2, Title = "Form 2", Description = "Test Form 2", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow, Fields = new List<CustomField>() }
            };

            mockFormService.Setup(service => service.GetAllFormsAsync()).ReturnsAsync(forms);

            // Act
            var result = await controller.GetAllForms();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedForms = Assert.IsAssignableFrom<IEnumerable<CustomForm>>(okObjectResult.Value);
            Assert.Equal(forms.Count, returnedForms.Count()); // Ensure to call Count() to get the actual count
        }

        [Fact]
        public async Task UpdateForm_ValidIdAndForm_ReturnsOkObjectResult()
        {
            // Arrange
            var mockFormService = new Mock<ICustomFormService>();
            var controller = new CustomFormsController(mockFormService.Object);
            var form = new CustomForm
            {
                Id = 1,
                Title = "Sample Form",
                Description = "Test Form",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Fields = new List<CustomField>()
            };

            mockFormService.Setup(service => service.UpdateFormAsync(It.IsAny<CustomForm>())).ReturnsAsync(form);

            // Act
            var result = await controller.UpdateForm(1, form);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var updatedForm = Assert.IsType<CustomForm>(okObjectResult.Value);
            Assert.Equal(form.Id, updatedForm.Id);
        }

        [Fact]
        public async Task DeleteForm_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var mockFormService = new Mock<ICustomFormService>();
            var controller = new CustomFormsController(mockFormService.Object);

            mockFormService.Setup(service => service.DeleteFormAsync(1)).ReturnsAsync(true);

            // Act
            var result = await controller.DeleteForm(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
