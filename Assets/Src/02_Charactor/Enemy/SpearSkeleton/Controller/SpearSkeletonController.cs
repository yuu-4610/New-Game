using UnityEngine;
using UnityEngine.Rendering;

public class SpearSkeletonController : StateMachine<SpearSkeletonController, SpearSkeletonState>, IEnemy
{
    private GameObject targetObject;
    private SpearSkeletonViewController spearSkeletonViewController;

    public Rigidbody2D rigidbody2D { get; private set; }
    public Vector2 moveVectol { get; private set; }
    public float distanceDifference { get; private set; }
    public float distanceDifferenceLimit { get; private set; } = 0.1f;
    public Vector2Int currentCell { get; private set; }

    private Vector2 toTargetDistance;

    private void Awake()
    {
        spearSkeletonViewController = GetComponent<SpearSkeletonViewController>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Stateクラスの追加
        stateList.Add(new SpearSkeletonStateIdle(this, spearSkeletonViewController));
        stateList.Add(new SpearSkeletonStateMove(this, spearSkeletonViewController));
        stateList.Add(new SpearSkeletonStateDie(this, spearSkeletonViewController));
    }
    void Start()
    {
        ChangeStateCall(SpearSkeletonState.Idle);
    }

    public override void Update()
    {
        //ターゲットとの距離を計算
        toTargetDistance = targetObject.transform.position - gameObject.transform.position;
        moveVectol = toTargetDistance.normalized;
        distanceDifference = toTargetDistance.sqrMagnitude;

        base.Update();
    }

    public void ChangeStateCall(SpearSkeletonState spearSkeletonState)
    {
        base.ChangeState(stateList[(int)spearSkeletonState]);
    }

    public void ChangeStateCall(SlimeState slimeState)
    {
        base.ChangeState(stateList[(int)slimeState]);
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