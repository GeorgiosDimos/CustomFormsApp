using Xunit;
using CustomFormsApp.Models;

namespace Tests
{
    public class CustomFieldTests
    {
        [Fact]
        public void CustomField_Initialization_ShouldWork()
        {
            // Arrange & Act
            var customField = new CustomField("Sample Field")
            {
                Id = 1,
                Value = 123
            };

            // Assert
            Assert.Equal(1, customField.Id);
            Assert.Equal("Sample Field", customField.Name);
            Assert.Equal(123, customField.Value);
        }
    }
}
