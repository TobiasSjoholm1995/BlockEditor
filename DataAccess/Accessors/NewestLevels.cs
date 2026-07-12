using System.Globalization;
using System.Text;

namespace DataAccess.Accessors
{
    internal class NewestLevels 
    {

        private static string SEARCH_LINK = Domain.Current + "/files/lists/newest/";

        public string Result { get; set; }
        internal NewestLevels(int page)
        {
            string link = SEARCH_LINK 
                        + page.ToString(CultureInfo.InvariantCulture) 
                        + GetSearchQuery();

            Result = GetAccessor.Download(link);
        }

        private string GetSearchQuery()
        {
            var query = new StringBuilder();

            query.Append("?rand=");
            query.Append("&token=");

            return query.ToString(); ;
        }
    }
}
