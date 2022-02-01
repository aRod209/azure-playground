using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionAppService
{
    public class Function1
    {
        /// <summary>
        /// Runs the Azure Function.
        /// </summary>
        /// <param name="req">The <see cref="HttpRequestData"/>.</param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string name)
        {
            HttpResponseData response;

            if (string.IsNullOrEmpty(name))
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString($"The parameter {nameof(name)} cannot be null or empty.");

                return response;
            }

            response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"{name}, welcome to Azure Functions!");

            return response;
        }
    }
}
