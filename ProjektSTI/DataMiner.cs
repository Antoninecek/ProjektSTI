using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjektSTI
{
    class DataMiner
    {
        public DataMiner()
        {
            AdresaServer = "http://api.github.com";
            Repozitar = "TEST";
            Uzivatel = "Antoninecek";

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var txt = System.IO.File.ReadAllText("F:\\STI\\ProjektSTI\\ProjektSTI\\config.json");
            Nastaveni n = JsonConvert.DeserializeObject<Nastaveni>(txt);
            Token = n.githubToken;

        }
        public string AdresaServer { get; set; }
        public string Repozitar { get; set; }
        public string Uzivatel { get; set; }
        public string Token { get; set; }

        public string UdelejRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/vnd.github.v3+json";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0";
            request.Headers.Add("Authorization", "token " + Token);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        public List<RootObject> VratSouborySlozky(RootObject ro)
        {
            string odpoved = UdelejRequest(ro.url);
            return JsonConvert.DeserializeObject<RootObject[]>(odpoved).ToList();
        }

        public List<Zaznam> VratCommity()
        {
            string odpoved = UdelejRequest(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits");
            return JsonConvert.DeserializeObject<Zaznam[]>(odpoved).ToList();
        }

        public DetailZaznamu VratDetailCommitu(string sha)
        {
            var odpoved = UdelejRequest(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits/" + sha);
            return JsonConvert.DeserializeObject<DetailZaznamu>(odpoved);
        }

        public List<RootObject> VratSoubory()
        {
            string odpoved = UdelejRequest(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/contents");
            return JsonConvert.DeserializeObject<RootObject[]>(odpoved).ToList();
        }

    }

    public class DetailZaznamu
    {
        public string sha { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Author author { get; set; }
        public Committer committer { get; set; }
        public Parent[] parents { get; set; }
        public Stats stats { get; set; }
        public File[] files { get; set; }
    }

    public class Zaznam
    {
        public string sha { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }

        /// <summary>
        /// Vraci vyber z listu zaznamu, ktery bude pozdejsiho data
        /// </summary>
        /// <param name="cas">okamzik, od ktereho se zacne filtrovat</param>
        /// <param name="zaznamy">list, ze ktereho probiha filtrace</param>
        /// <returns></returns>
        public static List<Zaznam> SelektujCasovouPeriodu(List<Zaznam> zaznamy, DateTime cas)
        {
            return zaznamy.Where(c => c.commit.committer.date > cas).ToList();
        }

        public static void VratSouboryZaznamu(List<Zaznam> zaznamy)
        {
            foreach (var z in zaznamy)
            {

            }
        }
    }

    public class Author
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
    }

    public class Committer
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
    }

    public class Tree
    {
        public string sha { get; set; }
        public string url { get; set; }
    }

    public class Verification
    {
        public bool verified { get; set; }
        public string reason { get; set; }
        public object signature { get; set; }
        public object payload { get; set; }
    }

    public class Commit
    {
        public Author author { get; set; }
        public Committer committer { get; set; }
        public string message { get; set; }
        public Tree tree { get; set; }
        public string url { get; set; }
        public int comment_count { get; set; }
        public Verification verification { get; set; }
    }

    public class Author2
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Committer2
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Parent
    {
        public string sha { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
    }

    public class RootObject
    {
        public string sha { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Author2 author { get; set; }
        public Committer2 committer { get; set; }
        public List<Parent> parents { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public int size { get; set; }
        public string git_url { get; set; }
        public string download_url { get; set; }
        public string type { get; set; }
        public Links _links { get; set; }

        public static List<RootObject> SelektujSouboryPodleKoncovky(List<RootObject> soubory, string koncovka)
        {
            var regex = new Regex(@".*." + koncovka + "$");
            return soubory.Where(x => regex.IsMatch(x.path.ToLower())).ToList();
        }

        public static List<RootObject> VratSouboryUrcitehoTypuRepozitare(string typ)
        {
            var vsechnySoubory = VratVsechnySouboryRepozitareRekurzivne();
            var vybraneSoubory = SelektujSouboryPodleKoncovky(vsechnySoubory, typ);
            return vybraneSoubory;
        }

        private static List<RootObject> VratVsechnySouboryRepozitareRekurzivne()
        {
            DataMiner dm = new DataMiner();
            var rootSouboryRepozitare = dm.VratSoubory();
            List<RootObject> vsechnySoubory = new List<RootObject>();
            foreach (var sr in rootSouboryRepozitare)
            {
                if (sr.type == "dir")
                {
                    vsechnySoubory.AddRange(VratRekurzivneVsechnySoubory(sr));
                }
                else
                {
                    vsechnySoubory.Add(sr);
                }
            }
            return vsechnySoubory;
        }

        private static List<RootObject> VratRekurzivneVsechnySoubory(RootObject ro)
        {
            DataMiner dm = new DataMiner();
            List<RootObject> soubory = new List<RootObject>();
            var souborySlozky = dm.VratSouborySlozky(ro);
            foreach (var ss in souborySlozky)
            {
                if (ss.type != "dir")
                {
                    soubory.Add(ss);
                }
                else
                {
                    soubory.AddRange(VratRekurzivneVsechnySoubory(ss));
                }
            }
            return soubory;
        }
    }

    public class Links
    {
        public string self { get; set; }
        public string git { get; set; }
        public string html { get; set; }
    }


    public class Stats
    {
        public int total { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
    }

    public class File
    {
        public string sha { get; set; }
        public string filename { get; set; }
        public string status { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changes { get; set; }
        public string blob_url { get; set; }
        public string raw_url { get; set; }
        public string contents_url { get; set; }
        public string patch { get; set; }
    }

}
