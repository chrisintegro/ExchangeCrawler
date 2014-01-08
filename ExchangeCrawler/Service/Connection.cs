using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
using System.Net;

namespace ExchangeCrawler.Service
{
   

    public class Connection
    { 
        private String email;
        private SecureString pwd;
        private String impersonationEmail;
        private ExchangeService service;

        private Connection(String email, SecureString pwd, String impersonationEmail)
        {
            this.email = email;
            this.pwd = pwd;
            this.impersonationEmail = impersonationEmail;
        }

        public static Connection GetConnection(String email, SecureString pwd)
        {
            Connection con = new Connection(email, pwd, email);

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1, TimeZoneInfo.Local);
            service.Credentials = new NetworkCredential(con.email, con.pwd);

            ImpersonatedUserId impersonatedUserId =
              new ImpersonatedUserId(ConnectingIdType.SmtpAddress, con.impersonationEmail);

            service.ImpersonatedUserId = impersonatedUserId;

            service.AutodiscoverUrl(email, RedirectionUrlValidationCallback);

            con.service = service;

            return con;
        }

        public Crawler GetCrawler()
        {
            Crawler crawler = new Crawler(this.service);

            return crawler;
        }


        // The following is a basic redirection validation callback method. It 
        // inspects the redirection URL and only allows the Service object to 
        // follow the redirection link if the URL is using HTTPS. 
        //
        // This redirection URL validation callback provides sufficient security
        // for development and testing of your application. However, it may not
        // provide sufficient security for your deployed application. You should
        // always make sure that the URL validation callback method that you use
        // meets the security requirements of your organization.
        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }

            return result;
        }

    }
}
