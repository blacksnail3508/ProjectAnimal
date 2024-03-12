using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimator : MonoBehaviour
{
    [SerializeField] SkeletonAnimation bear;

    TrackEntry track = new TrackEntry();
    public void Idle()
    {
        track = bear.AnimationState.SetAnimation(0 , BearAnim.Idle , true);
    }

    void Idle(TrackEntry track)
    {
        track=bear.AnimationState.SetAnimation(0 , BearAnim.Idle , true);
    }

    public void Run()
    {
        track=bear.AnimationState.SetAnimation(0 , BearAnim.Run , true);
    }
}

public static class BearAnim
{
    public static string Idle = "idle";
    public static string Run = "Run";
}
