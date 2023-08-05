
using System;

public class GameData //: MonoBehaviour
{
    public string userId;
    public string gameId;
    public string mapName;
    public int score;
    public int kills;
    public int towerhight;
    public int stars;
    public double timestamp;
    public int playTime;

    public DateTime gameDateTime;

    public GameData(string userId, string gameId, string mapName, int score, int kills, int towerhight, int stars, double timestamp, int playTime)
    {
        this.userId = userId;
     //   plec
   //         data ur
        this.gameId = gameId;
        this.mapName = mapName;
        this.score = score;
        this.kills = kills;
        this.towerhight = towerhight;
        this.stars = stars;
        this.timestamp = timestamp;
        this.playTime = playTime;
      //  this.gameDateTime = gameDateTime;

        ConvertTimestampAsDateTime();
    }
    public GameData()
    {
        this.userId = "";
        //   plec
        //         data ur
        this.gameId = "";
        this.mapName = "";
        this.score =  0;
        this.kills =  0;
        this.towerhight  = 0;
        this.stars =  0;
        this.timestamp  = 0;
        this.playTime  = 0;
    }


    public string UserId { get { return userId; } set { userId = value; } }
    public string GameId { get { return gameId; } set { gameId = value; } }
    public string MapName { get { return mapName; } set { mapName = value; } }
    public int Kills { get { return kills; } set { kills = value; } }
    public int Score { get { return score; } set { score = value; } }
    public int Towerhight { get { return towerhight; } set { towerhight = value; } }
    public int Stars { get { return stars; } set { stars = value; } }
    public double Timestamp { get { return timestamp; } set { timestamp = value; } }
    public int PlayTime { get { return playTime; } set { playTime = value; } }
    public DateTime GameDateTime { get { return gameDateTime; } set { gameDateTime = value; } }

    public void ConvertTimestampAsDateTime()
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(timestamp);
        gameDateTime = dtDateTime;
    }

    public void ConvertTimestampAsDateTime(double timestamp)
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(timestamp);
        gameDateTime = dtDateTime;
    }
}
