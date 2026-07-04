using UnityEngine;

public class FireWizardController : StateMachine<FireWizardController, FireWizardState>
{
    private FireWizardViewController fireWizardViewController;


    private void Awake()
    {
        fireWizardViewController = GetComponent<FireWizardViewController>();

        //Stateクラスの追加
        stateList.Add(new FireWizardStateIdle(this, fireWizardViewController));
        stateList.Add(new FireWizardStateMove(this, fireWizardViewController));
        stateList.Add(new FireWizardStateDie(this, fireWizardViewController));
    }
    void Start()
    {
        
    }

    public override void Update()
    {
        base.Update();
    }
    
    public void ChangeStateCall(FireWizardState fireWizardState)
    {
        base.ChangeState(stateList[(int)fireWizardState]);
    }
}