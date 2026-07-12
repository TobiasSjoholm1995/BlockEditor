using DataAccess.DataStructures;
using System;

namespace DataAccess.Accessors
{
    internal class UploadLevel : PostAccessor
    {

        private const string StatusExist = "status=exists";

        internal UploadLevel(string levelData, Action<LevelExistArg> onLevelExist)
        {
            var arg = new LevelExistArg();;

            do
            {
                arg = new LevelExistArg();
                Access(Domain.Current + "/upload_level.php", levelData);

                if (IsStatusExist(Result))
                {
                    Result = null;
                    onLevelExist.Invoke(arg);
                    levelData = arg.NewLevelData;
                }
            }
            while(arg.TryAgain);
        }

        public  bool IsStatusExist(string msg)
        {
            var sc = StringComparison.InvariantCultureIgnoreCase;

            return string.Equals(msg, StatusExist, sc);
        }


    }
}
