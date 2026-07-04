using UnityEngine;

public class DarkWizardStateDie : State<DarkWizardController>
{
    private DarkWizardViewController darkWizardViewController;
    public DarkWizardStateDie(DarkWizardController owner, DarkWizardViewController darkWizardViewController) : base(owner)
    {
        this.darkWizardViewController = darkWizardViewController;
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
