﻿using Builders.DataStructures.DTO;

namespace MultiTool.DataStructures.Info
{
    class AddArtBlocksInfo
    {

        public AddArtBlocksDTO DTO { get; set; }

        public int LevelID { get; set; }

        public string Title { get; set; }

        public AddArtBlocksInfo()
        {
            DTO = new AddArtBlocksDTO();
        }
    }
}
