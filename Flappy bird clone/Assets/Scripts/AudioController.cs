using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioMixer audioMixer;

    AudioClip musicMainLevel;
    AudioClip jumpSFX;
    AudioClip gameoverSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        InitAudioClips();
    }

    public void PlaySFX(string SFX)
    {
        if (SFX == "jump")
            SFXSource.PlayOneShot(jumpSFX);
        else if (SFX == "gameover")
            SFXSource.PlayOneShot(gameoverSFX);
    }

    public void PlayMusic(string music)
    {
        if (music == "")
            musicSource.clip = null;
        else if (music == "mainLevel")
            musicSource.clip = musicMainLevel;
        musicSource.Play();
    }

    void InitAudioClips()
    {
        musicMainLevel = Resources.Load<AudioClip>("Sounds/mainMusic");
        jumpSFX = Resources.Load<AudioClip>("Sounds/jump");
        gameoverSFX = Resources.Load<AudioClip>("Sounds/gameover");
    }

}
