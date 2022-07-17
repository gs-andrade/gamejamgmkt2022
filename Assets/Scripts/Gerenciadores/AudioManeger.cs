using UnityEngine;
using System;
using UnityEngine.Audio;

//Esse script controla os audios do jogo
public class AudioManeger : MonoBehaviour
{
    //Um array da classe Sound é criado contendo cada em cada posição as informações do audio
    public Sound[] sounds;

    void Awake()
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
    }

    //Toca o audio apartir de dois parametros: Seu nome e seu pitch (entregar o pitch como um parametro permite ser possivel altera-lo por código e randimiza-lo)
    public void Play (string name, float pitch)
    {
        return; 

        Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.Source.pitch = pitch;
        s.Source.Play();
    }
    //Para o audio com o nome
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.Source.Stop();
    }
}
