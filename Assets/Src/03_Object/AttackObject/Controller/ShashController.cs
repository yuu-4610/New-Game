using UnityEngine;
using UnityEngine.Rendering;

public class ShashController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private Slash[] slashClassReference = new Slash[4];
    private float coolTimeCount = default; //クールタイムのカウント
    private float coolTime; //クールタイム
    private int level = default;
    int count = 0;
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
            slashClassReference[i] = attackObject[i].GetComponent<Slash>();
            //Fireクラスが保持する参照の取得
            //処理順を考慮しここに明言
            slashClassReference[i].SetClassReference();
        }
        //攻撃許可
        LevelSetting(level = 5);
        attackPermission = true;
    }

    public void UpLeveling()
    {
        level++;
        LevelSetting(level);
    }

    private void LevelSetting(int level)
    {
        if (level == 1)
        {
            //Slashオブジェクトを１つ用意
            slashClassReference[0].InitializeProcess(followObject, 0);
            slashClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            coolTime = 3f;
        }
        else if (level == 2)
        {
            slashClassReference[0].InitializeProcess(followObject, 0); //仮置き
            slashClassReference[0].ChangeParameter(new Vector2(2.5f, 2.5f), 3f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            coolTime = 2.6f;
        }
        else if (level == 3)
        {
            slashClassReference[0].InitializeProcess(followObject, 0); //仮置き
            slashClassReference[1].InitializeProcess(followObject, 1);

            slashClassReference[0].ChangeParameter(new Vector2(3.5f, 3.5f), 5f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            slashClassReference[1].ChangeParameter(new Vector2(3.5f, 3.5f), 5f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            coolTime = 2.2f;
        }
        else if(level == 4)
        {
            slashClassReference[0].InitializeProcess(followObject, 0); //仮置き
            slashClassReference[1].InitializeProcess(followObject, 1); //仮置き

            slashClassReference[0].ChangeParameter(new Vector2(5.0f, 5.0f), 7f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            slashClassReference[1].ChangeParameter(new Vector2(5.0f, 5.0f), 7f, AttackEffectAnimationTriggerName.AttackTrigger_2.ToString());
            coolTime = 1.8f;
        }
        else if (level == 5)
        {
            slashClassReference[0].InitializeProcess(followObject, 0); //仮置き
            slashClassReference[1].InitializeProcess(followObject, 1); //仮置き

            slashClassReference[0].ChangeParameter(new Vector2(7.0f, 7.0f), 9f, AttackEffectAnimationTriggerName.AttackTrigger_3.ToString());
            slashClassReference[1].ChangeParameter(new Vector2(7.0f, 7.0f), 9f, AttackEffectAnimationTriggerName.AttackTrigger_3.ToString());
            coolTime = 1.5f;
        }
    }

    //生成後設定
    private void Initialize()
    {
        coolTimeCount = 0f;
        level = 1;
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    private void Execute()
    {
        if (attackPermission)
        {
            //Debug.Log($"計測開始{++count}：{coolTimeCount}");
            coolTimeCount += Time.deltaTime;

            if (coolTimeCount >= coolTime)
            {
                if (level == 1)
                {
                    slashClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                if (level == 2)
                {
                    slashClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                if (level == 3)
                {
                    slashClassReference[0].Attack();
                    slashClassReference[1].Attack();
                    coolTimeCount = 0f;
                }
                if (level == 4)
                {
                    slashClassReference[0].Attack();
                    slashClassReference[1].Attack();
                    coolTimeCount = 0f;
                }
                if (level == 5)
                {
                    slashClassReference[0].Attack();
                    slashClassReference[1].Attack();
                    coolTimeCount = 0f;
                }

            }
        }
    }
}
