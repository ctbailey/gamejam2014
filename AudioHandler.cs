using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioCollection
{
    public int repeatInterval;
    public AudioClip[] audioClips;

    [HideInInspector]
    public List<int> recentlyPlayedSounds = new List<int>();
}

public class AudioHandler : MonoBehaviour
{
    enum MusicState
    {
        FullVolume,
        FadingIn,
        FadingOut,
        Muted
    }

    private const float FadeFactor = 0.2f;

    // Audio clips - Example variables
    public AudioClip deliverAtSafeZoneSound;
    public AudioCollection jumpSounds;
	public AudioCollection zombieIdleSounds;
	public AudioCollection zombieCoweringSounds;
	public AudioCollection zombieDeathSounds;
	public AudioCollection playerFootsteps;
	public AudioCollection zombieShuffles;
	public AudioClip bgMusic1;
	public AudioClip bgMusic2;
	public AudioClip buttonPress;

    private static MusicState musicState = MusicState.Muted;
    private static int nextAudioSource;
    private static List<AudioSource> audioSources = new List<AudioSource>();
    private static AudioCollection collectionToPlay;

    // Static reference to singleton object
    public static AudioHandler instance;

    // Methods
    void Start()
    {
        audioSources.Clear();
        instance = gameObject.GetComponent<AudioHandler>();
        
        // Trigger FadeIn
        musicState = MusicState.FadingIn;

        // Get references to all audio sources in the scene
        foreach (var audioSource in GameObject.FindGameObjectsWithTag("AudioSourceObject"))
        {
            audioSources.Add(audioSource.GetComponent<AudioSource>());
        }
    }

    void Update()
    {
        HandleMusicFade();
    }

    void HandleMusicFade()
    {
        switch (musicState)
        {
            case MusicState.FadingIn:
                audio.volume = Mathf.Min(audio.volume + FadeFactor*Time.deltaTime, 1);
                if (audio.volume >= 1)
                    musicState = MusicState.FullVolume;
                break;
            case MusicState.FadingOut:
                audio.volume = Mathf.Max(audio.volume - FadeFactor*Time.deltaTime, 0);
                if (audio.volume <= 0)
                    musicState = MusicState.Muted;
                break;
        }
    }

    public void PlayCipFromCollection(ref AudioCollection collection)
    {
        collectionToPlay = collection;

        StartCoroutine("PlayCollectionClip");
    }

    IEnumerator PlayCollectionClip()
    {
        yield return 0;

        int clipToPlay;

        // The use of coroutines enables us to use a while loop
        do
        {
            clipToPlay = Random.Range(0, collectionToPlay.audioClips.Length);

        } while (collectionToPlay.recentlyPlayedSounds.Contains(clipToPlay));

        collectionToPlay.recentlyPlayedSounds.Add(clipToPlay);

        if (collectionToPlay.recentlyPlayedSounds.Count > collectionToPlay.repeatInterval)
            collectionToPlay.recentlyPlayedSounds.RemoveAt(0);

        PlaySound(collectionToPlay.audioClips[clipToPlay]);
    }

    public static void PlaySound(AudioClip clip)
    {
        // Get index for the next audio source to use
        nextAudioSource = (nextAudioSource + 1) % audioSources.Count;

        if (audioSources[nextAudioSource].isPlaying) return;

        // Assign the clip to the selected audio source
        audioSources[nextAudioSource].clip = clip;

        // Play the clip with the selected audio source
        audioSources[nextAudioSource].PlayOneShot(clip);
    }
}