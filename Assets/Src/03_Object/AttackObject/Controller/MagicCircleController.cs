using UnityEngine;

public class MagicCircleController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private MagicCircle[] magicCircleClassReference = new MagicCircle[4];
    private float coolTimeCount = default; //クールタイムのカウント
    private float coolTime; //クールタイム
    private int level = default;
    private bool attackPermission = default;
    private float duration = default;

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
            magicCircleClassReference[i] = attackObject[i].GetComponent<MagicCircle>();
            //MagicCircleクラスが保持する参照の取得
            //処理順を考慮しここに明言
            magicCircleClassReference[i].SetClassReference();
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
        Debug.Log($"MagicCircle：レベル{level}");
        if (level == 1)
        {
            //MagicCircleオブジェクトを１つ用意
            magicCircleClassReference[0].InitializeProcess(followObject);

            magicCircleClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            duration = 2f;

        }
        else if (level == 2)
        {
            //MagicCircleオブジェクトを１つ用意
            magicCircleClassReference[0].InitializeProcess(followObject);

            magicCircleClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            duration = 2f;
        }
        else if (level == 3)
        {
            //MagicCircleオブジェクトを１つ用意
            magicCircleClassReference[0].InitializeProcess(followObject);

            magicCircleClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            duration = 2f;
        }
        else if (level == 4)
        {
            //MagicCircleオブジェクトを１つ用意
            magicCircleClassReference[0].InitializeProcess(followObject);

            magicCircleClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            duration = 2f;
        }
        else if (level == 5)
        {
            //MagicCircleオブジェクトを１つ用意
            magicCircleClassReference[0].InitializeProcess(followObject);

            magicCircleClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            duration = 2f;
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
                magicCircleClassReference[0].Attack(true);
                //クールタイムのカウントをリセット
                if (coolTimeCount >= coolTime + duration)
                {
                    magicCircleClassReference[0].Attack(false);
                    coolTimeCount = 0f;
                }
            }
        }
    }
}
