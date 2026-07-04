using UnityEngine;

public class SlimeStateDie : State<SlimeController>
{
    private SlimeViewController slimeViewController;
    public SlimeStateDie(SlimeController owner, SlimeViewController slimeViewController) : base(owner)
    {
        this.slimeViewController = slimeViewController;
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
