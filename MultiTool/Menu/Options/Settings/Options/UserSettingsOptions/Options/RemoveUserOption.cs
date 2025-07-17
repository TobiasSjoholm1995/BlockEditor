using System;
using MultiTool.Handlers.FileHandlers;

namespace MultiTool.Menu.Options.Settings.Options.UserSettingsOptions.Options
{
    class RemoveUserOption : BaseMenuOption
    {

        public RemoveUserOption() {
            try {
                UserSettingsHandler.RemoveCurrentUser();
                WriteLine(Environment.NewLine + "\t" + "User was successfully removed!");
            }
            catch (Exception ex) {
                WriteLine(Environment.NewLine + "\tError: Update to file failed");
                WriteLine("\tReason: " + ex.Message);
            }
        }

    }
}
