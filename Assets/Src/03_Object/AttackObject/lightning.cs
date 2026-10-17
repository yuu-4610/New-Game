using UnityEngine;

public class lightning : MonoBehaviour
{
    private lightningProcess lightningProcess;
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    
    private BoxCollider2D boxCollider2D;
    

    private void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetClassReference()
    {
        animator = GetComponent<Animator>();
        lightningProcess = new lightningProcess(animator, this.gameObject);
    }

    //攻撃準備
    public void InitializeProcess(GameObject followObject)
    {
        lightningProcess.SetPlayerObjectReference(followObject);
    }

    public void ChangeParameter(Vector2 objectScale, float power, string animationTriggerName)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        lightningProcess.SetParameter(power, animationTriggerName);
    }

    public void Attack()
    {
        lightningProcess.Attack();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"当たった：{this.gameObject.name}");

        if (collision.gameObject.tag == TagName.Enemy.ToString())
        {
            lightningProcess.CollisionEnemy(collision);
        }
    }
}
