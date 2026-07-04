using UnityEngine;

public class FireWizardStateDie : State<FireWizardController>
{
    private FireWizardViewController fireWizardViewController;
    public FireWizardStateDie(FireWizardController owner, FireWizardViewController fireWizardViewController) : base(owner)
    {
        this.fireWizardViewController = fireWizardViewController;
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
