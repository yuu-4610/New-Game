using UnityEngine;

public class SkeletonStateDie : State<SkeletonController>
{
    private SkeletonViewController skeletonViewController;
    public SkeletonStateDie(SkeletonController owner, SkeletonViewController skeletonViewController) : base(owner)
    {
        this.skeletonViewController = skeletonViewController;
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
