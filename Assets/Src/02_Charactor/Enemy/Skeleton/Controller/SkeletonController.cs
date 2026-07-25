using UnityEngine;

public class SkeletonController : StateMachine<SkeletonController, SkeletonState>, IEnemy
{
    private GameObject targetObject;
    private SkeletonViewController SkeletonViewController;
    public Rigidbody2D rigidbody2D { get; private set; }

    public Vector2 moveVectol { get; private set; }
    public float distanceDifference { get; private set; }
    public float distanceDifferenceLimit { get; private set; } = 0.1f;

    private float hp;

    private Vector2 toTargetDistance;

    private void Awake()
    {
        SkeletonViewController = GetComponent<SkeletonViewController>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Stateクラスの追加
        stateList.Add(new SkeletonStateIdle(this, SkeletonViewController));
        stateList.Add(new SkeletonStateMove(this, SkeletonViewController));
        stateList.Add(new SkeletonStateDie(this, SkeletonViewController));
    }
    void Start()
    {

        ChangeStateCall(SkeletonState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log(targetObject.name);
        //ターゲットとの距離を計算
        toTargetDistance = targetObject.transform.position - gameObject.transform.position;
        moveVectol = toTargetDistance.normalized;
        distanceDifference = toTargetDistance.sqrMagnitude;

        base.Update();
    }

    public void ChangeStateCall(SkeletonState skeletonState)
    {
        base.ChangeState(stateList[(int)skeletonState]);
    }
    public void TakeDamage(float damage)
    {
        //HPを減らす
        hp -= damage;

        if(hp <= 0)
        {
            ChangeStateCall(SkeletonState.Die);
        }
    }
    public void SetTargetObject(GameObject targetObject)
    {
        Debug.Log(targetObject.name);
        this.targetObject = targetObject;
    }
}
