namespace Wimc.Models
{
    public class ResourceTemplateViewModel
    {
        public ResourceTemplateViewModel(string name, string template)
        {
            ResourceName = name;
            Template = template;
        }
        
     
        public string ResourceName { get; }
        
        public string Template { get; set; }
    }
}