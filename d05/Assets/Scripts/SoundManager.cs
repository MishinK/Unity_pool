using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public  AudioClip[] GameMusic;
	public AudioClip[] Applause;
	public AudioClip[] Victory;

	public AudioClip[] IronImpact;
	public AudioClip[] PutterImpact;
	public AudioClip[] WedgeImpact;
	public AudioClip[] WoodImpact;
	
	public AudioClip[] BallinHole;
	public AudioClip[] Wind;
	public AudioClip[] Water;

	private Dictionary<string, AudioSource[]> sounds;

	public AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol) {
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}

	public void AddClips (string name, ref AudioClip[] clips) {
		AudioSource[] sources = new AudioSource[clips.Length];
		for (int i = 0; i < clips.Length; ++i) {
			sources[i] = AddAudio (clips[i], false, false, 0.5f);
		}
		try {
			sounds[name] = sources;
		} catch (UnityException error) {
			Debug.Log (error);
		}
	}

	void Start () {
		sounds = new Dictionary<string, AudioSource[]> ();
		AddClips ("GameMusic", ref GameMusic);
		AddClips ("Applause", ref Applause);
		AddClips ("Victory", ref Victory);
		AddClips ("IronImpact", ref IronImpact);
		AddClips ("PutterImpact", ref PutterImpact);
		AddClips ("WedgeImpact", ref WedgeImpact);
		AddClips ("WoodImpact", ref WoodImpact);
		AddClips ("BallinHole", ref BallinHole);
		AddClips ("Wind", ref Wind);
		AddClips ("Water", ref Water);
	}

	void Update () {
		
	}

	public int Play (string name) {
		int length = 0;

		AudioSource[] array = sounds[name];
		if (array != null && array.Length > 0) {
			int i = Random.Range (0, array.Length);
			array[i].Play();
			length = (int) array[i].clip.length;
		}
		return length;
	}
}
