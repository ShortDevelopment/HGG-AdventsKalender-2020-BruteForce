using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace HGG_AdventsKalender_2020
{
    class Program
    {
        static void Main(string[] args)
        {

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            Console.WriteLine("Start ...");

            var t = new Thread(() => {
                int iterations = 30;
                for (int i = 1; i <= iterations; i++)
                {
                    MakeRequest1($"http://www.hgg-markgroeningen.de/pages/hgg/images/stories/advent2020/Bild{i}.jpg");
                    MakeRequest1($"http://www.hgg-markgroeningen.de/pages/hgg/images/stories/advent2020/Bild{i}a.jpg");
                    MakeRequest1($"http://www.hgg-markgroeningen.de/pages/hgg/images/stories/advent2020/Bild{i}b.jpg");
                    MakeRequest1($"http://www.hgg-markgroeningen.de/pages/hgg/images/stories/advent2020/text{i}.jpg");
                }
            });
            t.IsBackground = true;
            t.Start();

            Application.Run();
            
        }
        static void MakeRequest1(string url)
        {
            var req = HttpWebRequest.Create(url);
            req.Proxy = null;
            req.BeginGetResponse(state =>
            {
                try
                {
                    using (HttpWebResponse resp = (HttpWebResponse)req.EndGetResponse(state))
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine(resp.ResponseUri.ToString());
                        }
                    }
                }
                catch (Exception)
                {

                }
            }, null);
        }
        static async void MakeRequest2(string url)
        {
            var client = new HttpClient();
            //client.MaxResponseContentBufferSize = 1;
            var resp = await client.GetAsync(url);
            Console.WriteLine(resp.StatusCode.ToString());
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine(resp.RequestMessage.RequestUri.ToString());
            }
        }
    }
}