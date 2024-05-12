using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Работа с PlayerPrefs 
/// </summary>
public class DataPlayerPrefs
{
    [SerializeField]
    private SettingsCar _settings;

    /// <summary>
    /// Сохранить данные настроек авто
    /// </summary>
    public static void Save(SaveDataType type, string dataSave)
    {
        PlayerPrefs.SetString(type.ToString(), dataSave);
        PlayerPrefs.Save();
        Debug.Log($"Game {type} : {dataSave} saved!");
    }


    public static string Load(SaveDataType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
        {
            Debug.Log($"Game {type} load!");
            return PlayerPrefs.GetString(type.ToString());
        }
        else
        {
            Debug.LogError($"There is no save {type} data!");
            return null;
        }
    }

    public static bool CheckedHasKey(SaveDataType type)
    {
        return (PlayerPrefs.HasKey(type.ToString()));

    }


    public static Dictionary<string, string> ParceHasKey()
    {
        Dictionary<string, string> dictionary = new();
        string hasData = DataPlayerPrefs.Load(SaveDataType.SettingsPlayer);
        string[] tempData = hasData.Split(';');

        foreach (string data in tempData)
        {
            string[] temp = data.Split(':');
            dictionary.Add(temp[0], temp[1]);
        }
        return dictionary;


    }

    public static float ParceFloat(Dictionary<string, string> dictionart, DataSettings type)
    {
        if (dictionart.TryGetValue(type.ToString(), out string value))
        {
            return float.Parse(value);
        }
        else
        {
            return float.NaN;
        }

    }


}



