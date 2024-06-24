using System;
using System.Collections.Generic;
using Xunit;
using CustomFormsApp.Models;

namespace Tests
{
    public class CustomFormTests
    {
        [Fact]
        public void CustomForm_Initialization_ShouldWork()
        {
            // Arrange & Act
            var customForm = new CustomForm
            {
                Id = 1,
                Title = "Sample Form",
                Description = "This is a sample form.",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Fields = new List<CustomField>()
            };

            // Assert
            Assert.Equal(1, customForm.Id);
            Assert.Equal("Sample Form", customForm.Title);
            Assert.Equal("This is a sample form.", customForm.Description);
            Assert.NotNull(customForm.DateCreated);
            Assert.NotNull(customForm.DateUpdated);
            Assert.NotNull(customForm.Fields);
            Assert.Empty(customForm.Fields); // Ensure fields are initially empty
        }
    }
}
