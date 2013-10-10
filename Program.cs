using OpenTK.Input;
using SuperBitBros.OpenGL;
using System;

namespace SuperBitBros
{

    public class Program
    {
        public const int TEXTUREPACK = 2;

        public static BooleanKeySwitch debugViewSwitch = new BooleanKeySwitch(true, Key.F10, KeyTriggerMode.ON_DOWN);
        public static BooleanKeySwitch vectorDebugViewSwitch = new BooleanKeySwitch(true, Key.F9, KeyTriggerMode.ON_DOWN);
        public static BooleanKeySwitch minimapViewSwitch = new BooleanKeySwitch(false, Key.F8, KeyTriggerMode.ON_DOWN);

        public static BooleanKeySwitch debugFlySwitch = new BooleanKeySwitch(false, Key.LControl, KeyTriggerMode.WHILE_DOWN);
        public static BooleanKeySwitch debugFlyoverrideSwitch = new BooleanKeySwitch(false, Key.ShiftLeft, KeyTriggerMode.WHILE_DOWN);

        public static BooleanKeySwitch debugPlayerUpgrade = new BooleanKeySwitch(false, Key.F3, KeyTriggerMode.FLICKER_DOWN);
        public static BooleanKeySwitch debugCoinCheatSwitch = new BooleanKeySwitch(false, Key.F4, KeyTriggerMode.FLICKER_DOWN);
        public static BooleanKeySwitch debugExplosionSwitch = new BooleanKeySwitch(false, Key.F5, KeyTriggerMode.FLICKER_DOWN);
        public static BooleanKeySwitch debugMapExplosionSwitch = new BooleanKeySwitch(false, Key.F6, KeyTriggerMode.COOLDOWN_DOWN);
        public static BooleanKeySwitch debugParticleSwitch = new BooleanKeySwitch(false, Key.F7, KeyTriggerMode.ON_DOWN);

        private static void Main(string[] args)
        {
            debugFlySwitch.TurnOnEvent += delegate { Console.Out.WriteLine("DebugFlyMode enabled"); };
            debugFlySwitch.TurnOffEvent += delegate { Console.Out.WriteLine("DebugFlyMode disabled"); };

            debugFlyoverrideSwitch.TurnOnEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ enabled"); };
            debugFlyoverrideSwitch.TurnOffEvent += delegate { if (debugFlySwitch.Value) Console.Out.WriteLine("DebugFlyMode++ disabled"); };


            Console.Out.WriteLine("START");
            GameModel world = new GameWorld(8, 4);
            OpenGLView view = new OpenGLGameView(world);

            Textures.Load();

            world.Init(null);

            view.Start(48, 60);
        }
    }
}

//Time disp
//Pushback big Mrio Pipe down (manchmal auch small -> 8-4)
//Coin fo flag (~ 1 lvl on max)
//1 heart for killing bowser
//disp curr lvl
//mario tex plant 0 lvl shows pixel
//hammerbro ai wie hammerbowser
//sprite move left pix overflow
//jump when ducked
//down stand up when ducked and cant
//--> lvl 01-02
//kill mobs when killing ceiling / activate activatorblocks
//zoom x2 mode (double window size, no more vision rect)
//test all level with level test world (respawn on death in lvl select world) -> add roguelike pipe to map, spawn in map on start
//change testure pipes in lvl select lvl
//musik
//ENTER Right Pipe underwater -> 08-04