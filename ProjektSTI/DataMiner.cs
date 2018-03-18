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
            Repozitar = "sklad";
            Uzivatel = "Antoninecek";
        }
        public string AdresaServer { get; set; }
        public string Repozitar { get; set; }
        public string Uzivatel { get; set; }

        public string UdelejRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.ContentType = "application/vnd.github.v3+json";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0";
            //request.Headers.Add("Authorization", "token " + Token);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        public string UdelejRequestGitHub(string url, Dictionary<string, string> parametry = null)
        {
            var txt = System.IO.File.ReadAllText("F:\\STI\\ProjektSTI\\ProjektSTI\\config.json");
            Nastaveni n = JsonConvert.DeserializeObject<Nastaveni>(txt);
            // github api si nekdy doplni nejakej parametr sam, potrebuju zjistit, jestli uz nejakej parametr existuje, abych mohl navazat
            string znak = url.Contains("?") ? "&" : "?";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + znak + "access_token=" + n.githubToken + "&" + PrevedSlovnikParametruNaString(parametry));
            request.ContentType = "application/vnd.github.v3+json";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0";
            //request.Headers["Time-Zone"] = "Europe/Prague";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        private string PrevedSlovnikParametruNaString(Dictionary<string, string> parametry)
        {
            if (parametry == null)
            {
                return "";
            }
            string text = "";
            foreach (var param in parametry)
            {
                text += param.Key + "=" + param.Value + "&";
            }
            return text;
        }

        public Decimal SpocitejVyskytRetezceSouboruUrl(string url, string retezec)
        {
            DataMiner dm = new DataMiner();
            var odpoved = dm.UdelejRequest(url);
            return Regex.Matches(odpoved, retezec).Count;
        }

        /// <summary>
        /// ziska z API soubory, ktere jsou pod slozkou
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        public List<RootObject> VratSouborySlozky(RootObject ro)
        {
            List<RootObject> vsechnySoubory = new List<RootObject>();
            int stranka = -1;
            List<RootObject> tmpSoubory = new List<RootObject>();
            Dictionary<string, string> nastaveni = new Dictionary<string, string>() { { "per_page", "50" }, { "page", stranka.ToString() } };
            do
            {
                int tmpStranka = int.Parse(nastaveni["page"]) + 1;
                nastaveni["page"] = tmpStranka.ToString();
                string odpoved = UdelejRequestGitHub(ro.url, nastaveni);
                vsechnySoubory.AddRange(JsonConvert.DeserializeObject<RootObject[]>(odpoved).ToList());
            } while (vsechnySoubory.Count / (int.Parse(nastaveni["page"]) + 1) == 50);
            return vsechnySoubory;
        }

        public List<string> CyklujRequesty(string url)
        {
            List<string> vsechnyOdpovedi = new List<string>();
            Dictionary<string, string> nastaveni = new Dictionary<string, string>() { { "per_page", "50" }, { "page", "0" } };
            string odpoved = "";
            while (odpoved != "[\n\n]\n")
            {
                int tmpStranka = int.Parse(nastaveni["page"]) + 1;
                nastaveni["page"] = tmpStranka.ToString();
                odpoved = UdelejRequestGitHub(url, nastaveni);
                if (odpoved != "[\n\n]\n")
                {
                    vsechnyOdpovedi.Add(odpoved);
                }
            };
            return vsechnyOdpovedi;
        }

        public List<Zaznam> VratCommity()
        {
            List<Zaznam> zaznamy = new List<Zaznam>();
            List<string> odpovedi = CyklujRequesty(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits");
            foreach (var odpoved in odpovedi)
            {
                zaznamy.AddRange(JsonConvert.DeserializeObject<Zaznam[]>(odpoved).ToList());
            }
            return zaznamy;
        }

        public DetailZaznamu VratDetailCommitu(string sha)
        {
            var odpoved = UdelejRequestGitHub(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits/" + sha);
            return JsonConvert.DeserializeObject<DetailZaznamu>(odpoved);
        }

        public List<RootObject> VratSoubory()
        {
            string odpoved = UdelejRequestGitHub(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/contents");
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

        /// <summary>
        /// projede sadu souboru a vrati sumu radku
        /// </summary>
        /// <param name="soubory"></param>
        /// <returns></returns>
        public static Decimal SpocitejPocetRadkuSadySouboru(List<RootObject> soubory)
        {
            // znak /n == newline == uvozuje dalsi radek == pocet radku bude vzdycky n+1
            return SpocitejPocetZnakuSadySouboru(soubory, "\n") + 1;
        }

        /// <summary>
        /// projede sadu souboru a zjisti sumu vyskytu retezce ve vsech
        /// </summary>
        /// <param name="soubory"></param>
        /// <param name="znak"></param>
        /// <returns></returns>
        public static Decimal SpocitejPocetZnakuSadySouboru(List<RootObject> soubory, string znak)
        {
            DataMiner dm = new DataMiner();
            Decimal pocet = 0;
            foreach (var js in soubory)
            {
                pocet += dm.SpocitejVyskytRetezceSouboruUrl(js.download_url, znak);
            }
            return pocet;
        }

        /// <summary>
        /// vyfiltruje ze zadanych souboru jen ty se spravnou koncovkou
        /// </summary>
        /// <param name="soubory"></param>
        /// <param name="koncovka"></param>
        /// <returns></returns>
        public static List<RootObject> SelektujSouboryPodleKoncovky(List<RootObject> soubory, string koncovka)
        {
            var regex = new Regex(@".*." + koncovka + "$");
            return soubory.Where(x => regex.IsMatch(x.path.ToLower())).ToList();
        }

        /// <summary>
        /// main metoda - vrati vsechny soubory a vyfiltruje podle koncovky typ souboru
        /// </summary>
        /// <param name="typ">koncovka souboru</param>
        /// <returns></returns>
        public static List<RootObject> VratSouboryUrcitehoTypuRepozitare(string typ)
        {
            System.Diagnostics.Debug.WriteLine("soubory");
            var vsechnySoubory = VratVsechnySouboryRepozitareRekurzivne();
            var vybraneSoubory = SelektujSouboryPodleKoncovky(vsechnySoubory, typ);
            return vybraneSoubory;
        }

        public static async Task<List<RootObject>> VratSouboryUrcitehoTypuRepozitareAsync(string typ)
        {
            return await Task.Run(() => VratSouboryUrcitehoTypuRepozitare(typ));
        }

        /// <summary>
        /// main metoda - vrati vsechny soubory repozitare definovaneho v DataMineru
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Projde zadanou slozku a podslozky a vrati vsechny soubory
        /// </summary>
        /// <param name="ro">slozka</param>
        /// <returns></returns>
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

        /// <summary>
        /// main metoda pro ziskani souboru z commitu uskutecnenych po zadane dobe
        /// </summary>
        /// <param name="cas">Local time - funkce sama prevede do UTC pro github</param>
        /// <returns></returns>
        public static List<File> VratSouboryCommituDoCasu(DateTime cas)
        {
            System.Diagnostics.Debug.WriteLine("commity");
            cas = cas.ToUniversalTime();
            DataMiner dm = new DataMiner();
            List<Zaznam> zaznamy = dm.VratCommity();
            List<Zaznam> zaznamyHodina = Zaznam.SelektujCasovouPeriodu(zaznamy, cas);
            List<File> soubory = new List<File>();
            foreach (var z in zaznamyHodina)
            {
                var detail = dm.VratDetailCommitu(z.sha);
                soubory.AddRange(detail.files);
            }
            return soubory;
        }

        public static async Task<List<File>> VratSouboryCommituDoCasuAsync(DateTime cas)
        {
            return await Task.Run(() => (VratSouboryCommituDoCasu(cas)));
        }

    }

}
