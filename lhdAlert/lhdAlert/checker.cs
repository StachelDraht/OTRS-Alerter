using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using lhdAlert.Properties;
using Newtonsoft.Json.Linq;


namespace lhdAlert
{
    class checker
    {
        HttpWebResponse response;
        Stream resStream;
        string ticketId = null;
        string ticketTitle;

        public void check(object obj)
        {
            objClass objclass = new objClass();
            objclass = (objClass)obj;

            string sURL = Settings.Default.dataAddr;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sURL);

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US))";
            


            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                //logFile.WriteLine(e);
                System.Console.WriteLine(e);
            }

            try
            {
                resStream = response.GetResponseStream();
            }
            catch(Exception e)
            {
                //logFile.WriteLine(e);
                System.Console.WriteLine(e);
            }

            try
            {
                using (StreamReader objReader = new StreamReader(resStream))
                {
                    // parse json
                    string json = objReader.ReadToEnd();
                    JObject o = JObject.Parse(json);
                    ticketTitle = (string)o["title"];
                    
                    System.Console.WriteLine(ticketTitle+" and "+ticketId);


                    string str = objReader.ReadToEnd();
                    Label lable = (Label)objclass.lable;
                    NotifyIcon notify = (NotifyIcon)objclass.notify;

                    if (ticketId != (string)o["count"] && ticketId != null)
                    {
                        try
                        {
                            lable.Invoke(new MethodInvoker(delegate { playAlert(); notify.BalloonTipText = ticketTitle; notify.ShowBalloonTip(500); }));
                        }
                        catch (Exception e)
                        {
                            //logFile.WriteLine(e.ToString());
                            System.Console.WriteLine(e);
                        }
                    }
                    ticketId = (string)o["count"];
                    try
                    {
                        Thread.Sleep(1000);
                        // in this place was error
                        lable.Invoke(new MethodInvoker(delegate { lable.Text = (string)o["count"]; }));
                    }
                    catch (Exception e)
                    {
                        //logFile.WriteLine(e);
                        System.Console.WriteLine(e.ToString());
                    }

                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            Thread.Sleep(20000);
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            GC.Collect();
            check(obj);
        }

        public void playAlert()
        {
            Media.Player.GetPlayer().Play(Settings.Default.sound);
        }
    }
}
