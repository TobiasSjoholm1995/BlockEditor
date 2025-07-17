﻿using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using MultiTool.DataStructures.Constants;
using MultiTool.Menu.Options;
using MultiTool.Menu.Options.ExistingLevel.Options.ModifyLevel;
using MultiTool.Menu.Options.ExistingLevel.Options.SearchPublicLevels;
using MultiTool.Menu.Options.ExistingLevel.Options.SearchLevels.Options;

namespace MultiTool.Menu.Options.ExistingLevel.Options.SearchLevels
{
    class SearchLevelsOption : BaseMenuOption
    {

        public SearchLevelsOption()
        {
            ShowOptions();
            Write(Environment.NewLine + "Pick option:  ", UserInputColor);
            HandleOption(ReadInput());
        }


        private void ShowOptions()
        {
            WriteLine("\t" + MenuOptions.PUBLIC_LEVELS + "  -  Public Levels");
            WriteLine("\t" + MenuOptions.MY_LEVELS     + "  -  My Levels");
            WriteLine();
            WriteLine("\t" + MenuOptions.QUIT + "  -  Quit/Back");
        }

        private void HandleOption(string option)
        {
            switch (option.ToLower(CultureInfo.InvariantCulture))
            {
                case MenuOptions.PUBLIC_LEVELS:
                    new SearchPublicLevelsOption();
                    break;
                case MenuOptions.MY_LEVELS:
                    new SearchMyLevelsOption();
                    break;
                case MenuOptions.QUIT:
                    break;
                default:
                    WriteLine("\tError: Invalid input.", ErrorColor);
                    break;
            }
        }

    }
}
