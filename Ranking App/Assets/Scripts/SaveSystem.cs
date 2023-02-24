using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayers(List<Player> players)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/players.rnk";
        FileStream stream = new FileStream(path, FileMode.Create);

        AppData data = new AppData(players);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static AppData LoadPlayers()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/players.rnk";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AppData data = formatter.Deserialize(stream) as AppData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
