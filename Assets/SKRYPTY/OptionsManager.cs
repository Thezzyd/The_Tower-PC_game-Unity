using UnityEngine;
using Cinemachine;

public class OptionsManager : MonoBehaviour
{

	[HideInInspector] public static OptionsManager instance;
	private CinemachineVirtualCamera cineVCam;

	public static bool screenShake = true;
	public static float musicVolume = 1f;
	public static float sfxVolume = 1f;

	public static float fireCampBaseVolume = 0.45f;

	private AudioManager audioManager;

	void Start()
    {
		audioManager = FindObjectOfType<AudioManager>();

		LoadLevel();
	    
		cineVCam = FindObjectOfType<CinemachineVirtualCamera>();
	
		RefreashScreenShakeOptions();

		
	}

	public void RefreashScreenShakeOptions()
	{
		cineVCam = FindObjectOfType<CinemachineVirtualCamera>();

		if (screenShake)
		{
			CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
			cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 3.0f;

		}
		else
		{
			CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
			cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0.0f;
		}
	}


	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void LoadLevel()
	{
		LevelData data = SaveSystem.LoadLevel();
		if (data != null)
		{
			OptionsManager.sfxVolume = data.sfxVolume;
			OptionsManager.musicVolume = data.musicVolume;
			OptionsManager.screenShake = data.shakeScreen;
		}
	}

}
