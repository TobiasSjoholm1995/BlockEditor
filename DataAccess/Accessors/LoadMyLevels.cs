using System.Text;

namespace DataAccess.Accessors
{
    internal class LoadMyLevels : PostAccessor
    {




        internal LoadMyLevels(string token)
        {
            string loadLink  = Domain.Current + "/levels_get.php?";
            string loadQuery = GetSearchQuery(token);

            Access(loadLink, loadQuery);
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
