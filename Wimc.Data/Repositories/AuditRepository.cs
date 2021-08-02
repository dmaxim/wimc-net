using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Mx.Library.Serialization;
using Wimc.Domain.AppConfiguration;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AzureTableConfiguration _configuration;
        public AuditRepository(AzureTableConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<List<Resource>> GetNewResources()
        {
            var client = GetClient();
            
            var tableEntities =
                client.Query<TableEntity>(
                    $"PartitionKey gt '0'").ToList();

            var resources = ParseNewResources(tableEntities);
            return Task.FromResult(resources);

        }

        private static List<Resource> ParseNewResources(List<TableEntity> tableEntities)
        {
            var resources = new List<Resource>();

            foreach (var tableEntity in tableEntities)
            {
                var resourceAddedJson = tableEntity["ResourceAdded"].ToString();
                var newResource = resourceAddedJson.DeserializeJson<Resource>();
                if (newResource != null)
                {
                    resources.Add(newResource);
                }
            }

            return resources;
        }

        private TableClient GetClient()
        {
            return new TableClient(
                new Uri(_configuration.EndpointUrl),
                _configuration.Name,
                new TableSharedKeyCredential(_configuration.Account, _configuration.AccountKey)
            );

        }
    }
}