using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace AssistantAPI.Helpers
{
    public static class CommandsHelper
    {
        private static Event myEvent;

        private static Task myTask;
        public static void ScheduleEvent(string eventsubject, DateTime eventdate, string eventlocation)
        {
            var uri = "https://prod-63.westeurope.logic.azure.com:443/workflows/b6ded1401b20404380dcaa0a89a9e4ed/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=uzRlp3sjiUiX4kLz5E1XVOuNtJPf-znNQQgBDeRQlIg";
            if (myEvent == null)
            {
                myEvent = new Event {Subject = eventsubject, Location = eventlocation, Date = eventdate};
            }
            else
            {
                myEvent.Subject = eventsubject;
                myEvent.Date = eventdate;
                myEvent.Location = eventlocation;
            }
            string json = JsonConvert.SerializeObject(myEvent);
           var response =  HttpHelper.PostRequest(uri, json);
            
        }

        public static void AddTask(string recipient, DateTime deadline, string action)
        {
            var uri = "https://prod-59.westeurope.logic.azure.com:443/workflows/b2f5ef05a79146e9b9ad2544bd767bab/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=TrgW94sTp9Z4Shf6DuJ5yhaaa86DVKVK5QqWmD3SGRQ";
            if (myTask == null)
            {
                myTask = new Task{ Recipient = recipient,Action= action, Deadline = deadline};
            }
            else
            {
                myTask.Recipient = recipient;
                myTask.Action = action;
                myTask.Deadline = deadline;
            }
            string json = JsonConvert.SerializeObject(myTask);
            var response = HttpHelper.PostRequest(uri, json);

        }
    }

    public class Event
    {
        public string Subject;
        public string Location;
        public DateTime Date;
    }

    public class Task
    {
        public string Recipient;
        public string Action;
        public DateTime Deadline;
    }
}