using UnityEngine;

public class SpearSkeletonStateMove : State<SpearSkeletonController>
{
    private SpearSkeletonViewController spearSkeletonViewController;
    private Rigidbody2D rigidbody2D;
    private float moveSpeed = 2f;

    public SpearSkeletonStateMove(SpearSkeletonController owner, SpearSkeletonViewController spearSkeletonViewController) : base(owner)
    {
        this.spearSkeletonViewController = spearSkeletonViewController;

        Initialize();
    }

    private void Initialize()
    {
        rigidbody2D = owner.rigidbody2D;

        moveSpeed = 1;
    }

    public override void Enter()
    {

    }

    //毎フレーム処理をするメソッド
    public override void Execute()
    {
        //
        rigidbody2D.linearVelocity = owner.moveVectol * moveSpeed;

        if (owner.distanceDifference < owner.distanceDifferenceLimit)
        {
            owner.ChangeStateCall(SlimeState.Idle);
        }

        spearSkeletonViewController.SpearSkeletonAnimationMove(owner.moveVectol.x);
    }

    //別ステートへ遷移時に処理されるメソッド
    public override void Exit()
    {
        rigidbody2D.linearVelocity = Vector2.zero;
    }
}
