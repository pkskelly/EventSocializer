#r "Newtonsoft.Json"

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IAsyncCollector<string> speakersOut, TraceWriter log)
{
    log.Info(string.Format("C# HTTP trigger function processed a request. {0}", req.RequestUri));

    dynamic data = await req.Content.ReadAsStringAsync<object>();

    HttpResponseMessage res = null;
    string twitterHandle = data?.twitterHandle;
    if (!string.IsNullOrEmpty(twitterHandle))
    {
        var speaker = new Speaker(){FirstName= data?.firstName, LastName=data?.lastName, TwitterHandle=data?.twitterHandle };
        speakersOut.AddAsync(JsonConvert.SerializeObject(speaker);
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

    return res;
}

public class Speaker
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TwitterHandle { get; set; }

}