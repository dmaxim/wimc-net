using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class TemplateViewModel
    {
        public TemplateViewModel() {}

        public TemplateViewModel(Resource resource, string content)
        {
            TemplateType = resource.ResourceType;
            Content = content;
            ResourceJson = resource.ResourceDefinition;
        }
        
        public string TemplateType { get; set; }
        
        public string Content { get; set; }
        
        public string ResourceJson { get; set; }
    }
}