using UnityEngine;
using UnityEngine.Audio;

//Essa classe personalizada define os componentes do audio maneger, como clip a ser usado, mixer, volume, se é loopavel ou não, etc.
[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;
    public AudioMixerGroup Mixer;
    public bool Loop;

    [Range(1, 100)]
    public int Priority;

    [Range(0f, 1f)]
    public float Volume;

    [Range(.1f, 3f)]
    public float Pitch;
    

    [HideInInspector]
    public AudioSource Source;

}
