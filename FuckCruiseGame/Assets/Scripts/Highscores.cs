using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscores : MonoBehaviour
{
    [Header("Drag in editor")]
    public GameObject highscorePrefab;
    public GameObject content;

    public void UpdateHighscoreView(List<HighScoreObject> highscores)
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < highscores.Count; i++)
        {
            GameObject prefab = GameObject.Instantiate(highscorePrefab);
            prefab.transform.SetParent(content.transform);
            prefab.transform.Find("TeamName").gameObject.GetComponent<TextMeshProUGUI>().text = highscores[i].team;
            prefab.transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>().text = highscores[i].score.ToString();
        }
    }
}
