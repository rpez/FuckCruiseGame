using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    enum GameState { MENU, GAME, END }
    List<string> basicParts = new List<string>(new string[] {
        "Pää",
        "Oikea kämmen",
        "Vasen kämmen",
        "Oikea Jalkaterä",
        "Vasen Jalkaterä",
        "Selkä",
        "Otsa",
        "Hiukset",
        "Peppu",
        "Maha",
        "Vasen Olkapää",
        "Oikea olkapää",
        "Vasen Polvi",
        "Oikea Polvi",
        "Oikea Kyynärpää",
        "Vasen Kyynärpää"
    });

    string currentTeamName;
    List<string> currentTeamMembers;
    List<HighScoreObject> currentHighScores;

    GameState currentState;

    List<string> bodyPartsInUse;
    float currentTime;
    int currentScore;
    string currentBodyPart1;
    string currentBodyPart2;
    string currentTeamMember1;
    string currentTeamMember2;

    [Header("Game settings")]
    public float maxTime;

    [Header("Drag in editor")]
    public GameObject mainMenu;
    public GameObject gameView;
    public GameObject highscores;
    public GameObject settings;
    public TextMeshProUGUI timer, score, name1, name2, bodyPart1, bodyPart2;
    public TMP_InputField teamNameField;
    public TMP_InputField timeLimitField;
    public TeamMembers teamMemberScript;
    public Highscores highscoresScript;

    void Start()
    {
        currentState = GameState.MENU;
        currentHighScores = DataHandler.LoadHighscores();
        UpdateHighscores();
    }

    public void StartGame()
    {
        bodyPartsInUse = basicParts;
        TMP_InputField field = timeLimitField.GetComponent<TMP_InputField>();
        string limitString = field.text != "" ? field.text : "30";
        maxTime = float.Parse(limitString);
        currentTime = maxTime;
        currentScore = 0;

        currentTeamMembers = teamMemberScript.GetMembers();
        currentTeamName = teamNameField.text;

        if (currentTeamMembers.Count == currentTeamMembers.Distinct().Count())
        {
            ToggleView("gameview");
            GetNewBodyParts(true);
            UpdateTimer();
        }
    }

    public void ToggleView(string view)
    {
        switch (view)
        {
            case "mainmenu":
                mainMenu.SetActive(true);
                gameView.SetActive(false);
                highscores.SetActive(false);
                settings.SetActive(false);
                currentState = GameState.MENU;
                break;
            case "gameview":
                mainMenu.SetActive(false);
                gameView.SetActive(true);
                highscores.SetActive(false);
                settings.SetActive(false);
                currentState = GameState.GAME;
                break;
            case "highscores":
                mainMenu.SetActive(false);
                gameView.SetActive(false);
                highscores.SetActive(true);
                settings.SetActive(false);
                break;
            case "settings":
                mainMenu.SetActive(false);
                gameView.SetActive(false);
                highscores.SetActive(false);
                settings.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void GetNewBodyParts(bool skip)
    {
        System.Random rand = new System.Random();

        currentTeamMember1 = currentTeamMembers[rand.Next(0, currentTeamMembers.Count)];
        do
        {
            currentTeamMember2 = currentTeamMembers[rand.Next(0, currentTeamMembers.Count)];
        } while (currentTeamMember1 == currentTeamMember2);

        name1.text = currentTeamMember1;
        name2.text = currentTeamMember2;
        currentBodyPart1 = bodyPartsInUse[rand.Next(0, bodyPartsInUse.Count)];
        currentBodyPart2 = bodyPartsInUse[rand.Next(0, bodyPartsInUse.Count)];
        bodyPart1.text = currentBodyPart1;
        bodyPart2.text = currentBodyPart2;

        if (!skip && currentState == GameState.GAME) currentScore++;

        score.text = currentScore.ToString();
    }

    void UpdateTimer()
    {
        if (currentTime <= 0.0f) timer.text = "0.00";
        else
        {
            string timeString = currentTime.ToString();
            timer.text = timeString.Substring(0, Mathf.Min(timeString.Length, 4));
        }
    }

    public void AddHighscore()
    {
        if (currentState == GameState.END)
        {
            HighScoreObject highscore = new HighScoreObject(currentTeamName, currentTeamMembers.ToArray(), currentScore);
            currentHighScores.Add(highscore);

            DataHandler.SaveHighscores(currentHighScores);
            UpdateHighscores();
        }
    }

    public void UpdateHighscores()
    {
        List<HighScoreObject> sorted = currentHighScores.OrderBy(x => -x.score).ToList();
        highscoresScript.UpdateHighscoreView(sorted);
    }

    void FixedUpdate()
    {
        if (currentState == GameState.GAME)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0.0f)
            {
                currentTime = 0.00f;
                currentState = GameState.END;
            }

            UpdateTimer();
        }
    }
}
