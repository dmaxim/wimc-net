using System.Collections.Generic;
using Mx.Library.Serialization;
using Newtonsoft.Json.Linq;
using Wimc.Domain.Models;

namespace Wimc.Business.Builders
{
    public static class ResourceContainerBuilder
    {
        public static ResourceContainer BuildFromUpload(string name, string containerJson)
        {
            var newContainer = new ResourceContainer
            {
                ContainerName = name,
                RawJson =  containerJson
                
            };


            var resources = new List<Resource>();
            var resourceArray = JArray.Parse(containerJson);
            foreach (var resourceObject in resourceArray)
            {
                var resourceJson = resourceObject.ToJson();
                var azureResource = resourceJson.DeserializeJson<AzureResource>();
                resources.Add(new Resource(azureResource, resourceJson));
            }

            newContainer.Resources = resources;

            return newContainer;
        }

        public static ResourceContainer BuildFromApi(string name, string containerJson)
        {
            var newContainer = new ResourceContainer
            {
                ContainerName = name,
            };


            var resources = new List<Resource>();
            var resourceContainer = JObject.Parse(containerJson);
            var resourceArray = (JArray) resourceContainer["value"];

            newContainer.RawJson = resourceArray.ToString();
            foreach (var resourceObject in resourceArray)
            {
                var resourceJson = resourceObject.ToJson();
                var azureResource = resourceJson.DeserializeJson<AzureResource>();
                resources.Add(new Resource(azureResource, resourceJson));
            }

            newContainer.Resources = resources;

            return newContainer;
        }

        public static IList<Resource> BuildResources(string resourcesJson)
        {
            var resources = new List<Resource>();
            var resourceContainer = JObject.Parse(resourcesJson);
            var resourceArray = (JArray) resourceContainer["value"];

            foreach (var resourceObject in resourceArray)
            {
                var resourceJson = resourceObject.ToJson();
                var azureResource = resourceJson.DeserializeJson<AzureResource>();
                resources.Add(new Resource(azureResource, resourceJson));
            }

            return resources;
        }

        public static Resource BuildResource(string resourceJson)
        {
            return new Resource(resourceJson.DeserializeJson<AzureResource>(), resourceJson);
        }
        
        
    }
}