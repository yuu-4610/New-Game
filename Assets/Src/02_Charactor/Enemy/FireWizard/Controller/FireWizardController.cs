using UnityEngine;

public class FireWizardController : StateMachine<FireWizardController, FireWizardState>, IEnemy
{
    private GameObject targetObject;
    private FireWizardViewController fireWizardViewController;
    public Vector2Int currentCell { get; private set; }

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
    public void TakeDamage(float damage)
    {

    }
    public void SetTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }

    public void SetCurrentCell(Vector2Int currentCell)
    {
        this.currentCell = currentCell;
    }
    public Vector2Int GetCurrentCell()
    {
        return this.currentCell;
    }
}