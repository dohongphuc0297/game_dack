using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInformation
{
    public static void SaveAllInformation()
    {
        PlayerPrefs.SetString("player_name", "phuc");
    }
}
