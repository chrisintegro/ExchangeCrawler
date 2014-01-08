using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExchangeCrawler.Service;
using System.Security;

namespace ExchangeCrawler
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length < 2)
            {
                Console.WriteLine("You must supply the following arguments to run the Exchange Crawler:");
                Console.WriteLine("     Email: Address of impersonation ID");
                Console.WriteLine("     Password: Password for the impersonation ID");
                Console.WriteLine("     DeepTraversal: True or False");
                Console.WriteLine("     FolderPath: (Optional) The root folder to start the crawl, defaults to the mailbox root");
            }
            //Get email address of mailbox from command line
            String email = args[0];
            SecureString pwd = new SecureString();
            CharEnumerator pwdEnum = args[1].GetEnumerator();
            pwd.AppendChar(pwdEnum.Current);
            while(pwdEnum.MoveNext())
            {
                pwd.AppendChar(pwdEnum.Current);
            }
            pwdEnum.Dispose();
            pwd.MakeReadOnly();

            bool deepTraversal = false;
            if(args[2] != null)
            {
                 Boolean.TryParse(args[2], out deepTraversal);
            }
           
            String folderPath = "";
            if(args[3] != null)
            {
                folderPath = args[3];
            }
            //Get delegate account from command line
            Connection exCon = Connection.GetConnection(email, pwd);
            Crawler exCrawler = exCon.GetCrawler();
            List<ItemResult> crawlResults = exCrawler.Crawl("", true);
            foreach (ItemResult curResult in crawlResults)
            {
                Console.WriteLine("Item id: " + curResult.Id);
                Console.WriteLine("Item subject: " + curResult.Subject);
                Console.WriteLine("Item reference date: " + curResult.ItemDate);
                Console.WriteLine("");
            }
        }
    }
}
