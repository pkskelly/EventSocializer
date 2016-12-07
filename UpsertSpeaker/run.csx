using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.WebJobs.Host;

public static Task<HttpResponseMessage> Run(HttpRequestMessage req, IAsyncCollector<string> speakersOut, TraceWriter log)
{
    log.Info(string.Format("C# HTTP trigger function processed a request. {0}", req.RequestUri));

    dynamic data = await req.Content.ReadAsAsync<object>();

    HttpResponseMessage res = null;
    string twitterHandle = data?.twitterHandle;
    if (!string.IsNullOrEmpty(twitterHandle))
    {
        speakersOut.AddAsync(data);
        res = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(string.Empty)
        };
    }
    else
    {
        res = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Please pass a valid speaker.")
        };
    }

    return Task.FromResult(res);
}