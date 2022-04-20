using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<SFXManager>
{
	public AudioSource source;
	public AudioClip[] clips;

	public void Start()
	{
		source.clip = clips[Random.Range(0, clips.Length - 1)];
		source.loop = true;
		source.Play();
	}
}
