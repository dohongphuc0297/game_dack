﻿using System.Collections;
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
        FileStream file;
        string destination = Application.persistentDataPath + "/savedGames.dat";
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        bf.Serialize(file, savedUnits);
        file.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.dat", FileMode.Open);
            savedUnits = (List<BaseCharacterClass>)bf.Deserialize(file);
            file.Close();
            Level = PlayerPrefs.GetInt("level");
        }
    }
}
