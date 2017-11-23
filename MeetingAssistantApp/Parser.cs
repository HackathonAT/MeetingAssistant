using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

public class Parser
{

    static HttpClient client = new HttpClient();
    static string uri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/3ab8a4ab-7890-4837-9394-70d9d5d3769b?subscription-key=68e86676246241edae67e7b9d7fbe9fd&timezoneOffset=0&q=";

    /**
     * static constructor
     */
    static Parser()
	{
        // initialize client
        RunAsync();
    }

    static async Task RunAsync()
    {
        client.BaseAddress = new Uri("uri");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    static async string getCommand(string conversationSnippet)
    {
        string command = null;
        conversationSnippet = HttpUtility.UrlEncode(converstaionSnippet);
        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            return "success";
            //command = await response.Content.ReadAsAsync<Product>();
        }
        return "failure";
    }

    static void Main()
    {
        string result = getCommand("hallo");
        WriteLine(result);
    }
}
