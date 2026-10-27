using UnityEngine;

public class BottomController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private Bottom[] bottomClassReference = new Bottom[4];

    private float coolTimeCount = default; //クールタイムのカウント

    private float coolTime; //クールタイム
    private int level = default;
    private bool attackPermission = default;


    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Execute();
    }
    public void IsAddObject(GameObject[] attackObject, GameObject followObject)
    {
        this.gameObject.transform.parent = followObject.transform;
        this.followObject = followObject;
        for (int i = 0; i < attackObject.Length; i++)
        {
            bottomClassReference[i] = attackObject[i].GetComponent<Bottom>();
            //Fireクラスが保持する参照の取得
            //処理順を考慮しここに明言
            bottomClassReference[i].SetClassReference();
        }
        //攻撃許可
        LevelSetting(level = 1);
        attackPermission = true;
    }
    public void UpLeveling()
    {
        level++;
        LevelSetting(level);
    }

    private void LevelSetting(int level)
    {
        Debug.Log($"Bottom：レベル{level}");
        if (level == 1)
        {
            //Fireオブジェクトを１つ用意
            bottomClassReference[0].InitializeProcess(followObject);

            bottomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            coolTime = 4f;
        }
        else if (level == 2)
        {
            bottomClassReference[0].InitializeProcess(followObject); //仮置き

            bottomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            coolTime = 3.7f;
        }
        else if (level == 3)
        {
            bottomClassReference[0].InitializeProcess(followObject); //仮置き
            bottomClassReference[1].InitializeProcess(followObject);
            
            bottomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            bottomClassReference[1].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            coolTime = 3.4f;
        }
        else if (level == 4)
        {
            bottomClassReference[0].InitializeProcess(followObject); //仮置き
            bottomClassReference[1].InitializeProcess(followObject); //仮置き
            bottomClassReference[2].InitializeProcess(followObject);

            bottomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            bottomClassReference[1].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            bottomClassReference[2].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            coolTime = 2.9f;
        }
        else if (level == 5)
        {
            bottomClassReference[0].InitializeProcess(followObject); //仮置き
            bottomClassReference[1].InitializeProcess(followObject); //仮置き
            bottomClassReference[2].InitializeProcess(followObject); //仮置き

            bottomClassReference[0].ChangeParameter(new Vector2(4.0f, 4.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            bottomClassReference[1].ChangeParameter(new Vector2(4.0f, 4.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            bottomClassReference[2].ChangeParameter(new Vector2(4.0f, 4.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            coolTime = 2.5f;
        }
    }

    //生成後設定
    private void Initialize()
    {
        coolTimeCount = 0f;
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    private void Execute()
    {
        if (attackPermission)
        {
            coolTimeCount += Time.deltaTime;

            if (coolTimeCount >= coolTime)
            {
                if (level == 1)
                {
                    bottomClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 2)
                {
                    bottomClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 3)
                {
                    bottomClassReference[0].Attack();
                    bottomClassReference[1].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 4)
                {
                    bottomClassReference[0].Attack();
                    bottomClassReference[1].Attack();
                    bottomClassReference[2].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 5) 
                {
                    bottomClassReference[0].Attack();
                    bottomClassReference[1].Attack();
                    bottomClassReference[2].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
            }
        }
    }
}
