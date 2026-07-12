using DataAccess.DataStructures;
using System;
using System.Globalization;
using static DataAccess.DataStructures.SearchLevelInfo;

namespace DataAccess.Accessors
{

    internal class SearchLevels : PostAccessor
    {

        internal SearchLevels(SearchLevelInfo info)
        {
            if (info == null)
                return;

            string searchQuery = GetSearchQuery(info);

            Access(Domain.Current + "/search_levels.php?", searchQuery);
        }

        private string GetDirection(SearchDirectionEnum dir)
        {
            switch(dir)
            {
                case SearchDirectionEnum.Ascending: return "asc";
                default: return "desc";
            }
        }

        private string GetSearchQuery(SearchLevelInfo info)
        {
            string searchQuery = "search_str=" + info.SearchValue
                + "&mode="  + Enum.GetName(typeof(SearchModeEnum), info.Mode).ToLowerInvariant()
                + "&order=" + Enum.GetName(typeof(SearchOrderEnum), info.Order).ToLowerInvariant()
                + "&dir="   + GetDirection(info.Direction).ToLowerInvariant()
                + "&page="  + info.Page.ToString(CultureInfo.InvariantCulture);

            return searchQuery;
        }


    }
}
