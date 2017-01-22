using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private AudioSource[] sfxSources;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    private int AmountOfSfxSources = 5;
    private int sfxSourceInd = 0;
    void Awake() {

        if (instance == null)
            instance = this;

        else if (instance != null) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        sfxSources = new AudioSource[AmountOfSfxSources];
        for (int i = 0; i < AmountOfSfxSources; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].volume = 0.3f;
        }

    }
    public void PlaySingle(AudioClip clip)
    {
        AudioSource sfxSource = GetNextAvailableSource();
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        sfxSource.clip = clip;

        //Play the clip.
        sfxSource.Play();
    }

    private AudioSource GetNextAvailableSource()
    {
        sfxSourceInd++;
        if (sfxSourceInd >= AmountOfSfxSources)
        {
            sfxSourceInd = 0;
        }
        return sfxSources[sfxSourceInd];
    }
    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        AudioSource sfxSource = GetNextAvailableSource();

        //Set the pitch of the audio source to the randomly chosen pitch.
        sfxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        sfxSource.clip = clips[randomIndex];

        //Play the clip.
        sfxSource.Play();
    }
}
