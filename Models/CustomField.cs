namespace CustomFormsApp.Models
{
    public class CustomField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public required int Value { get; set; }

        public CustomField(string name)
        {
            Name = name;
        }
        
    }
}
