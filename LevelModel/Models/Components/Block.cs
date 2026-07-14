using LevelModel.DTO;
using System;

namespace LevelModel.Models.Components
{
    public class Block
    {


        public static readonly Point START_POSITION = new Point(444, 335);

        public const int BLOCK_ID_ADJUSTER = 100;

        public const int PIXELS_PER_BLOCK = 30;


        public const int BASIC_BROWN    = 100;
        public const int BASIC_WHITE    = 101;
        public const int BASIC_RED      = 102;
        public const int BASIC_WAFFLE   = 103;
        public const int BRICK          = 104;

        public const int ARROW_DOWN     = 105;
        public const int ARROW_UP       = 106;
        public const int ARROW_LEFT     = 107;
        public const int ARROW_RIGHT    = 108;
        public const int MINE           = 109;

        public const int ITEM_BLUE      = 110;
        public const int START_BLOCK_P1 = 111;
        public const int START_BLOCK_P2 = 112;
        public const int START_BLOCK_P3 = 113;
        public const int START_BLOCK_P4 = 114;

        public const int ICE            = 115;
        public const int GOAL           = 116;
        public const int CRUMBLE        = 117;
        public const int VANISH         = 118;
        public const int MOVE_BLOCK     = 119;

        public const int WATER          = 120;
        public const int ROTATE_RIGHT   = 121;
        public const int ROTATE_LEFT    = 122;
        public const int PUSH_BLOCK     = 123;
        public const int NET            = 124;

        public const int ITEM_RED       = 125;
        public const int HAPPY_BLOCK    = 126;
        public const int SAD_BLOCK      = 127;
        public const int HEART          = 128;
        public const int CLOCK          = 129;
        public const int EGG            = 130;
        public const int CUSTOM_STATS   = 131;
        public const int TELEPORT       = 132;

        // 8p trapwork.org
        public const int MUD           = 150;
        public const int FREEZE        = 151;
        public const int LEFT_ONE_WAY  = 152;
        public const int RIGHT_ONE_WAY = 153;
        public const int TOP_ONE_WAY   = 154;
        public const int DOWN_ONE_WAY  = 155;
        public const int BASIC_UNSAFE  = 156;
        public const int BASIC_PILLAR  = 157;


        public static readonly int MaxBlockId_Pr2Hub = TELEPORT;
        public static readonly int MaxBlockId_Trapwork = BASIC_PILLAR;


        public int X { get; set; }

        public int Y { get; set; }

        public int Id { get; set; }

        public string Options { get; set; }


        public Block(int x, int y, int id, string options) {
            X  = x;
            Y  = y;
            Id = id;
            Options = options;
        }


        public static bool IsValidBlock(int? id)
        {
            if (id == null)
                return false;

            if (id >= BASIC_BROWN && id <= MaxBlockId_Pr2Hub)
                return true;

            if (id >= MUD && id <= MaxBlockId_Trapwork)
                return true;

            return false;
        }

        public static bool IsStartBlock(int? id)
        {
            if (id == null)
                return false;

            switch (id)
            {
                case START_BLOCK_P1: return true;
                case START_BLOCK_P2: return true;
                case START_BLOCK_P3: return true;
                case START_BLOCK_P4: return true;

                default: return false;
            }
        }


        public static string GetBlockName(int blockID)
        {
            switch(blockID)
            {                       
                case BASIC_BROWN:    return "Basic Brown";
                case BASIC_WHITE:    return "Basic White";
                case BASIC_RED:      return "Basic Red";
                case BASIC_WAFFLE:   return "Basic Waffle";
                case BRICK:          return "Brick";
                case ARROW_DOWN:     return "Arrow Down";
                case ARROW_UP:       return "Arrow Up";
                case ARROW_LEFT:     return "Arrow Left";
                case ARROW_RIGHT:    return "Arrow Right";
                case MINE:           return "Mine";
                case ITEM_BLUE:      return "Item Blue";
                case START_BLOCK_P1: return "Start Player 1";
                case START_BLOCK_P2: return "Start Player 2";
                case START_BLOCK_P3: return "Start Player 3";
                case START_BLOCK_P4: return "Start Player 4";
                case ICE:            return "Ice";
                case GOAL:           return "Goal";
                case CRUMBLE:        return "Crumble";
                case VANISH:         return "Vanish";
                case MOVE_BLOCK:     return "Move Block";
                case WATER:          return "Water";
                case ROTATE_RIGHT:   return "Rotate Right";
                case ROTATE_LEFT:    return "Rotate Left";
                case PUSH_BLOCK:     return "Push Block";
                case NET:            return "Net";
                case ITEM_RED:       return "Item Red";
                case HAPPY_BLOCK:    return "Happy Block";
                case SAD_BLOCK:      return "Sad Block";
                case HEART:          return "Heart";
                case CLOCK:          return "Clock";
                case EGG:            return "Egg Minion";
                case CUSTOM_STATS:   return "Custom Stats";
                case TELEPORT:       return "Teleport";
                case BASIC_UNSAFE:   return "Basic Unsafe";
                case TOP_ONE_WAY:    return "Top One-Way";
                case RIGHT_ONE_WAY: return "Right One-Way";
                case DOWN_ONE_WAY:  return "Down One-Way";   
                case LEFT_ONE_WAY:  return "Left One-Way";
                case MUD:           return "Mud";
                case FREEZE:        return "Freeze";
                case BASIC_PILLAR:  return "Basic Pillar";



                default: return "Unknown Block";
            }
        }


    }
}
