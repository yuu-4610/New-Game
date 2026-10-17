using UnityEngine;

public class Bottom : MonoBehaviour
{
    private BottomProcess bottomProcess;
    private Animator animator; //アニメーター


    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        
    }

    private void Initialize()
    {
        animator = GetComponent<Animator>();
    }
    public void SetClassReference()
    {
        animator = GetComponent<Animator>();
        bottomProcess = new BottomProcess(animator, this.gameObject);
    }

    public void InitializeProcess(GameObject followObject)
    {
        bottomProcess.SetPlayerObjectReference(followObject);
    }

    public void ChangeParameter(Vector2 objectScale, float power, string animationTriggerName)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
        bottomProcess.SetParameter(power, animationTriggerName);
    }

    public void Attack()
    {
        bottomProcess.Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"当たった：{this.gameObject.name}");
        if (collision.gameObject.tag == TagName.Enemy.ToString())
        {
            bottomProcess.CollisionEnemy(collision);
        }
    }
}
