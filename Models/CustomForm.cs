namespace CustomFormsApp.Models
{
    public class CustomForm
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public required List<CustomField> Fields { get; set; }

        public CustomForm()
        {
            Fields = new List<CustomField>();
        }
        
    }
}