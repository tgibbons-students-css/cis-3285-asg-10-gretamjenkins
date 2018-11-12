using CurrencyTrader.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTrader.AdoNet
{
    public class AsyncTradeStorage : ITradeStorage
    {
        // Local variables to store information and facilitate trade storage process
        private readonly ILogger logger;
        private ITradeStorage syncTradeStorage;

        /// <summary>
        /// Constructor for async storage. Creates a new logger and synchronous trade object
        /// </summary>
        /// <param name="logger"></param>
        public AsyncTradeStorage(ILogger logger)
        {
            this.logger = logger;
            syncTradeStorage = new AdoNetTradeStorage(logger);
        }

        /// <summary>
        /// Persists the trade data through to the sync object through a separate, asynchronous process
        /// </summary>
        /// <param name="trades"></param>
        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Starting synchronous trade storage");
            Task.Run(() => syncTradeStorage.Persist(trades));
        }
    }
}
