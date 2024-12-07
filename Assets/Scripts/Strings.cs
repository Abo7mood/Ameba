using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Strings
{
    public static string ENEMY = "Enemy", EDGE = "Edge", PLAYER = "Player";

    public static string DIE_MESSAGE = "You Lost All Your Lives ... ";

    public static string LIVE_MESSAGE()
    {
        if(MenuManager.instance.lives<1) return $"You Lost A Life, {MenuManager.instance.lives + 1} Live Left";
else
        return $"You Lost A Life, {MenuManager.instance.lives+1} Lives Left";
    }
}

