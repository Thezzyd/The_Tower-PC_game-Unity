using UnityEngine;
using System;

public class AgeCorelationData : IComparable<AgeCorelationData>
{
    public string userId;
    public int userAge;

    public int allGamesCount;
    public int allGamesScore;
    public int allGamesPlayTime;
    public int allGamesTowerHightReached;

    public AgeCorelationData(string userId, int userAge, int allGamesCount, int allGamesScore, int allGamesPlayTime, int allGamesTowerHightReached)
    {
        this.userId = userId;
        this.userAge = userAge;
        this.allGamesCount = allGamesCount;
        this.allGamesScore = allGamesScore;
        this.allGamesPlayTime = allGamesPlayTime;
        this.allGamesTowerHightReached = allGamesTowerHightReached;
    }

    public string UserId { get { return userId; } set { userId = value; } }
    public int AllGamesCount { get { return allGamesCount; } set { allGamesCount = value; } }
    public int AllGamesPlayTime { get { return allGamesPlayTime; } set { allGamesPlayTime = value; } }
    public int UseallGamesTowerHightReachedrId { get { return allGamesTowerHightReached; } set { allGamesTowerHightReached = value; } }
    public int AllGamesScore { get { return allGamesScore; } set { allGamesScore = value; } }
    public int UserAge { get { return userAge; } set { userAge = value; } }
    public int AverageScorePerGame { get { return allGamesScore / allGamesCount; } }

    // Default comparer for Part type.
    public int CompareTo(AgeCorelationData comparePart)
    {
        // A null value means that this object is greater.
        if (comparePart == null)
            return 1;

        else
            return this.userAge.CompareTo(comparePart.UserAge);
    }

}
