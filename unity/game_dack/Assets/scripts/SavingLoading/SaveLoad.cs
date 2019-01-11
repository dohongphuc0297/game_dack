using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static List<BaseCharacterClass> savedUnits = new List<BaseCharacterClass>();
    public static int Level = 0;
    public static void Save(List<BaseCharacterClass> list, int level)
    {
        PlayerPrefs.SetInt("level", level);
        savedUnits = list;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoad.savedUnits);
        file.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedUnits = (List<BaseCharacterClass>)bf.Deserialize(file);
            file.Close();
            Level = PlayerPrefs.GetInt("level");
        }
    }
}
