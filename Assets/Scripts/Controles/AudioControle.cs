using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControle : MonoBehaviour
{

    public AudioSource musicFonte;
    public AudioSource fxFonte;

    public void TocarAudio(AudioClip musica, AudioClip fx)
    {
        if (fx != null)
        {
            fxFonte.clip = fx;
            fxFonte.Play();
        }

        if (musica != null && musicFonte.clip != musica)
        {
            StartCoroutine(TrocarMusica(musica));
        }
    }

    private IEnumerator TrocarMusica(AudioClip musica)
    {
        if(musicFonte.clip != null)
        {
            while(musicFonte.volume > 0)
            {
                musicFonte.volume -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            musicFonte.volume = 0;
        }

        musicFonte.clip = musica;
        musicFonte.Play();

        while (musicFonte.volume < 0.5)
        {
            musicFonte.volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }


    }


}
