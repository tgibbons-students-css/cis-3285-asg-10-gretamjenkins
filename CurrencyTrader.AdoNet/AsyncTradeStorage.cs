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
        private readonly ILogger logger;
        private ITradeStorage syncTradeStorage;

        public AsyncTradeStorage(ILogger logger)
        {
            this.logger = logger;
            syncTradeStorage = new AdoNetTradeStorage(logger);

        }

        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Starting synchronous trade storage");
            Task.Run(() => syncTradeStorage.Persist(trades));
        }
    }
}
