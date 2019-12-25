using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace AppCashback.MongoDb
{
    internal class ConexaoMongoDb
    {
        public IMongoDatabase DataBase { get; }

        public ConexaoMongoDb(IOptions<ConfigMongoDb> config, ILogger<ConexaoMongoDb> logger)
        {
            var mongoConnectionUrl = new MongoUrl(config.Value.Host);
            var mongoSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            mongoSettings.ClusterConfigurator = cb =>
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    var jsonSettings = new JsonWriterSettings
                    {
                        Indent = true,
                        OutputMode = JsonOutputMode.Shell,
                    };

                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        if (e.DatabaseNamespace.DatabaseName != "admin")
                        {
                            logger.LogDebug("{Command}", e.Command.ToJson(jsonSettings));
                        }
                    });
                }
            };

            var client = new MongoClient(mongoSettings);
            DataBase = client.GetDatabase(config.Value.Database);
        }
    }
}
