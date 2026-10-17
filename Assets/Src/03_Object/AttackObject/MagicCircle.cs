using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private MagicCircleProcess magicCircleProcess;
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private BoxCollider2D boxCollider2D;
    private bool isfollow = default;


    private void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isfollow) magicCircleProcess.TargetObjectFollow();
    }

    private void Initialize()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetClassReference()
    {
        animator = GetComponent<Animator>();
        magicCircleProcess = new MagicCircleProcess(animator, this.gameObject);
    }

    public void InitializeProcess(GameObject followObject)
    {
        magicCircleProcess.SetPlayerObjectReference(followObject);
    }

    public void ChangeParameter(Vector2 objectScale, float power, string animationTriggerName)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        magicCircleProcess.SetParameter(power, animationTriggerName);

        isfollow = true;
    }

    public void Attack(bool isAttack)
    {
        magicCircleProcess.Attack(isAttack);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"当たった：{this.gameObject.name}");

        if (collision.gameObject.tag == TagName.Enemy.ToString())
        {
            magicCircleProcess.CollisionEnemy(collision);
        }
    }
}
