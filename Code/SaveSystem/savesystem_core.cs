using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class savesystem_core
{
    public static void SaveData(int[] levels)
    {
        BinaryFormatter binaryF = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.michiguardado";

        FileStream fileS = new FileStream(path, FileMode.Create);

        c_gameData data = new c_gameData(levels);

        binaryF.Serialize(fileS, data);
        fileS.Close();
    }

    public static c_gameData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.michiguardado";

        if (File.Exists(path))
        {
            BinaryFormatter binaryF = new BinaryFormatter();
            FileStream fileS = new FileStream(path, FileMode.Open);

            c_gameData data = binaryF.Deserialize(fileS) as c_gameData;
            fileS.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
