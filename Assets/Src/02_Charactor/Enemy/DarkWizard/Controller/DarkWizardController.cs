using UnityEngine;

public class DarkWizardController : StateMachine<DarkWizardController, DarkWizardState>
{
    private DarkWizardViewController darkWizardViewController;


    private void Awake()
    {
        darkWizardViewController = GetComponent<DarkWizardViewController>();

        //Stateクラスの追加
        stateList.Add(new DarkWizardStateIdle(this, darkWizardViewController));
        stateList.Add(new DarkWizardStateMove(this, darkWizardViewController));
        stateList.Add(new DarkWizardStateDie(this, darkWizardViewController));
    }
    void Start()
    {
        
    }

    public override void Update()
    {
        base.Update();
    }
    
    public void ChangeStateCall(DarkWizardState darkWizardState)
    {
        base.ChangeState(stateList[(int)darkWizardState]);
    }
}