using UnityEngine;


public class SoundController : MonoBehaviour
{
    public static SoundController instance { private set; get; }

    public GameObject SoundPrefab;
    private AudioSource[] Sounds;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Setup();

    }

    public void Setup()
    {
        var effectClips = Resources.LoadAll<AudioClip>("Sounds");
        Sounds = new AudioSource[effectClips.Length];

        for (int i = 0; i < effectClips.Length; i++)
        {
            var audioSource = Instantiate(SoundPrefab, transform).GetComponent<AudioSource>();
            audioSource.clip = effectClips[i];
            audioSource.gameObject.name = effectClips[i].name;
            Sounds[i] = audioSource;
            audioSource.volume = 0.1f;
        }
    }

    public void PlayAudioEffect(string name, SoundAction action = SoundAction.Play)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            var effect = Sounds[i];
            if (effect.name == name)
            {
                if (action == SoundAction.Play)
                {
                    if (!effect.isPlaying)
                        effect.Play();
                }
                else if (action == SoundAction.Stop)
                    effect.Stop();
                else if (action == SoundAction.Reset)
                    effect.Play();

            }
        }
    }

}

public enum SoundAction
{
    Play,
    Stop,
    Reset
}