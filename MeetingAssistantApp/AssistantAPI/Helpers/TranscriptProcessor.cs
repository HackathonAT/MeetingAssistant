using AssistantAPI.Classes;
using GemBox.Document;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AssistantAPI.Helpers
{
    public static class TranscriptProcessor
    {
        /// <summary>
        /// Processes the video brakdown as json and returns a meeting protocol as string
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string ProcessVideoBreakDown(string jsonString)
        {
            JToken token = JObject.Parse(jsonString);
            JObject obj = JObject.Parse(jsonString);
            List<TranscripctsData> transcriptsDataList = new List<TranscripctsData>();
           
            foreach (var pair in obj)
            {
                TranscripctsData temp = new TranscripctsData();
                temp.key = pair.Key.ToString();
                temp.data = pair.Value.ToString();
                transcriptsDataList.Add(temp);

                foreach (var item in pair.Value)
                {
                    temp.key = item.ToString();
                    temp.data = item.ToString();
                    transcriptsDataList.Add(temp);
                }
                 
            }

            createWordFromList(transcriptsDataList);

            return transcriptsDataList.ToString();
        }

        public static void createWordFromList(List<TranscripctsData> list)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
            DocumentModel document = new DocumentModel();

            foreach (var item in list)
            {
           
                Section section = new Section(document);
                document.Sections.Add(section);

                Paragraph paragraph = new Paragraph(document);
                section.Blocks.Add(paragraph);

                Run run = new Run(document, item.ToString());
                paragraph.Inlines.Add(run);

                
            }

            document.Save("Transscript.docx");


        }

        public static List<string> GetCommands(string jsonString)
        {
            throw new NotImplementedException();
        }
    }
}