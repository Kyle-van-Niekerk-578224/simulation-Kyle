using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoad
{
    public static string directory = "/SaveData/";
    public static string fileName = "KukaData.txt";

    public static void Save(SaveData actions)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(actions, true);
        File.WriteAllText(dir + fileName, json);
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveData actions = new SaveData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            actions = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.Log("SaveFile doesn't exist!");
        }

        return actions;
    }

    public static void GetCamImage(RenderTexture rendTex)
    {
        byte[] bytes = toTex2d(rendTex).EncodeToPNG();

        if (!Directory.Exists(Application.persistentDataPath + "/Image/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Image/");
        }

        File.WriteAllBytes(Application.persistentDataPath + "/Image/Picture.png", bytes);
    }

    public static Texture2D toTex2d(RenderTexture texToRender)
    {
        Texture2D tex2D = new Texture2D(190, 140, TextureFormat.RGB24, false);
        RenderTexture.active = texToRender;
        tex2D.ReadPixels(new Rect(0, 0, texToRender.width, texToRender.height), 0, 0);
        tex2D.Apply();

        return tex2D;
    }
}
