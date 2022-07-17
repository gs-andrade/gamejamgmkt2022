using UnityEngine;
using System;
using UnityEngine.Audio;

//Esse script controla os audios do jogo
public class AudioManeger : MonoBehaviour
{
    //Um array da classe Sound é criado contendo cada em cada posição as informações do audio
    public Sound[] sounds;
    public static AudioManeger Instance;

    private bool initialized = false;
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        if (!initialized)
        {
            //Para cada audio são atribuidas as informações presentes na classe Sound: Nome, Volume, mixer de audio, etc
            foreach (Sound s in sounds)
            {
                s.Source = gameObject.AddComponent<AudioSource>();

                s.Source.outputAudioMixerGroup = s.Mixer;
                s.Source.clip = s.Clip;
                s.Source.volume = s.Volume;
                s.Source.loop = s.Loop;
                s.Source.priority = s.Priority;
            }

            initialized = true;
        }
    }


    //Toca o audio apartir de dois parametros: Seu nome e seu pitch (entregar o pitch como um parametro permite ser possivel altera-lo por código e randimiza-lo)
    public void Play (string name, float pitch)
    {
        if(sounds != null)
        {
            for(int i = 0; i < sounds.Length; i++)
            {
                //evitar erro quando nulo
                var sound = sounds[i];
                if(sound.Name == name)
                {
                    sound.Source.pitch = pitch;
                    sound.Source.Play();
                }

            }
        }

       /* Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.Source.pitch = pitch;
        s.Source.Play();*/
    }
    //Para o audio com o nome
    public void Stop(string name)
    {
        if (sounds != null)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                //evitar erro quando nulo
                var sound = sounds[i];
                if (sound.Name == name)
                {
                    sound.Source.Stop();
                }

            }
        }

       /* Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.Source.Stop();*/
    }
}
