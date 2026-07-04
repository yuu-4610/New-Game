using UnityEngine;

public class SkeletonStateMove : State<SkeletonController>
{
    private SkeletonViewController skeletonViewController;
    private Rigidbody2D rigidbody2D;
    private float moveSpeed;

    public SkeletonStateMove(SkeletonController owner, SkeletonViewController skeletonViewController) : base(owner)
    {
        this.skeletonViewController = skeletonViewController;
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

    //
    public override void Execute()
    {
        //
        rigidbody2D.linearVelocity = owner.moveVectol * moveSpeed;

        if(owner.distanceDifference < owner.distanceDifferenceLimit)
        {
            owner.ChangeStateCall(SkeletonState.Idle);
        }

        skeletonViewController.SkeletonMoveAnimation(owner.moveVectol.x);
    }

    //
    public override void Exit()
    {
        rigidbody2D.linearVelocity = Vector2.zero;
    }
}
