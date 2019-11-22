using System.Collections.Generic;

[System.Serializable]
public class HighScoreObject
{
    public HighScoreObject(string team, string[] teamMembers, int score) {
        this.team = team;
        this.teamMembers = teamMembers;
        this.score = score;
    }
    public string team;
    public string[] teamMembers;
    public int score;
}

[System.Serializable]
public class HighScoreList
{
    public HighScoreList(List<HighScoreObject> highscores)
    {
        this.highscores = highscores;
    }
    public List<HighScoreObject> highscores;
}

