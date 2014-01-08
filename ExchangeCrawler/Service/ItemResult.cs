using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeCrawler.Service
{
    class ItemResult
    {

        public String Id { get; set; }
        public String Sender { get; set; }
        public String Subject { get; set; }
        public DateTime ItemDate { get; set; }

    }
}
