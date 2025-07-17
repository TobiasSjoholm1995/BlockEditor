using MultiTool.Handlers.FileHandlers;

namespace MultiTool.Menu.Options.Settings.Options.UserSettingsOptions.Options
{
    internal class RemoveAllUsersOption
    {

        public RemoveAllUsersOption() {
            UserSettingsHandler.RemoveCurrentUser();
            UserSettingsHandler.RemoveAllUsers();
        }

    }
}
