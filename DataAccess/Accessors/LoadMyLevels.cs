using System.Text;

namespace DataAccess.Accessors
{
    internal class LoadMyLevels : PostAccessor
    {


        private static string LOAD_LINK = Domain.Current + "/levels_get.php?";


        internal LoadMyLevels(string token)
        {
            string loadQuery = GetSearchQuery(token);

            Access(LOAD_LINK, loadQuery);
        }


        private string GetSearchQuery(string token)
        {
            StringBuilder query = new StringBuilder();

            query.Append("&rand=");
            query.Append("&token=");
            query.Append(token);

            return query.ToString(); ;
        }


    }
}
