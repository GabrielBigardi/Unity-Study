using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
	public AudioSource source;
	public AudioClip[] clips;

    public void PlaySFX(int index)
	{
		source.PlayOneShot(clips[index]);
	}
}
