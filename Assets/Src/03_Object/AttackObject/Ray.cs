using UnityEngine;

public class Ray : MonoBehaviour
{
    private RayProcess rayProcess;
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private BoxCollider2D boxCollider2D;
    private bool isfollow = default;


    private void Awake()
    {
        Initialize();
    }
    void Update()
    {
        if (isfollow) rayProcess.TargetObjectFollow();
    }

    private void Initialize()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetClassReference()
    {
        animator = GetComponent<Animator>();
        rayProcess = new RayProcess(animator, this.gameObject);
    }

    //攻撃準備
    public void InitializeProcess(GameObject followObject, int objectIndex)
    {
        rayProcess.InitializeProcess(followObject, objectIndex);
    }

    public void ChangeParameter(Vector2 objectScale, float power, string animationTriggerName)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        rayProcess.SetParameter(power, animationTriggerName);

        isfollow = true;
    }

    public void Attack()
    {
        rayProcess.Attack();
    }

    //座標の更新、プレイヤーを起点に更新していく


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"当たった：{this.gameObject.name}");

        if (collision.gameObject.tag == TagName.Enemy.ToString())
        {
            rayProcess.CollisionEnemy(collision);
        }
    }
}
