using Spine;
using Spine.Unity;
using UnityEngine;

public class PigAnimator : MonoBehaviour
{
    [SerializeField] SkeletonAnimation pig;

    TrackEntry track = new TrackEntry();

    FaceDirection lastDirection;

    public void PlayWin()
    {
        pig.AnimationState.ClearTrack(0);
        pig.skeleton.SetToSetupPose();
        track=pig.AnimationState.SetAnimation(0 , PigAnim.Win , false);
        track.Complete+=Idle;
    }
    void GoLeft()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.Left , true);
    }
    void GoRight()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.Right , true);
    }
    void GoUp()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.Up , true);
    }
    void GoDown()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.Down , true);
    }
    void StopLeft()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.LeftStop , false);
        track.Complete+=Idle;
    }
    void StopRight()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.RightStop , false);
        track.Complete+=Idle;
    }
    void StopUp()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.UpStop , false);
        track.Complete+=Idle;
    }
    void StopDown()
    {
        track=pig.AnimationState.SetAnimation(0 , PigAnim.DownStop , false);
        track.Complete+=Idle;
    }

    public void Idle()
    {
        pig.AnimationState.ClearTrack(0);
        pig.skeleton.SetToSetupPose();
        track = pig.AnimationState.SetAnimation(0 , PigAnim.Idle , true);
    }
    void Idle(TrackEntry track)
    {
        track = pig.AnimationState.SetAnimation(0 , PigAnim.Idle , true);
    }

    public void Move(FaceDirection direction)
    {
        lastDirection = direction;
        switch (direction)
        {
            case FaceDirection.Up:
                GoUp();
                break;
            case FaceDirection.Down:
                GoDown();
                break;
            case FaceDirection.Left:
                GoLeft();
                break;
            case FaceDirection.Right:
                GoRight();
                break;
        }
    }

    public void Stop()
    {
        switch (lastDirection)
        {
            case FaceDirection.Up:
                StopUp();
                break;
            case FaceDirection.Down:
                StopDown();
                break;
            case FaceDirection.Left:
                StopLeft();
                break;
            case FaceDirection.Right:
                StopRight();
                break;
        }
    }
}

public static class PigAnim
{
    public static string Idle = "idle";

    public static string Left = "Right_to_Left";
    public static string LeftStop = "Right_to_Left_Impact";

    public static string Right = "Left_To_Right";
    public static string RightStop = "Left_To_Right_Impact";

    public static string Up = "UpDown";
    public static string UpStop = "UpDown_Impact";

    public static string Down = "UpDown";
    public static string DownStop = "UpDown_Impact";

    public static string Win = "Win";

}
