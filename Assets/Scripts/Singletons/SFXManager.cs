using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
	public AudioSource source;
	public AudioClip[] clips;

    public void PlaySFX(int index)
	{
		source.PlayOneShot(clips[index]);
	}
}
