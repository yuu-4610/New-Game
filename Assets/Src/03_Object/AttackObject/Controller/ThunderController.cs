using System.Collections;
using UnityEngine;

public class ThunderController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private Thunder[] thunderClassReference = new Thunder[4];

    private float coolTimeCount = default; //クールタイムのカウント

    private float coolTime; //クールタイム
    private int level = default;
    private bool attackPermission = default;
    private int attackNumberOfTimes = default;

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
            thunderClassReference[i] = attackObject[i].GetComponent<Thunder>();
            //Thunderクラスが保持する参照の取得
            //処理順を考慮しここに明言
            thunderClassReference[i].SetClassReference();
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
        Debug.Log($"Thunder：レベル{level}");
        if (level == 1)
        {
            //Fireオブジェクトを１つ用意
            thunderClassReference[0].InitializeProcess(followObject);
            
            thunderClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
        }
        else if (level == 2)
        {
            //Fireオブジェクトを１つ用意
            thunderClassReference[0].InitializeProcess(followObject); //仮置き
            
            thunderClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
        }
        else if (level == 3)
        {
            //Fireオブジェクトを１つ用意
            thunderClassReference[0].InitializeProcess(followObject); //仮置き
            thunderClassReference[1].InitializeProcess(followObject);

            thunderClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            thunderClassReference[1].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
            attackNumberOfTimes = 2;
        }
        else if(level == 4)
        {
            //Fireオブジェクトを１つ用意
            thunderClassReference[0].InitializeProcess(followObject); //仮置き
            thunderClassReference[1].InitializeProcess(followObject); //仮置き
            thunderClassReference[2].InitializeProcess(followObject);

            thunderClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            thunderClassReference[1].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            thunderClassReference[2].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
            attackNumberOfTimes = 3;
        }
        else if (level == 5)
        {
            //Fireオブジェクトを１つ用意
            thunderClassReference[0].InitializeProcess(followObject); //仮置き
            thunderClassReference[1].InitializeProcess(followObject); //仮置き
            thunderClassReference[2].InitializeProcess(followObject);

            thunderClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            thunderClassReference[1].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            thunderClassReference[2].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
            attackNumberOfTimes = 3;
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
            //Debug.Log($"計測開始：{coolTimeCount}");

            if (coolTimeCount >= coolTime)
            {
                if (level == 1)
                {
                    thunderClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if(level == 2)
                {
                    thunderClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 3)
                {
                    StartCoroutine(ContinuityAttack(attackNumberOfTimes));
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 4)
                {
                    StartCoroutine(ContinuityAttack(attackNumberOfTimes));
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 5)
                {
                    StartCoroutine(ContinuityAttack(attackNumberOfTimes));
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
            }
        }
    }

    private IEnumerator ContinuityAttack(int numberOfTimes)
    {
        if(numberOfTimes == 2)
        {
            thunderClassReference[0].Attack();
            yield return new WaitForSeconds(0.3f);
            thunderClassReference[1].Attack();
        }
        else if(numberOfTimes == 3)
        {
            thunderClassReference[0].Attack();
            yield return new WaitForSeconds(0.1f);
            thunderClassReference[1].Attack();
            yield return new WaitForSeconds(0.1f);
            thunderClassReference[2].Attack();
        }
        else
        {
            yield return null;
        }
    }
}
