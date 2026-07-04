using UnityEngine;

public class Slash : MonoBehaviour, IEffectParameter
{
    private Animator animator; //アニメーター
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Transform followObjectTransform; //追従するオブジェクト
    private float coolTime; //クールタイム
    private bool isActive = default; //このオブジェクトが活性化しているか確認
    private float coolTimeCount = default; //クールタイムのカウント


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolTimeCount = 0f;
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
            //再生されているアニメーションが指定のものかつ、90%終わっているなら終了処理を行う
            if (animatorStateInfo.IsName(AttackEffectAnimatorName.Attack.ToString()) && animatorStateInfo.normalizedTime > 0.8f)
            {
                Debug.Log("再生できました。");
                ////アニメーションを終了する
                //animator.SetBool(AttackEffectAnimationTriggerName.AttackBool.ToString(), false);
                ////クールタイムのカウントをリセット
                //coolTimeCount = 0f;
            }
        }
    }

    public void InitialSetting(float coolTime, GameObject followObject)
    {
        SetCoolTime(coolTime);
        SetFollowObject(followObject);
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

    private void TargetObjectFollow()
    {
        //対象を追従
        this.gameObject.transform.position = followObjectTransform.position;
    }

    private void Execute()
    {
        Debug.Log($"攻撃");

        //アニメーションを開始
        animator.SetBool(AttackEffectAnimationTriggerName.AttackBool.ToString(), true);
    }
}
