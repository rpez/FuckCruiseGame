using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    enum GameState { MENU, GAME, END }
    List<string> basicParts = new List<string>(new string[] { "Pää", "Oikea Käsi", "Vasen Käsi", "Oikea Jalka", "Vasen Jalka", "Selkä", "Nenä", "Hiukset" });
    List<string> extraParts = new List<string>(new string[] { "Peppu" });

    string currentTeamName;
    List<string> currentTeamMembers;

    GameState currentState;

    List<string> currentBodyParts;
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
    public TeamMembers teamMemberScript;

    void Start()
    {
        currentState = GameState.MENU;
    }

    public void StartGame()
    {
        currentBodyParts = basicParts;
        currentTime = maxTime;
        currentScore = 0;

        currentState = GameState.GAME;

        ToggleView("gameview");
        GetNewBodyParts(true);
        UpdateTimer();
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
                break;
            case "gameview":
                mainMenu.SetActive(false);
                gameView.SetActive(true);
                highscores.SetActive(false);
                settings.SetActive(false);
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
        int randomNum1 = Random.Range(0, currentTeamMembers.Count);
        int randomNum2 = Random.Range(0, randomNum1);
        int randomNum3 = Random.Range(randomNum2, currentTeamMembers.Count);
        int coinFlip = Random.Range(0, 2);

        string player1 = currentTeamMembers[randomNum1];
        int rand = coinFlip == 0 ? randomNum2 : randomNum3;
        string player2 = currentTeamMembers[rand];

        if (!skip) currentScore++;
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
