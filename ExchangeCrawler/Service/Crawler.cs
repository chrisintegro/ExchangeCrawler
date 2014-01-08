using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeCrawler.Service
{
    class Crawler
    {
 
        private ExchangeService service;

        public Crawler(ExchangeService service)
        {
            this.service = service;
        }

        public List<ItemResult> Crawl(String rootFolderPath, bool deepTraversal)
        {
            List<ItemResult> result = new List<ItemResult>();
            return result;

        }
    }
}
