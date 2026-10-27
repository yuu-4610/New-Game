using System.Collections;
using UnityEngine;

public class lightningController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private lightning[] lightningClassReference = new lightning[4];
    private bool attackPermission = default;
    private float coolTimeCount = default; //クールタイムのカウント
    private float coolTime; //クールタイム
    private int level = default;
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
            lightningClassReference[i] = attackObject[i].GetComponent<lightning>();
            //lightningクラスが保持する参照の取得
            Debug.Log(lightningClassReference[i].name);
            //処理順を考慮しここに明言
            lightningClassReference[i].SetClassReference();
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
        Debug.Log($"Lightning：レベル{level}");
        if (level == 1)
        {
            lightningClassReference[0].InitializeProcess(followObject);

            lightningClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
        }
        else if (level == 2)
        {
            lightningClassReference[0].InitializeProcess(followObject); //仮置き

            lightningClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
        }
        else if (level == 3)
        {
            lightningClassReference[0].InitializeProcess(followObject); //仮置き
            lightningClassReference[1].InitializeProcess(followObject);

            lightningClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            lightningClassReference[1].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
            attackNumberOfTimes = 2;
        }
        else if (level == 4)
        {
            lightningClassReference[0].InitializeProcess(followObject); //仮置き
            lightningClassReference[1].InitializeProcess(followObject); //仮置き
            lightningClassReference[2].InitializeProcess(followObject);

            lightningClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            lightningClassReference[1].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            lightningClassReference[2].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            coolTime = 2f;
            attackNumberOfTimes = 3;
        }
        else if (level == 5)
        {
            lightningClassReference[0].InitializeProcess(followObject); //仮置き
            lightningClassReference[1].InitializeProcess(followObject); //仮置き
            lightningClassReference[2].InitializeProcess(followObject); //仮置き

            lightningClassReference[0].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            lightningClassReference[1].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());
            lightningClassReference[2].ChangeParameter(new Vector2(6f, 12f), 1f, AttackEffectAnimationTriggerName.AttackTrigger.ToString());

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

            if (coolTimeCount >= coolTime)
            {
                if (level == 1)
                {
                    lightningClassReference[0].Attack();
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 2)
                {
                    lightningClassReference[0].Attack();
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
        if (numberOfTimes == 2)
        {
            lightningClassReference[0].Attack();
            yield return new WaitForSeconds(0.3f);
            lightningClassReference[1].Attack();
        }
        else if (numberOfTimes == 3)
        {
            lightningClassReference[0].Attack();
            yield return new WaitForSeconds(0.1f);
            lightningClassReference[1].Attack();
            yield return new WaitForSeconds(0.1f);
            lightningClassReference[2].Attack();
        }
        else
        {
            yield return null;
        }
    }
}
