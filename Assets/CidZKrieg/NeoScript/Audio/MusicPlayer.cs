using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        //PlayRandomly();
    }

    // Componente
    AudioSource aSource;

    // Lista de músicas
    public List<AudioClip> duelMusic = new List<AudioClip>();

    public void PlayMusic(int i)
    {
        aSource.clip = duelMusic[i];

        aSource.Play();
    }

    public void PlayRandomly()
    {
        int i = Random.Range(0, duelMusic.Count);

        PlayMusic(i);
    }

}
