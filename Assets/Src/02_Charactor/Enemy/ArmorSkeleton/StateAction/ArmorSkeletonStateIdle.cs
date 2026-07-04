using UnityEngine;

public class ArmorSkeletonStateIdle : State<ArmorSkeletonController>
{
    private ArmorSkeletonViewController armorSkeletonViewController;
    public ArmorSkeletonStateIdle(ArmorSkeletonController owner, ArmorSkeletonViewController armorSkeletonViewController) : base(owner)
    {
        this.armorSkeletonViewController = armorSkeletonViewController;
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
