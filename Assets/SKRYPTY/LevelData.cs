

[System.Serializable]
public class LevelData
{
    public string email;
    public string password;

    public float sfxVolume;
    public float musicVolume;
    public bool shakeScreen;

    public LevelData()
    {
        email = FirebaseManager.rememberedEmail;
        password = FirebaseManager.rememberedPassword;

        sfxVolume = OptionsManager.sfxVolume;
        musicVolume = OptionsManager.musicVolume;
        shakeScreen = OptionsManager.screenShake;

    }

}
