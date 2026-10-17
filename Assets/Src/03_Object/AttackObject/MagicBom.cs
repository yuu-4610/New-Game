using UnityEngine;

public class MagicBom : MonoBehaviour
{
    private MagicBomProcess magicBomProcess;
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private BoxCollider2D boxCollider2D;
    public bool isActive { get; private set; } = default;
    

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
        magicBomProcess = new MagicBomProcess(animator, this.gameObject);
        Debug.Log("初期処理完了：MagicBomProcess");
    }

    public void InitializeProcess(GameObject followObject)
    {
        magicBomProcess.SetPlayerObjectReference(followObject);
    }

    //座標の更新、プレイヤーを起点に更新していく


    public void ChangeParameter(Vector2 objectScale, Vector2 objectRotation, float power, string animationTriggerName)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        this.gameObject.transform.rotation = Quaternion.Euler(
            objectRotation.x,
            objectRotation.y,
            this.gameObject.transform.rotation.z);
        magicBomProcess.SetParameter(power, animationTriggerName);
    }

    public void Attack()
    {
        isActive = true;
        magicBomProcess.Attack();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"当たった：{this.gameObject.name}");

        if (collision.gameObject.tag == TagName.Enemy.ToString())
        {
            magicBomProcess.CollisionEnemy(collision);
            isActive = false;
        }
    }
}
