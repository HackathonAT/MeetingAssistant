using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace VideoHelper
{

    class
        Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "XXXXXXX");

            // Request parameters
            queryString["name"] = "Testvideo";
            queryString["privacy"] = "Private";
            // queryString["videoUrl"] = "file:///c:/Users/ddudulea/Pictures/Camera Roll/test.mp4";
            queryString["language"] = "de-DE";
            queryString["externalId"] = "Test1234";
            /*  queryString["metadata"] = "{string}";
              queryString["description"] = "{string}";
              queryString["partition"] = "{string}";
              queryString["callbackUrl"] = "{string}";
              queryString["indexingPreset"] = "{string}";
              queryString["streamingPreset"] = "{string}";*/
            var uri = "https://videobreakdown.azure-api.net/Breakdowns/Api/Partner/Breakdowns?" + queryString;

            //HttpResponseMessage response;

            //// Request body
            //byte[] byteData = File.ReadAllBytes("c:/Users/ddudulea/Pictures/Camera Roll/test.mp4");

            //using (var content = new ByteArrayContent(byteData))
            //{
            //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    response = await client.PostAsync(uri, content);
            //}

            MultipartFormDataContent Content2 = new MultipartFormDataContent();
            var filePath = "c:/Users/ddudulea/Pictures/Camera Roll/test.mp4";
            FileStream fs = File.OpenRead(filePath);
            var streamContent = new StreamContent(fs);

            //HttpContent metaDataContent = new ByteArrayContent(byteData);
            var videoContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
            videoContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            Content2.Add(videoContent, "video", Path.GetFileName(filePath));
            var response = await client.PostAsync(uri, Content2);

            string resultContent = await response.Content.ReadAsStringAsync();

            //------------------------
            queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
          

            // Request parameters
            queryString["language"] = "en-US";
            uri = string.Format("https://videobreakdown.azure-api.net/Breakdowns/Api/Partner/Breakdowns/{0}/VttUrl?{1}",
                resultContent.Substring(1, resultContent.Length - 2), queryString);

            response = await client.GetAsync(uri);

            resultContent = await response.Content.ReadAsStringAsync();

        }
    }
}
