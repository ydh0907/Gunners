using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource effect;

    public void Play(AudioClip clip)
    {
        effect.PlayOneShot(clip);
    }
}
