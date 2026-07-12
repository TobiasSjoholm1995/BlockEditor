using System.Net.NetworkInformation;

namespace DataAccess.Accessors
{
    internal class DownloadLevel
    {
        internal string Result { get; set; }

        internal DownloadLevel(string domain, int levelID)
        {
            string url = GetUrl(domain, levelID);

            Result = GetAccessor.Download(url);
        }

        private string GetUrl(string domain, int levelID)
        {
            string prefix = "";

            if (string.Equals(domain, DataAccess.Domain.Trapwork))
                prefix = "8p_";
    
            string query = domain + "/levels/"
                        + prefix+ levelID + ".txt";
                        // + "?version=" + version;  //PR2 server will auto take last version

            return query;
        }

    }
}
