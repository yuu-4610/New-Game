using UnityEngine;

public class SpearSkeletonStateIdle : State<SpearSkeletonController>
{
    private SpearSkeletonViewController spearSkeletonViewController;
    public SpearSkeletonStateIdle(SpearSkeletonController owner, SpearSkeletonViewController spearSkeletonViewController) : base(owner)
    {
        this.spearSkeletonViewController = spearSkeletonViewController;
    }

    public override void Enter()
    {

    }

    //毎フレーム処理をするメソッド
    public override void Execute()
    {
        spearSkeletonViewController.SpearSkeletonAnimationIdle();

        if (owner.distanceDifference > owner.distanceDifferenceLimit)
        {
            Debug.Log("Moveへ遷移");
            owner.ChangeStateCall(SlimeState.Move);
        }
    }

    //別ステートへ遷移時に処理されるメソッド
    public override void Exit()
    {

    }
}
