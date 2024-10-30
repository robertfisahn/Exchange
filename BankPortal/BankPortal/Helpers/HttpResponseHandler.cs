using Newtonsoft.Json;
using System.Net;
using BankPortal.Exceptions;

namespace BankPortal.Helpers
{
    public class HttpResponseHandler
    {
        public void HandleErrorResponse(HttpResponseMessage response)
        {
            var errorMessage = response.Content.ReadAsStringAsync().Result;

            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorMessage);

            if (errorResponse != null && errorResponse.ContainsKey("error"))
            {
                errorMessage = errorResponse["error"];
            }
            throw response.StatusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestException(errorMessage),
                HttpStatusCode.NotFound => new NotFoundException(errorMessage),
                _ => new Exception("Something went wrong" + errorMessage)
            };
        }
    }
}
