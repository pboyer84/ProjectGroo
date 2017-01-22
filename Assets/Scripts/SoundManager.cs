using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private AudioSource[] sfxSources;
    private AudioSource[] laserSources;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float laserVolume;
    public float fxVolume;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    private int AmountOfSfxSources = 4;
    private int AmountOfLaserSources = 8;
    private int sfxSourceInd = 0;
    private int explosionSourceInd = 0;
    void Awake() {

        if (instance == null)
            instance = this;

        else if (instance != null) Destroy(gameObject);

        laserSources = new AudioSource[AmountOfLaserSources];
        DontDestroyOnLoad(gameObject);
        sfxSources = new AudioSource[AmountOfSfxSources];
        for (int i = 0; i < AmountOfSfxSources; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].volume = fxVolume;
        }

        for (int i=0; i< AmountOfLaserSources; i++)
        {
            laserSources[i] = gameObject.AddComponent<AudioSource>();
            laserSources[i].volume = laserVolume;
        }

    }
    public void PlaySingle(AudioClip clip)
    {
        AudioSource sfxSource;
        if (clip.name.Contains("laser"))
        {
            sfxSource = GetNextAvailableLaserFxSource();
        }
        else
        {
            sfxSource = GetNextAvailableSfxSource();
        }
         
        //Set the clip of our sfxSource audio source to the clip passed in as a parameter.
        sfxSource.clip = clip;

        //Play the clip.
        sfxSource.Play();
    }

    private AudioSource GetNextAvailableSfxSource()
    {
        sfxSourceInd++;
        if (sfxSourceInd >= AmountOfSfxSources)
        {
            sfxSourceInd = 0;
        }
        return sfxSources[sfxSourceInd];
    }

    private AudioSource GetNextAvailableLaserFxSource()
    {
        explosionSourceInd++;
        if (explosionSourceInd >= AmountOfLaserSources)
        {
            explosionSourceInd = 0;
        }
        return laserSources[explosionSourceInd];
    }
    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        AudioSource sfxSource = GetNextAvailableSfxSource();

        //Set the pitch of the audio source to the randomly chosen pitch.
        sfxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        sfxSource.clip = clips[randomIndex];

        //Play the clip.
        sfxSource.Play();
    }
}
