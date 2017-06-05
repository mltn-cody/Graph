using System;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Lexicon.Graph.Persistance
{
    public class Neo4JPersistence : INeo4JPersistence
    {
        private readonly IConfiguration _config;
        private INeo4JGraphClient _client;
        private readonly ILogger _logger;
        private IDriver _driver; 
        public INeo4JGraphClient Persistence => _client;

        public Neo4JPersistence(IConfiguration config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task Prepare()
        {
            _logger.Debug("Started - Preparing Neo4j persistence");

            var uri = new Uri(_config["Neo4jConnectionUrl"].ToString());
            _driver =
                GraphDatabase.Driver(uri,
                                     AuthTokens.Basic(_config.GetConfigValueAs<string>("Username"), _config.GetConfigValueAs<string>("Password")),
                                     Config.Builder.WithEncryptionLevel(EncryptionLevel.Encrypted).ToConfig());

            _logger.Debug("Completed - Perparing Neo4j persistence");
        }

        /// <summary>
        /// This method is used to make session connections to the Neo4j Instance
        /// </summary>
        public ISession CreateSession()
        {
            return _driver.Session();
        }

        public ITransaction CreateTransaction()
        {
            return CreateSession().BeginTransaction();
        }

        public void EnsureLabelExists<TStorable>() where TStorable : QueryStorable
        {
            throw new NotImplementedException();
        }

        public Task EnsureLabelExistsAsync<TStorable>() where TStorable : QueryStorable
        {
            throw new NotImplementedException();
        }

        public Task Reset()
        {
            throw new NotImplementedException();
        }

    }
}
