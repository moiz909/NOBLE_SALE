using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NOBLE_SALE.Helper
{
    class WebAPI
    {
        public string URL { get; set; }
        public HttpClient client { get; set; }
        public WebAPI()
        {
            URL = "https://" + WebApiSettings.IP + ":" + WebApiSettings.Port + "/api/";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            client = new HttpClient(clientHandler);
        }
    }
}
