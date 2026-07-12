using DataAccess.Accessors;
using DataAccess.DataStructures;
using System;
using System.Collections.Generic;
using static DataAccess.Accessors.VersionFetcher;
using static System.Net.WebRequestMethods;

namespace DataAccess
{
    public static class Domain
    {
        public static List<string> All => new List<string> { Pr2Hub, Trapwork };

        public const string Pr2Hub   = "https://pr2hub.com";
        public const string Trapwork = "https://trapwork.org";

    }

    public static class PR2Accessor 
    {

        public static string Download(string domain, int levelID) =>  new DownloadLevel(domain, levelID).Result;

        public static string Search(SearchLevelInfo info) => new SearchLevels(info).Result;

        public static string Newest(string domain, int page) => new NewestLevels(domain, page).Result;

        public static string BestWeek(string domain, int page) => new BestWeekLevels(domain, page).Result;

        public static string Upload(string domain, string levelData, Action<LevelExistArg> onLevelExist) => new UploadLevel(domain, levelData, onLevelExist).Result;

        public static string LoadMyLevels(string domain, string token) => new LoadMyLevels(domain, token).Result;

        public static VersionInfo Pr2Version(string domain) => new VersionFetcher(domain).Info;

        public static UserInfo GetUser(string domain, uint id) => new UserFetcher(domain, id).Info;

        public static TokenInfo GetToken(string username, string password, string domain, string version) => new TokenFetcher(username, password, domain, version).Result;

    }
}
