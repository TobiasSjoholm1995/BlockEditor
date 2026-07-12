using System.Globalization;
using System.Text;

namespace DataAccess.Accessors
{
    internal class NewestLevels 
    {


        public string Result { get; set; }
        internal NewestLevels(int page)
        {
            string searchLink = Domain.Current + "/files/lists/newest/";

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
