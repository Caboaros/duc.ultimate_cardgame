using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource cardSource;

    public AudioSource towerSource;

    public AudioSource environmentSource;

    public AudioSource battleSource;

    // Classe que guarda o clipe de áudio com uma string para nome, pra facilitar na identificação nos outros scripts

    [System.Serializable]
    public class SFX
    {
        public string name;
        public AudioClip clip;
    }

    public List<SFX> cardSFX = new List<SFX>();

    public List<SFX> towerSFX = new List<SFX>();

    public List<SFX> environmentSFX = new List<SFX>();

    public List<SFX> battleSFX = new List<SFX>();
    
    public void PlayCardSFX(string clipName)
    {
        for (int i = 0; i < cardSFX.Count; i++)
        {
            if (cardSFX[i].name == clipName)
            {
                cardSource.clip = cardSFX[i].clip;

                cardSource.Play();
            }
        }
    }

    public void PlayTowerSFX(string clipName)
    {
        for (int i = 0; i < towerSFX.Count; i++)
        {
            if (towerSFX[i].name == clipName)
            {
                towerSource.clip = towerSFX[i].clip;

                towerSource.Play();
            }
        }
    }

    public void PlayEnvSFX(string clipName)
    {
        for (int i = 0; i < environmentSFX.Count; i++)
        {
            if (environmentSFX[i].name == clipName)
            {
                environmentSource.clip = environmentSFX[i].clip;

                environmentSource.Play();
            }
        }
    }

    public void PlayBattleSFX(string clipName)
    {
        for (int i = 0; i < battleSFX.Count; i++)
        {
            if (battleSFX[i].name == clipName)
            {
                battleSource.clip = battleSFX[i].clip;

                battleSource.Play();
            }
        }
    }


}
