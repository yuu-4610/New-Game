using UnityEngine;

public class Slash : MonoBehaviour, IEffectParameter
{
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private Quaternion followObjectRotation;
    private float coolTime; //クールタイム
    private float attackPower = default;
    private float coolTimeCount = default; //クールタイムのカウント
    private float followObjectDistance = default; //追従対象との距離
    private float currentPosiotionXValue = default;
    private float curentXPosition = default;
    private bool isActive = default; //このオブジェクトが活性化しているか確認　今のところ活用場面なし
    private bool isStartAnime = default;
    private bool attackPermission = default;


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
        if (!isActive)
        {
            Debug.Log("非活性状態です");
        }
        TargetObjectFollow();
        Execute();

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

    public void SetAttackPermission(bool permission)
    {
        attackPermission = permission;
    }

    //座標の更新、プレイヤーを起点に更新していく
    private void TargetObjectFollow()
    {   
        //向いている方向に飛ばす
        if (currentPosiotionXValue < followObjectTransform.position.x)
        {
            curentXPosition = followObjectTransform.position.x + followObjectDistance;

            this.transform.rotation = followObjectRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (currentPosiotionXValue > followObjectTransform.position.x)
        {
            curentXPosition = followObjectTransform.position.x - followObjectDistance;

            this.transform.rotation = followObjectRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            curentXPosition = (followObjectRotation.y > 0) ? followObjectTransform.position.x - followObjectDistance : followObjectTransform.position.x + followObjectDistance;
            this.transform.rotation = followObjectRotation;
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
        if (attackPermission)
        {
            coolTimeCount += Time.deltaTime;

            if (coolTimeCount >= coolTime)
            {
                if (!isStartAnime)
                {
                    //アニメーションを開始
                    animator.SetBool(AttackEffectAnimationTriggerName.AttackBool.ToString(), true);
                    isStartAnime = true;
                }
                //現在のアニメーションのステートを取得ー＞想定：Attack
                animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                //再生されているアニメーションが指定のものかつ、90%終わっているなら終了処理を行う
                if (animatorStateInfo.IsName(AttackEffectAnimatorName.Attack.ToString()) && animatorStateInfo.normalizedTime > 0.9f) // && animatorStateInfo.normalizedTime > 0.9f
                {
                    //アニメーションを終了する
                    animator.SetBool(AttackEffectAnimationTriggerName.AttackBool.ToString(), false);
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                    isStartAnime = false;

                    //現在のアニメーションのステートを取得ー＞想定：Attack_Stand
                    animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                }
            }
        }
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
