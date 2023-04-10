using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMgr : MonoBehaviour
{
   
    
    //saveData
    public static void SetSaveInt(string k, int v)
    {
        PlayerPrefs.SetInt(k, v);
    }
    public static void SetSaveString(string k, string v)
    {
        PlayerPrefs.SetString(k, v);
    }
    //loadData

    public static int GetSaveInt(string k, int v)
    {
        return PlayerPrefs.GetInt(k, v);
    }
    public static string GetSaveString(string k, string v)
    {
        return PlayerPrefs.GetString(k, v);
    }
}
