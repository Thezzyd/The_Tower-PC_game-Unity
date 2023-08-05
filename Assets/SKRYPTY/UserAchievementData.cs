using System;

public class UserAchievementData :  IComparable<UserAchievementData>
{
    public string userId;
    public string achievementName;
    public int allGamesCount;
    public int allGamesPlayTime;
    public int allGamesTowerHightReached;
    public string gameKey;
    public double timestamp;
    public DateTime date;

    public UserAchievementData(string userId, string achievementName, int allGamesCount, int allGamesPlayTime, int allGamesTowerHightReached, string gameKey, double timestamp )
    {
        this.userId = userId;
        this.achievementName = achievementName;
        this.allGamesCount = allGamesCount;
        this.allGamesPlayTime = allGamesPlayTime;
        this.allGamesTowerHightReached = allGamesTowerHightReached;
        this.gameKey = gameKey;
        this.timestamp = timestamp;

        ConvertTimestampAsDateTime();
    }

    public string UserId { get { return userId; } set { userId = value; } }
    public string AchievementName { get { return achievementName; } set { achievementName = value; } }
    public int AllGamesCount { get { return allGamesCount; } set { allGamesCount = value; } }
    public int AllGamesPlayTime { get { return allGamesPlayTime; } set { allGamesPlayTime = value; } }
    public int UseallGamesTowerHightReachedrId { get { return allGamesTowerHightReached; } set { allGamesTowerHightReached = value; } }
    public string GameKey { get { return gameKey; } set { gameKey = value; } }
    public double Timestamp { get { return timestamp; } set { timestamp = value; } }
    public DateTime Date { get { return date; } set { date = value; } }


    public void ConvertTimestampAsDateTime()
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(timestamp);
        date = dtDateTime;
    }

    // Default comparer for Part type.
    public int CompareTo(UserAchievementData comparePart)
    {
        // A null value means that this object is greater.
        if (comparePart == null)
            return 1;

        else
            return this.allGamesPlayTime.CompareTo(comparePart.AllGamesPlayTime);
    }

}
