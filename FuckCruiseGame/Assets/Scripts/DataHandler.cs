using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataHandler
{
    public static void SaveHighscores(List<HighScoreObject> highscores)
    {
        HighScoreList list = new HighScoreList(highscores);
        string path = Application.persistentDataPath + "/highscores.txt";
        string json = JsonUtility.ToJson(list);

        File.WriteAllText(path, json);
    }

    public static List<HighScoreObject> LoadHighscores()
    {
        HighScoreList list;
        List<HighScoreObject> highscores = new List<HighScoreObject>();
        string path = Application.persistentDataPath + "/highscores.txt";

        if (File.Exists(path))
        {
            string str = File.ReadAllText(path);
            list = JsonUtility.FromJson<HighScoreList>(str);

            foreach (HighScoreObject score in list.highscores)
            {
                highscores.Add(score);
            }
        }

        return highscores;
    }
}