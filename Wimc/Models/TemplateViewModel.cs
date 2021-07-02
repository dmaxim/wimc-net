using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class TemplateViewModel
    {
        public TemplateViewModel() {}

        public TemplateViewModel(Resource resource, string content, string fullDefinition)
        {
            TemplateType = resource.ResourceType;
            Content = content;
            ResourceJson = resource.ResourceDefinition;
            FullDefinition = fullDefinition;

        }
        
        public string TemplateType { get; set; }
        
        public string Content { get; set; }
        
        public string ResourceJson { get; set; }
        
        public string FullDefinition { get;  }
    }
}