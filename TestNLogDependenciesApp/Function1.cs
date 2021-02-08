using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestNLogDependenciesApp
{
    public static class Function1
    {
        private static HttpClient httpClient = new HttpClient();
        [FunctionName("Function1")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                Uri uri = new Uri("http://ptsv2.com/t/iiq4u-1610745983/post");

                // Construct the JSON to post.
                StringContent content = new StringContent(
                    "{ \"firstName\": \"Eliot\" }",
                    UnicodeEncoding.UTF8,
                    "application/json");



                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                    uri,
                    content);



                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                log.LogInformation(httpResponseBody);
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                log.LogInformation(ex.ToString());
            }
        }
    }
}
