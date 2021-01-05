using Cqrs.Domain.Interfaces;
using Cqrs.Domain.Models;
using Cqrs.Infra.CrossCutting.Configuration;
using Cqrs.Infra.Data.Base;
using Microsoft.Extensions.Options;

namespace Cqrs.Infra.Data
{
    public class AlertPriceRepository : BaseRepository<AlertPrice>, IAlertPriceRepository
    {
        public override string TableName { get => "AlertPrice"; }

        public AlertPriceRepository(IOptions<SqlServerSettings> config) : base(config.Value.ConnectString)
        {
        }


    }
}
