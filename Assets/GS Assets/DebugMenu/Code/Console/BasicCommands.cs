using JetBrains.Annotations;
using UnityEngine;
using ZinklofDev.Console;

 namespace ZinklofDev.Console
{
    public class BasicCommands : MonoBehaviour
    {
        public static Command HELLOWORLD = new Command("0000x0000000000", "HelloWorld", "Prints HelloWorld", false, () =>
        {
            ZinklofDev.Console.BasicCommands.HelloWorld();
        });

        public static void HelloWorld()
        {
            Log.LogMisc("Hello World!", "BasicCommands.cs Line(14)");
        }

        public static Command RENDERHITBOX = new Command("0000x000000005", "gs_renderer_hitboxes", "renders hitboxes of anything with a hitbox", false, () =>
        {
            bool temp = Player.ToggleHitboxes();
            if (temp) { Log.LogResponse("Now rendering HitBoxes"); }
            else { Log.LogResponse("No longer rendering HitBoxes"); }
        });

        public static Command RENDERBOUNDS = new Command("0000x000000005", "gs_renderer_bounds", "renders the boundarys of player movment", false, () =>
        {
            bool temp = Player.ToggleBounds();
            if (temp) { Log.LogResponse("Now rendering Bounds"); }
            else { Log.LogResponse("No longer rendering Bounds"); }
        });

        // 1 = on, 0 = off, everything else returns out of bounds
        public static Command<byte> DEBUGCHEATS = new Command<byte>("0000x0000000001", "gs_cheats", "Turns on Cheats", false, (t1) =>
        {
            DebugCheats(t1);
        });

        public static Command<int, int> ADDITION = new Command<int, int>("0000x0000000003", "gs_add", "Adds two values together (two ints)", false, (t1, t2) =>
        {
            Addition(t1, t2);
        });

        public static Command EXIT = new Command("0000x0000000004", "gs_exit", "Exits the program", false, () =>
        {
            ZinklofDev.Console.BasicCommands.Exit();
        }); 

        public static void DebugCheats(byte value)
        {
            if (value == 1)
            {
                ZinklofDev.Console.Shell.CheatsOn = true;
                Log.LogResponse("Debug Cheats are enabled");
            }
            else if (value == 0)
            {
                ZinklofDev.Console.Shell.CheatsOn = false;
                Log.LogResponse("Debug Cheats are disabled");
            }
            else
            {
                Log.LogError(value + " Is not a valid parameter/value for the command 'DebugCheats'. Use 1 or 0", "BasicCommands.cs(Line 49)");
            }
        }

        public static void Exit()
        {
            Application.Quit();
        }

        public static void Addition (int value, int value2)
        {
            Log.LogResponse(value + " + " +  value2 + " = " + (value + value2));
        }

        private void Awake()
        {
            Shell.RegisterCommand(HELLOWORLD);
            Shell.RegisterCommand(EXIT);
            Shell.RegisterCommand(DEBUGCHEATS);
            //Shell.RegisterCommand(ADDITION);
            Shell.RegisterCommand(RENDERBOUNDS);
            Shell.RegisterCommand(RENDERHITBOX);
        }
    }
}
