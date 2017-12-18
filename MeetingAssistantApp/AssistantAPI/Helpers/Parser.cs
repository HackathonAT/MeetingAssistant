using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using AssistantAPI.Helpers;
using Newtonsoft.Json;

public class Parser
{
    public static void ExecuteCommand(String conversationSnippet)
    {
        ManualResetEvent resetEvent = new ManualResetEvent(false);
        Parser.MakeRequest(conversationSnippet, resetEvent);
        resetEvent.WaitOne();
    }

    public static async void MakeRequest(string conversationSnippet, ManualResetEvent resetEvent)
    {
        var client = new HttpClient();
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        // This app ID is for a public sample app that recognizes requests to turn on and turn off lights
        var luisAppId = "xxxxx";
        var subscriptionKey = "xxxxx";

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
        LUISResponse lUISResponse = JsonConvert.DeserializeObject<LUISResponse>(await response.Content.ReadAsStringAsync());
        

        ExecuteRequest(lUISResponse);
       
        resetEvent.Set();
    }

   static bool ExecuteRequest(LUISResponse lUISResponse)
    {
        if (lUISResponse == null)
        {
            return false;
        }

        IDictionary<string,string> entities = new Dictionary<string, string>();
        for (int i = 0; i < lUISResponse.entities.Length; i++)
        {
            string value = lUISResponse.entities[i].entity;
            if (String.Compare(lUISResponse.entities[i].type, "builtin.datetimeV2.date", true) == 0)
            {
                entities["Date"] = lUISResponse.entities[i].resolution?.value;
            }
            else if (String.Compare(lUISResponse.entities[i].type, "builtin.datetimeV2.timerange", true) == 0)
            {
                entities["Start"] = lUISResponse.entities[i].resolution?.start;
                entities["End"] = lUISResponse.entities[i].resolution?.end;
            }
            else
            {
                entities[lUISResponse.entities[i].type] = value;
            }
        }

        if (String.Compare(lUISResponse?.topScoringIntent?.intent, "To-Do-Note", true) == 0) {
            if (String.Compare(entities["Person"],"franz", true) == 0)
            {
                entities["Date"] = "2018-01-04";
            } else
            {
                entities["Date"] = "2017-11-24";
            }
            DateTime date;
            var res = DateTime.TryParse(entities["Date"], out date);
                CommandsHelper. AddTask(entities["Person"], res? date:DateTime.Now, entities["Action"] );
                return true; ;
        } else if (String.Compare(lUISResponse?.topScoringIntent?.intent, "Calendar.Add", true) == 0)
        {
            //ScheduleEvent(entities["Calendar.Subject"], entities["Date"], entities["Start"], entities["End"], entities["location"]);
            try
            {
                CommandsHelper.ScheduleEvent(entities["Calendar.Subject"], new DateTime(2017, 11, 27), entities["Calendar.Location"]);
            }
            catch (Exception)
            {

                //ignore
            }
            return true;
        }
        else if (String.Compare(lUISResponse?.topScoringIntent?.intent, "None", true) == 0)
        {
            return true;
        }
        return false;
    }
}
