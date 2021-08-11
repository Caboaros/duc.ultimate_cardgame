using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public MusicPlayer musicPlayer;

    public SoundEffects SFX;
}
