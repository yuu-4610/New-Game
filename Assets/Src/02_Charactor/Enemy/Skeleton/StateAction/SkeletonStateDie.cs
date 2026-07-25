using UnityEngine;

public class SkeletonStateDie : State<SkeletonController>
{
    private SkeletonViewController skeletonViewController;
    private AnimatorStateInfo animatorStateInfo;
    public SkeletonStateDie(SkeletonController owner, SkeletonViewController skeletonViewController) : base(owner)
    {
        this.skeletonViewController = skeletonViewController;
    }

    public override void Enter()
    {
        skeletonViewController.SkeletonDieAnimation();
    }

    //毎フレーム処理をするメソッド
    public override void Execute()
    {
        animatorStateInfo = skeletonViewController.GetStateInfo();
        //Dieアニメーションが終了したら
        if (animatorStateInfo.IsName(EnemyAnimatorStateName.Die.ToString()) && animatorStateInfo.normalizedTime >= 0.9f)
        {
            //オブジェクトプールに戻す
            EventManager.Instance.EnemyObjectPushEvent(EnemyObjectPoolName.Skeleton.ToString(), owner.gameObject);
            //アニメーションをIdleにする
            skeletonViewController.SkeletonIdleAnimation();
        }
    }

    //別ステートへ遷移時に処理されるメソッド
    public override void Exit()
    {

    }
}
