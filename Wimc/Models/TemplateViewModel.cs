namespace Wimc.Models
{
    public class TemplateViewModel
    {
        public TemplateViewModel() {}

        public TemplateViewModel(string type, string content)
        {
            TemplateType = type;
            Content = content;
        }
        
        public string TemplateType { get; set; }
        
        public string Content { get; set; }
    }
}