using UnityEngine;

public class SpearSkeletonStateDie : State<SpearSkeletonController>
{
    private SpearSkeletonViewController spearSkeletonViewController;
    public SpearSkeletonStateDie(SpearSkeletonController owner, SpearSkeletonViewController spearSkeletonViewController) : base(owner)
    {
        this.spearSkeletonViewController = spearSkeletonViewController;
    }

    public override void Enter()
    {

    }

    //毎フレーム処理をするメソッド
    public override void Execute()
    {

    }

    //別ステートへ遷移時に処理されるメソッド
    public override void Exit()
    {

    }
}
