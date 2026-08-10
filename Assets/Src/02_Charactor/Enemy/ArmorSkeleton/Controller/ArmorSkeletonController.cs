using UnityEngine;

public class ArmorSkeletonController : StateMachine<ArmorSkeletonController, ArmorSkeletonState>, IEnemy
{
    private GameObject targetObject;
    private ArmorSkeletonViewController armorSkeletonViewController;
    public Vector2Int currentCell { get; private set; }

    private void Awake()
    {
        armorSkeletonViewController = GetComponent<ArmorSkeletonViewController>();

        //Stateクラスの追加
        stateList.Add(new ArmorSkeletonStateIdle(this, armorSkeletonViewController));
        stateList.Add(new ArmorSkeletonStateMove(this, armorSkeletonViewController));
        stateList.Add(new ArmorSkeletonStateDie(this, armorSkeletonViewController));
    }
    void Start()
    {
        ChangeStateCall(ArmorSkeletonState.Idle);
    }

    public override void Update()
    {
        base.Update();
    }
    public void ChangeStateCall(ArmorSkeletonState armorSkeletonState)
    {
        base.ChangeState(stateList[(int)armorSkeletonState]);
    }

    public void TakeDamage(float damage)
    {

    }
    public void SetTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }

    public GameObject GetGameObject()
    {
        return this.targetObject;
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