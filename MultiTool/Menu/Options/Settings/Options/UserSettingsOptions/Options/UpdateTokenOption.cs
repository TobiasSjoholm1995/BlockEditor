using System;
using MultiTool.Handlers.FileHandlers;

namespace MultiTool.Menu.Options.Settings.Options.UserSettingsOptions.Options
{
    class UpdateTokenOption : BaseMenuOption
    {

        private string _token;

        public UpdateTokenOption() {
            GetRequiredInfo();

            if (IsInputValid)
                UserSettingsHandler.SetCurrentUser(UserSettingsHandler.CurrentUser.Name, _token);
            else
                WriteLine(Environment.NewLine + "\tError, invalid input!", ErrorColor);
        }


        private void GetRequiredInfo() {
            IsInputValid = true;

            _token = GetToken(false);
        }

    }
}
