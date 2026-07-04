using UnityEngine;

public class SlimeStateIdle : State<SlimeController>
{
    private SlimeViewController slimeViewController;
    public SlimeStateIdle(SlimeController owner, SlimeViewController slimeViewController) : base(owner)
    {
        this.slimeViewController = slimeViewController;
    }

    public override void Enter()
    {

    }

    //毎フレーム処理をするメソッド
    public override void Execute()
    {
        slimeViewController.SlimeAnimationIdle();

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
