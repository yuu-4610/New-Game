using UnityEngine;

public class Fire : MonoBehaviour
{
    private FireProcess fireProcess;
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
        fireProcess.TargetObjectFollow();
    }

    private void Initialize()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetClassReference()
    {
        animator = GetComponent<Animator>();
        fireProcess = new FireProcess(animator, this.gameObject);
    }

    public void InitializeProcess(GameObject followObject, int objectIndex)
    {
        fireProcess.InitializeProcess(followObject, objectIndex);
    }

    public void ChangeParameter(Vector2 objectScale, float speed, float power, string animationTriggerName, string animationTriggerName_2)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        fireProcess.SetParameter(speed, power, animationTriggerName, animationTriggerName_2);
    }

    public void Attack()
    {
        fireProcess.Attack();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"当たった：{this.gameObject.name}");

        if (collision.gameObject.tag == TagName.Enemy.ToString())
        {
            fireProcess.CollisionEnemy(collision);
        }
    }
}
