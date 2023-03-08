using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for keeping time to the beat of the music.
public class Conductor : SingletonMonobehaviour<Conductor>
{
    [SerializeField] float songBpm = 140;
    [SerializeField] float secPerBeat;
    [SerializeField] float firstBeatOffset = 0;
    [SerializeField] AudioSource musicSource;

    private float songPosition;                 // Current song pos, in seconds
    private float songPositionInBeats;          // Current song pos, in beats
    private float dspSongTime;                  // Seconds passed since song started


    public float SongPosition { get => songPosition; set => songPosition = value; }
    public float SongPositionInBeats { get => songPositionInBeats; set => songPositionInBeats = value; }
    public float DspSongTime { get => dspSongTime; set => dspSongTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (!musicSource) {
            musicSource = GetComponentInChildren<AudioSource>();
        }
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;

        // TODO:  This has to sync up to start of the level in some way
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;
    }
}
