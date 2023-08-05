using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	[HideInInspector] public static AudioManager instance;

	[HideInInspector] public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	private List<float> soundsStartVolume;

	private TowerSpawnManager towerSpawnManager;


	void Awake()
	{
		soundsStartVolume = new List<float>();

		for (int i = 0; i < sounds.Length; i++)
		{
			GameObject audioHolder = new GameObject(sounds[i].name);
			audioHolder.transform.SetParent(gameObject.transform);

			sounds[i].source = audioHolder.AddComponent<AudioSource>();

			sounds[i].source.clip = sounds[i].clip;
			sounds[i].source.loop = sounds[i].loop;

			sounds[i].source.spatialBlend = 1.0f;
			sounds[i].source.reverbZoneMix = 0.62f;
			sounds[i].source.dopplerLevel = 0.34f;
			sounds[i].source.rolloffMode = AudioRolloffMode.Linear;
			sounds[i].source.minDistance = 1.6f;
			sounds[i].source.maxDistance = 50.0f;
			sounds[i].source.outputAudioMixerGroup = sounds[i].mixerGroup;

			soundsStartVolume.Add(sounds[i].volume);
			
		}
	}

	public void SetSfxVolume(float vol)
	{
		towerSpawnManager = FindObjectOfType<TowerSpawnManager>();

		for (int i = 0; i < sounds.Length; i++)
		{
			if (!sounds[i].name.Equals("MUSIC"))
			{
				sounds[i].volume = soundsStartVolume[i] * vol;
				sounds[i].source.volume = soundsStartVolume[i] * vol;
			}
		}

		for (int i = 0; i < towerSpawnManager.fireCampAudioSourceList.Count; i++)
		{
			towerSpawnManager.fireCampAudioSourceList[i].volume = OptionsManager.fireCampBaseVolume * vol;
		}

		

		OptionsManager.sfxVolume = vol;
	}

	public void SetSfxVolumeMenu(float vol)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (!sounds[i].name.Equals("MUSIC"))
			{
				sounds[i].volume = soundsStartVolume[i] * vol;
				sounds[i].source.volume = soundsStartVolume[i] * vol;
			}
		}

		OptionsManager.sfxVolume = vol;
	}

	public void SetMusicVolumeMenu(float vol)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name.Equals("MUSIC"))
			{
				sounds[i].volume = soundsStartVolume[i] * vol;
				sounds[i].source.volume = soundsStartVolume[i] * vol;
			}
		}

		OptionsManager.musicVolume = vol;
	}

	public void SetMusicVolume(float vol)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name.Equals("MUSIC"))
			{
				sounds[i].volume = soundsStartVolume[i] * vol;
				sounds[i].source.volume = soundsStartVolume[i] * vol;
			}
		}

		OptionsManager.musicVolume = vol;
	}

	public void Play(string sound, Vector3 whereToPlay)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name+ " not found!");
			return;
		}

		s.source.transform.position = whereToPlay;
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.transform.position = gameObject.transform.position;
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public bool isPlaying(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return false;
		}

		return s.source.isPlaying;
	}

	public bool isPlaying(string sound, Vector3 whereToPlay)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return false;
		}

		s.source.transform.position = whereToPlay;
		return s.source.isPlaying;
	}

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = 0f;
		s.source.pitch = 0f;

		s.source.Stop();
	}

	public void SetVolume(float vol)
	{
		//Sound s = Array.Find(sounds, item => item.name == sound);
	}

	public void StopEveryActiveSoundEffect()
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (!sounds[i].name.Equals("MUSIC"))
			{
				sounds[i].source.volume = 0f;
				sounds[i].source.pitch = 0f;
				sounds[i].source.Stop();
			}
		}
	}

	public void SetTempo(string sound, float tempoValue)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.pitch = tempoValue;
		s.source.pitch = tempoValue;
		AudioMixerGroup mixerGroup = s.mixerGroup;

		mixerGroup.audioMixer.SetFloat("sfxPitch", 0.8f / tempoValue);

	}

	public void PlayOneShoot(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.transform.position = gameObject.transform.position;
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.PlayOneShot(s.clip);
	}

	public void PlayOneShoot(string sound, Vector3 whereToPlay)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.transform.position = whereToPlay;
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.PlayOneShot(s.clip);
	}

}
