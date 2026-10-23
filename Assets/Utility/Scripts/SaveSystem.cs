using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int totalPacksOpened;
    public List<PitySaveEntry> pityCounters = new List<PitySaveEntry>();
}

[Serializable]
public class PitySaveEntry
{
    public string setCode;
    public int count;
}

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath))
            return new SaveData(); // first run
        try
        {
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't read save at {SavePath}: {e.Message}. Starting fresh.");
            return new SaveData(); // return a new instance if loading fails
        }
    }
}
