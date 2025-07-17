using System;
using System.Globalization;
using MultiTool.DataStructures.Constants;

namespace MultiTool.Menu.Options.Settings.Options
{
    internal class InfoOption : IOHandler
    {

        public InfoOption() {
            WriteLine("\tVersion:    " + Constants.APPLICATION_VERSION);
            WriteLine("\tCreator:    " + Constants.CREATOR);
            WriteLine("\tStart Date: " + Constants.START_DATE);
            WriteLine("\tTutorials:  " + Constants.LINK_TO_TUTORIALS + Environment.NewLine);

            WriteLine("\tThanks to "   + Constants.CREDITS + " for inspiring me to start this project!" + Environment.NewLine);
        }

    }
}
