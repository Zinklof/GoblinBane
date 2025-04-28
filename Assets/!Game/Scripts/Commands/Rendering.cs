using JetBrains.Annotations;
using UnityEngine;
using ZinklofDev.ConsoleV2;

public class Rendering : MonoBehaviour
{
    [Command("Toggles the rendering of (Most) structure Hitboxes")]
    public static void RenderHitBoxes()
    {
        bool temp = Player.ToggleHitboxes();
        if (temp) { Console.Log("Now rendering (most) HitBoxes", "Rendering"); }
        else { Console.Log("No longer rendering HitBoxes"); }
    }

    [Command("Toggles the rendering of the map boundaries")]
    public static void RenderBounds()
    {
        bool temp = Player.ToggleBounds();
        if (temp) { Console.Log("Now rendering Bounds", "Rendering"); }
        else { Console.Log("No longer rendering Bounds", "Rendering"); }
    }

    [Command("Exits the game", false, "Exit")]
    public static void Exit()
    {
        Application.Quit();
    }
}
