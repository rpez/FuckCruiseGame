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