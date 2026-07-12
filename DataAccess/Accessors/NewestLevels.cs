using System.Globalization;
using System.Text;

namespace DataAccess.Accessors
{
    internal class NewestLevels 
    {


        public string Result { get; set; }
        internal NewestLevels(string domain, int page)
        {
            string searchLink = domain + "/files/lists/newest/";

            string link = searchLink 
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
