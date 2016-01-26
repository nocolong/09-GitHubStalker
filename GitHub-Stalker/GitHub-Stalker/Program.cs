using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Stalker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a username");

            string username = Console.ReadLine();

            WebClient wc = new WebClient();

            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            string json = wc.DownloadString("https://api.github.com/users/" + username);

            var o = JObject.Parse(json);

            Console.WriteLine(o["login"].ToString());
            Console.WriteLine(o["login"].ToString() + " has " + o["followers"].ToString() + " followers " + o["public_repos"].ToString() + " repositories");

            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            json = wc.DownloadString(o["repos_url"].ToString());

            var oRepos = JArray.Parse(json);

            foreach (var repo in oRepos)
            {
                Console.WriteLine(repo["name"] + " " + repo["has_issues"].ToString());
            }

            Console.ReadLine();
        }
    }
}
