using System.Collections.Generic;
using UnityEngine;

// 1. We create a custom class to hold the clip AND its specific volume.
// [System.Serializable] tells Unity to show this in the Inspector.
[System.Serializable]
public class MusicTrack
{
    public AudioClip clip;

    // [Range] creates a nice slider in the Inspector from 0 (mute) to 1 (full volume)
    [Range(0f, 1f)]
    public float volume = 1f;
}

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [Header("Playlist Configuration")]
    [Tooltip("Add your music tracks and adjust their individual base volumes here.")]
    [SerializeField] private MusicTrack[] musicTracks;

    private AudioSource audioSource;
    private List<MusicTrack> tracksToPlay = new List<MusicTrack>();
    private MusicTrack lastPlayedTrack;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicTracks.Length > 0)
        {
            StartGameMusic();
        }
        else
        {
            Debug.LogWarning("No music tracks assigned to the MusicManager!");
        }
    }

    void Update()
    {
        // Check if the current song has finished playing
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            PlayNextSong();
        }
    }

    void StartGameMusic()
    {
        InitializePlaylist();
        PlayNextSong();
    }

    void InitializePlaylist()
    {
        // Refill the bag with all our track data
        tracksToPlay.Clear();
        tracksToPlay.AddRange(musicTracks);

        // Shuffle the bag
        Shuffle(tracksToPlay);

        // Edge case: Make sure the first song of this new batch isn't the same
        // as the very last song we just played.
        if (lastPlayedTrack != null && tracksToPlay.Count > 1 && tracksToPlay[0].clip == lastPlayedTrack.clip)
        {
            // Swap the first song with the second song
            MusicTrack temp = tracksToPlay[0];
            tracksToPlay[0] = tracksToPlay[1];
            tracksToPlay[1] = temp;
        }
    }

    void PlayNextSong()
    {
        // If the bag is empty, refill and reshuffle
        if (tracksToPlay.Count == 0)
        {
            InitializePlaylist();
        }

        // Pull the first track out of the shuffled bag
        MusicTrack nextTrack = tracksToPlay[0];
        tracksToPlay.RemoveAt(0);

        // Apply BOTH the clip and the custom volume to our single AudioSource
        audioSource.clip = nextTrack.clip;
        audioSource.volume = nextTrack.volume;
        audioSource.Play();

        // Remember this track so we don't repeat it immediately next time
        lastPlayedTrack = nextTrack;
    }

    // A standard Fisher-Yates shuffle algorithm
    private void Shuffle(List<MusicTrack> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            MusicTrack temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Add this to the bottom of your MusicManager class
    [ContextMenu("Skip Track")]
    public void SkipTrack()
    {
        // We make sure the game is actually playing before trying to skip
        if (Application.isPlaying)
        {
            PlayNextSong();
        }
        else
        {
            Debug.LogWarning("You can only skip tracks while the game is playing!");
        }
    }
}