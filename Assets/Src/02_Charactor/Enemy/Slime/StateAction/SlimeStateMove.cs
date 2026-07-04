using UnityEngine;

public class SlimeStateMove : State<SlimeController>
{
    private SlimeViewController slimeViewController;
    private Rigidbody2D rigidbody2D;
    private float moveSpeed;
    public SlimeStateMove(SlimeController owner, SlimeViewController slimeViewController) : base(owner)
    {
        this.slimeViewController = slimeViewController;

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

        if (owner.distanceDifference < owner.distanceDifferenceLimit)
        {
            owner.ChangeStateCall(SlimeState.Idle);
        }

        slimeViewController.SlimeAnimationMove(owner.moveVectol.x);
    }

    //
    public override void Exit()
    {
        rigidbody2D.linearVelocity = Vector2.zero;
    }
}
