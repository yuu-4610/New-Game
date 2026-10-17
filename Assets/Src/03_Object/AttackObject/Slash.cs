using UnityEngine;

public class Slash : MonoBehaviour
{
    private SlashProcess slashProcess;
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private Quaternion followObjectRotation;
    private BoxCollider2D boxCollider2D;
    private bool isfollow = default;
    public int index;
    


    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isfollow) slashProcess.TargetObjectFollow();
    }

    private void Initialize()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetClassReference()
    {
        animator = GetComponent<Animator>();
        slashProcess = new SlashProcess(animator, this.gameObject);
    }

    //攻撃準備
    public void InitializeProcess(GameObject followObject, int objectIndex)
    {
        slashProcess.InitializeProcess(followObject, objectIndex);
        index = objectIndex;
    }

    public void ChangeParameter(Vector2 objectScale, float power, string animationTriggerName)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        slashProcess.SetParameter(power, animationTriggerName);

        isfollow = true;
    }

    public void Attack()
    {
        slashProcess.Attack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag  == TagName.Enemy.ToString())
        {
            if (collision.TryGetComponent<IEnemy>(out var damageable))
            {
                slashProcess.CollisionEnemy(collision);
            }
        }
    }
}
