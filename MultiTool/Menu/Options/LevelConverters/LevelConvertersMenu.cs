﻿using System;
using System.Globalization;
using MultiTool.DataStructures.Constants;
using MultiTool.Menu.Options.NewLevel;
using MultiTool.Menu.Options.LevelConverters.Options.ConvertFromPr2;
using MultiTool.Menu.Options.LevelConverters.Options.ConvertFromTmx;
using MultiTool.Menu.Options.LevelConverters.Options.ConvertFromTxt;

namespace MultiTool.Menu.Options.LevelConverters
{
    internal class LevelConvertersMenu : IOHandler
    {


        public LevelConvertersMenu()
        {
            ShowOptions();
            Write(Environment.NewLine + "Pick option:  ", UserInputColor);
            HandleOption(ReadInput());
        }


        private void ShowOptions()
        {
            WriteLine("Convert From:");
            WriteLine("\t" + MenuOptions.FROM_PR2_LEVEL   + "  -  PR2 Level");
            WriteLine("\t" + MenuOptions.FROM_TMX_LEVEL   + "  -  Tiled");
            WriteLine("\t" + MenuOptions.FROM_TXT_LEVEL   + "  -  Text File");

            WriteLine();
            WriteLine("\t" + MenuOptions.QUIT + "  -  Quit/Back");
        }

        private void HandleOption(string option)
        {
            switch (option.ToLower(CultureInfo.InvariantCulture))
            {
                case MenuOptions.FROM_PR2_LEVEL:
                    new ConvertFromPr2Menu();
                    break;

                case MenuOptions.FROM_TMX_LEVEL:
                    new ConvertFromTmxMenu();
                    break;

                case MenuOptions.FROM_TXT_LEVEL:
                    new ConvertFromTxtMenu();
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
