using DataAccess.DataStructures;
using Newtonsoft.Json.Linq;

namespace DataAccess.Accessors
{
    internal class VersionFetcher
    {

        public VersionInfo Info { get; private set; }

        public VersionFetcher()
        {
            Info = new VersionInfo();
            var path = "https://pr2hub.com/version.txt";
            var data = GetAccessor.Download(path);
            Parse(data);
        }

        private void Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            var json = JObject.Parse(input);
            Info.Url = json?.GetValue("url")?.Value<string>() ?? string.Empty;
            Info.Time = json?.GetValue("time")?.Value<string>() ?? string.Empty;
            Info.Version = json?.GetValue("version")?.Value<string>() ?? string.Empty;
            Info.BuildVersion = GetBuildVersion(json);
        }

        private string GetBuildVersion(JObject json)
        {
            if (json == null)
                return string.Empty;

            var name  = "build";
            var value = json?.GetValue(name)?.Value<string>() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
                return value;

            return string.Empty;

        }

        internal class UserFetcher
        {

            public UserInfo Info { get; private set; }

            public UserFetcher(uint id)
            {
                Info = new UserInfo();
                var path = "https://pr2hub.com/get_player_info.php?user_id=" + id;
                var data = GetAccessor.Download(path);
                Parse(data);
            }

            private void Parse(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return;

                var json = JObject.Parse(input);
                Info.Name = json?.GetValue("name")?.Value<string>() ?? string.Empty;
            }

        }
    }
}
