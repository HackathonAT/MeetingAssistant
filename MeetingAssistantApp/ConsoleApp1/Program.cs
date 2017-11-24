using System;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            for (int i=0; i < 10; i++)
            {
                ManualResetEvent resetEvent = new ManualResetEvent(false);
                Parser.MakeRequest("So dark the con of man.", resetEvent);
                Console.WriteLine("Hit ENTER to exit...");
                // Display the JSON result from LUIS

                resetEvent.WaitOne();
            }
        }

        
    }
}
