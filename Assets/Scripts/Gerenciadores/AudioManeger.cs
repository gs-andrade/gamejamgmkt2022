using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

//Esse script controla os audios do jogo
public class AudioManeger : MonoBehaviour
{
    //Um array da classe Sound é criado contendo cada em cada posição as informações do audio
    public Sound[] sounds;
    public static AudioManeger Instance;

    private bool initialized = false;

#if UNITY_EDITOR    
    public void RefreshSounds()
    {
        var soundsToAdd = Resources.LoadAll<AudioClip>("Sound");

        sounds = new Sound[soundsToAdd.Length];

        /*if (sounds.Length < soundsToAdd.Length)
        {
            Array.Resize(ref sounds, soundsToAdd.Length);
        }*/

        


        int soundToAddIndex = 0;
        for (int i = 0; i < soundsToAdd.Length; i++)
        {
            if (sounds[i] == null)
            {
                var newclip = soundsToAdd[soundToAddIndex];

                bool canAdd = true;

                for (int j = 0; j < sounds.Length; j++)
                {
                    if (sounds[j] == null)
                        continue;

                    if (sounds[j].Clip == newclip)
                    {
                        canAdd = false;
                        break;
                    }
                }

                if (!canAdd)
                    continue;

                var name = Regex.Replace(newclip.name, @"\s+", "");

                sounds[i] = new Sound
                {
                    Clip = newclip,
                    Name = name,
                    Volume = 0.5f,
                };

                soundToAddIndex++;
            }
        }

        EditorUtility.SetDirty(this);
    }

#endif
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
    public void Play(string name, float pitch, bool loop = false)
    {
        if (sounds != null)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                //evitar erro quando nulo
                var sound = sounds[i];
                if (sound.Name == name)
                {
                    sound.Source.pitch = pitch;
                    sound.Source.loop = loop;
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
