using UnityEngine;

public class SlimeController : StateMachine<SlimeController, SlimeState>, IEnemy
{
    private GameObject targetObject;
    private SlimeViewController slimeViewController;
    public Rigidbody2D rigidbody2D { get; private set; }
    public Vector2 moveVectol { get; private set; }
    public float distanceDifference { get; private set; }
    public float distanceDifferenceLimit { get; private set; } = 0.1f;

    private Vector2 toTargetDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        slimeViewController = GetComponent<SlimeViewController>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Stateクラスの追加
        stateList.Add(new SlimeStateIdle(this, slimeViewController));
        stateList.Add(new SlimeStateMove(this, slimeViewController));
        stateList.Add(new SlimeStateDie(this, slimeViewController));
    }
    void Start()
    {
        ChangeStateCall(SlimeState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        //ターゲットとの距離を計算
        toTargetDistance = targetObject.transform.position - gameObject.transform.position;
        moveVectol = toTargetDistance.normalized;
        distanceDifference = toTargetDistance.sqrMagnitude;

        base.Update();
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
}
