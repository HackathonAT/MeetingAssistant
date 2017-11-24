using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

public class Parser
{
    public static async void MakeRequest(string conversationSnippet, ManualResetEvent resetEvent)
    {
        var client = new HttpClient();
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        // This app ID is for a public sample app that recognizes requests to turn on and turn off lights
        var luisAppId = "3ab8a4ab-7890-4837-9394-70d9d5d3769b";
        var subscriptionKey = "68e86676246241edae67e7b9d7fbe9fd";

        // The request header contains your subscription key
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

        // The "q" parameter contains the utterance to send to LUIS
        queryString["q"] = conversationSnippet;

        // These optional request parameters are set to their default values
        queryString["timezoneOffset"] = "0";
        queryString["verbose"] = "false";
        queryString["spellCheck"] = "false";
        queryString["staging"] = "false";

        var uri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;
        var response = await client.GetAsync(uri);

        var strResponseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine(strResponseContent.ToString());
        Console.ReadLine();
        resetEvent.Set();
    }
}
