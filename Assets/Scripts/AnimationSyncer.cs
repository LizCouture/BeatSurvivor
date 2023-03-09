using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Syncs the speed of the animator to the beat from the Conductor
// To work, all animations on the animator must be scaled to the same speed.
public class AnimationSyncer : MonoBehaviour
{
    // set this to any animation on this animator that loops every beat.
    [SerializeField] AnimationClip onBeatAnimation;


    private Animator animator;
    private float onBeatAnimationTime;

    private void Awake() {
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        onBeatAnimationTime = onBeatAnimation.length;
        Conductor.Instance.conductorReady.AddListener(setAnimationSpeedToMusic);

        //setAnimationSpeedToMusic();
    }

    private void setAnimationSpeedToMusic() {
        Debug.Log("animation Clip length for 1 beat = " + onBeatAnimationTime);
        Debug.Log("seconds For 1 beat = " + Conductor.Instance.SecPerBeat);
        float newSpeed = Conductor.Instance.SecPerBeat / onBeatAnimationTime;
        Debug.Log("Speed before: " + animator.speed);
        animator.speed = animator.speed/newSpeed;
        Debug.Log("Speed to get clip length to that time: " + newSpeed.ToString());
    }
}
