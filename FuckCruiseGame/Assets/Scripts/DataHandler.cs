using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataHandler
{
    public static void SaveHighscores(List<HighScoreObject> highScores)
    {
        string json = JsonUtility.ToJson(highScores);

        File.WriteAllText(Application.persistentDataPath + "/highscores.txt", json);
    }

    public static List<HighScoreObject> LoadHighscores()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/highscores.txt");
        List<HighScoreObject> highscores = JsonUtility.FromJson<List<HighScoreObject>>(str);

        return highscores;
    }
}