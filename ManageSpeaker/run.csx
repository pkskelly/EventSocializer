using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.WebJobs.Host;

public static Task<HttpResponseMessage> Run(HttpRequestMessage req, IAsyncCollector<string> speakers, TraceWriter log)
{
    var queryParams = req.GetQueryNameValuePairs()
        .ToDictionary(p => p.Key, p => p.Value, StringComparer.OrdinalIgnoreCase);

    log.Info(string.Format("C# HTTP trigger function processed a request. {0}", req.RequestUri));

    HttpResponseMessage res = null;
    string name;
    if (queryParams.TryGetValue("name", out name))
    {
        speakers.AddAsync(name);
        res = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Hello2 " + name)
        };
    }
    else
    {
        res = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Please pass a name on the query string")
        };
    }

    return Task.FromResult(res);
}