using UnityEngine;

public class Slash : MonoBehaviour, IEffectParameter
{
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private float coolTime; //クールタイム
    private bool isActive = default; //このオブジェクトが活性化しているか確認
    private float attackPower = default;
    private float coolTimeCount = default; //クールタイムのカウント
    private float followObjectDistance = default; //追従対象との距離
    private float currentPosiotionXValue = default;
    private float curentXPosition = default;


    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        coolTimeCount = 0f;
        attackPower = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //非活性かつクールタイムが設定されてない（この攻撃が登録されてない）場合は処理続行不可
        if (!isActive || coolTime == 0)
        {
            Debug.Log("非活性、もしくは登録されてません");
            return;
        }
        TargetObjectFollow();

        coolTimeCount += Time.deltaTime;

        if (coolTimeCount >= coolTime)
        {
            Execute();
            //現在のアニメーションのステートを取得ー＞想定：Attack
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //再生されているアニメーションが指定のものかつ、90%終わっているなら終了処理を行う
            if (animatorStateInfo.IsName(AttackEffectAnimatorName.Attack.ToString()) && animatorStateInfo.normalizedTime > 0.9f) // && animatorStateInfo.normalizedTime > 0.9f
            {
                //アニメーションを終了する
                animator.SetBool(AttackEffectAnimationTriggerName.AttackBool.ToString(), false);
                //クールタイムのカウントをリセット
                coolTimeCount = 0f;

                //現在のアニメーションのステートを取得ー＞想定：Attack_Stand
                animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            }
        }
    }

    private void Initialize()
    {
        animator = GetComponent<Animator>();
    }

    public void InitialSetting(float coolTime, GameObject followObject)
    {
        SetCoolTime(coolTime);
        SetFollowObject(followObject);
    }

    public void SetObjectPosition(float followObjectDistance, Vector2 objectScale)
    {
        this.followObjectDistance = followObjectDistance;
        UpScale(objectScale);
    }

    private void TargetObjectFollow()
    {   
        //1フレーム前に取得した追従しているオブジェクトのX座標より、現在の追従オブジェクトのX座標の方が大きければ
        if (currentPosiotionXValue < followObjectTransform.position.x)
        {
            curentXPosition = followObjectTransform.position.x + followObjectDistance;

            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //1フレーム前に取得した追従しているオブジェクトのX座標より、現在の追従オブジェクトのX座標の方が小さければ
        else if (currentPosiotionXValue > followObjectTransform.position.x)
        {
            curentXPosition = followObjectTransform.position.x - followObjectDistance;

            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //対象を追従
        this.gameObject.transform.position = new Vector3(curentXPosition, followObjectTransform.position.y, followObjectTransform.position.z);

        //最新のX座標を取得
        //追従しているオブジェクトのX座標を取得
        currentPosiotionXValue = followObjectTransform.transform.position.x;
    }

    private void UpScale(Vector2 objectScale)
    {
        //このオブジェクトのスケール（大きさ）を更新
        this.gameObject.transform.localScale = new Vector3(
            objectScale.x,
            objectScale.y,
            this.gameObject.transform.localScale.z);
    }

    private void Execute()
    {
        //アニメーションを開始
        animator.SetBool(AttackEffectAnimationTriggerName.AttackBool.ToString(), true);
    }

    //クールタイムをセット
    //登録時に呼ばれる
    public void SetCoolTime(float coolTime)
    {
        this.coolTime = coolTime;
        isActive = true;
    }

    private void SetFollowObject(GameObject followObject)
    {
        followObjectTransform = followObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IEnemy>(out var damageable))
        {
            damageable.TakeDamage(attackPower);
        }
    }
}
