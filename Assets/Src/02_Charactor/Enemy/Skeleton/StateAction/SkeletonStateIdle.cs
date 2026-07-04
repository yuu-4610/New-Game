using UnityEngine;

public class SkeletonStateIdle : State<SkeletonController>
{
    private SkeletonViewController skeletonViewController;
    public SkeletonStateIdle(SkeletonController owner, SkeletonViewController skeletonViewController) : base(owner)
    {
        this.skeletonViewController = skeletonViewController;
    }

    public override void Enter() 
    {

    }

    //毎フレーム処理をするメソッド
    public override void Execute() 
    {
        skeletonViewController.SkeletonIdleAnimation();

        if (owner.distanceDifference > owner.distanceDifferenceLimit)
        {
            Debug.Log("Moveへ遷移");
            owner.ChangeStateCall(SkeletonState.Move);
        }

        Debug.Log("StateIdle");
    }

    //別ステートへ遷移時に処理されるメソッド
    public override void Exit() 
    {
        Debug.Log("移動");
    }
}
