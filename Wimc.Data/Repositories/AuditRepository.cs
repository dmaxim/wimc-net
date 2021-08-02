using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
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
            
            var result =
                client.Query<TableEntity>(
                    $"PartitionKey gt '0'").ToList();

            return Task.FromResult(new List<Resource>());

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