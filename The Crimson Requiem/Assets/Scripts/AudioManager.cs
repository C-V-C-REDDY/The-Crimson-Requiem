using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set; }

    [Header("SFX Clips")]
    public AudioClip darkOrbCast;
    public AudioClip enemyDeath;
    public AudioClip shieldHit;
    public AudioClip shieldBreak;
    public AudioClip playerHit;
    public AudioClip spellCast;
    public AudioClip waveClear;

    [Header("Music")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 0.4f;

    [Header("Settings")]
    [SerializeField] int poolSize = 8;
    [SerializeField] float pitchVariation = 0.1f;

    AudioSource[] pool;
    AudioSource musicSource;

    int nextIndex;

    void Awake()
    {
        if(Instance != null && Instance != this) { Destroy(gameObject); return;}
        Instance = this;

        pool = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = gameObject.AddComponent<AudioSource>();
            pool[i].playOnAwake = false;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.clip = backgroundMusic;

    }

    void Start()
    {
        if(backgroundMusic != null) musicSource.Play();
    }

    public void Play(AudioClip clip, float volume = 1f, bool randomPitch = false)
    {
        if(clip == null) return;

        AudioSource src = pool[nextIndex];
        nextIndex = (nextIndex + 1) % pool.Length;

        src.pitch = randomPitch
            ? 1f + Random.Range(-pitchVariation, pitchVariation)
            : 1f;
        src.PlayOneShot(clip, volume);
    }

}
