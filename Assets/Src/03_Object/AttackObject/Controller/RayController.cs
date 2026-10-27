using UnityEngine;

public class RayController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private Ray[] rayClassReference = new Ray[4];

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
        Debug.Log($"Ray：レベル{level}");
        this.gameObject.transform.parent = followObject.transform;
        this.followObject = followObject;
        for (int i = 0; i < attackObject.Length; i++)
        {
            rayClassReference[i] = attackObject[i].GetComponent<Ray>();
            //Fireクラスが保持する参照の取得
            //処理順を考慮しここに明言
            rayClassReference[i].SetClassReference();
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
        if (level == 1)
        {
            //Fireオブジェクトを１つ用意
            rayClassReference[0].InitializeProcess(followObject, 0);

            rayClassReference[0].ChangeParameter(new Vector2(3.0f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 3f;
        }
        else if (level == 2)
        {
            //Fireオブジェクトを１つ用意
            rayClassReference[0].InitializeProcess(followObject, 0); //仮置き

            rayClassReference[0].ChangeParameter(new Vector2(3.0f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 3f;
        }
        else if (level == 3)
        {
            //Fireオブジェクトを１つ用意
            rayClassReference[0].InitializeProcess(followObject, 0); //仮置き
            rayClassReference[1].InitializeProcess(followObject, 1);

            rayClassReference[0].ChangeParameter(new Vector2(3.0f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            rayClassReference[1].ChangeParameter(new Vector2(3.0f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 3f;
        }
        else if (level == 4)
        {
            //Fireオブジェクトを１つ用意
            rayClassReference[0].InitializeProcess(followObject, 0); //仮置き
            rayClassReference[1].InitializeProcess(followObject, 1); //仮置き

            rayClassReference[0].ChangeParameter(new Vector2(3.0f, 3.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            rayClassReference[1].ChangeParameter(new Vector2(3.0f, 3.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            
            coolTime = 3f;
        }
        else if (level == 5)
        {
            //Fireオブジェクトを１つ用意
            rayClassReference[0].InitializeProcess(followObject, 0); //仮置き
            rayClassReference[1].InitializeProcess(followObject, 1); //仮置き
            rayClassReference[2].InitializeProcess(followObject, 2);
            rayClassReference[3].InitializeProcess(followObject, 3);


            rayClassReference[0].ChangeParameter(new Vector2(3.0f, 3.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            rayClassReference[1].ChangeParameter(new Vector2(3.0f, 3.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            rayClassReference[2].ChangeParameter(new Vector2(3.0f, 3.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            rayClassReference[3].ChangeParameter(new Vector2(3.0f, 3.0f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 3f;
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
                    rayClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                if (level == 2)
                {
                    rayClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                if (level == 3)
                {
                    rayClassReference[0].Attack();
                    rayClassReference[1].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                if (level == 4)
                {
                    rayClassReference[0].Attack();
                    rayClassReference[1].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                if (level == 5)
                {
                    rayClassReference[0].Attack();
                    rayClassReference[1].Attack();
                    rayClassReference[2].Attack();
                    rayClassReference[3].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
            }
        }
    }
}
