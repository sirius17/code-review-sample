using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace ApiRelay
{
    public class RelayService
    {
        public Stream RelayAPI(RelayRequest request)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            using (APIClient apiClient = new APIClient(request.Url))
            {

                apiClient.AddHeaders(request.Headers);

                switch (request.Method)
                {
                    case "GET":
                        responseMessage = apiClient.GetAsync().Result;
                        break;

                    case "POST":
                        responseMessage = apiClient.PostAsync(JToken.Parse(request.body)).Result;
                        break;

                    case "PUT":
                        responseMessage = apiClient.PutAsync(JToken.Parse(request.body)).Result;
                        break;

                    case "DELETE":
                        responseMessage = apiClient.DeleteAsync().Result;
                        break;

                    default:
                        break;
                }
            }

            using (var ms = new MemoryStream())
            {
                return responseMessage.Content.ReadAsStreamAsync().Result;
            }
        }
    }
}
