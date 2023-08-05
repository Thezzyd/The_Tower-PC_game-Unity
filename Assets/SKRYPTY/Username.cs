
using System;

[Serializable]
public class Username
{
    public string username;
    public string password;

    public Username() { }

    public Username(string username, string password) {
        this.username = username;
        this.password = password;
    }

}
