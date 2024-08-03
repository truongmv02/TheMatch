using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Main Setting:")]
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float sfxVolume;

    public AudioSource music;
    public AudioSource sfx;


    [Header("Audio:")]
    public AudioClip hurt;
    public AudioClip click;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip, sfxVolume);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        music.clip = clip;
        music.loop = loop;
        music.volume = musicVolume;
        music.Play();
    }

    /* public void PlayBackgroundMusic()
     {
         PlayMusic(background);
     }*/
}