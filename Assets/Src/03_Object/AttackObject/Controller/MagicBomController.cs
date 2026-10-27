using UnityEngine;

public class MagicBomController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private MagicBom[] magicBomClassReference = new MagicBom[4];
    private bool attackPermission = default;
    private float coolTimeCount = default; //クールタイムのカウント
    private float coolTime; //クールタイム
    private int level = default;
    private int attackObjectUse;

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
            magicBomClassReference[i] = attackObject[i].GetComponent<MagicBom>();
            //MagicBomクラスが保持する参照の取得
            //処理順を考慮しここに明言
            magicBomClassReference[i].SetClassReference();
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
        Debug.Log($"MagicBom：レベル{level}");
        if (level == 1)
        {
            //MagicBomオブジェクトを１つ用意
            magicBomClassReference[0].InitializeProcess(followObject);
            magicBomClassReference[1].InitializeProcess(followObject);
            magicBomClassReference[2].InitializeProcess(followObject);
            magicBomClassReference[3].InitializeProcess(followObject);


            magicBomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            attackObjectUse = 1;
        }
        else if (level == 2)
        {
            //MagicBomオブジェクトを１つ用意
            magicBomClassReference[0].InitializeProcess(followObject); //仮置き
            magicBomClassReference[1].InitializeProcess(followObject); //仮置き
            magicBomClassReference[2].InitializeProcess(followObject); //仮置き
            magicBomClassReference[3].InitializeProcess(followObject); //仮置き

            magicBomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[1].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[2].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[3].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            attackObjectUse = 1;
        }
        else if (level == 3)
        {
            //MagicBomオブジェクトを１つ用意
            magicBomClassReference[0].InitializeProcess(followObject); //仮置き
            magicBomClassReference[1].InitializeProcess(followObject); //仮置き
            magicBomClassReference[2].InitializeProcess(followObject); //仮置き
            magicBomClassReference[3].InitializeProcess(followObject); //仮置き

            magicBomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[1].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[2].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[3].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            attackObjectUse = 2;
        }
        else if (level == 4)
        {
            //MagicBomオブジェクトを１つ用意
            magicBomClassReference[0].InitializeProcess(followObject); //仮置き
            magicBomClassReference[1].InitializeProcess(followObject); //仮置き
            magicBomClassReference[2].InitializeProcess(followObject); //仮置き
            magicBomClassReference[3].InitializeProcess(followObject); //仮置き

            magicBomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[1].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[2].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[3].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            attackObjectUse = 3;
        }
        else if (level == 5)
        {
            //MagicBomオブジェクトを１つ用意
            magicBomClassReference[0].InitializeProcess(followObject); //仮置き
            magicBomClassReference[1].InitializeProcess(followObject); //仮置き
            magicBomClassReference[2].InitializeProcess(followObject); //仮置き
            magicBomClassReference[3].InitializeProcess(followObject); //仮置き

            magicBomClassReference[0].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[1].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[2].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());
            magicBomClassReference[3].ChangeParameter(new Vector2(1.5f, 1.5f), new Vector2(50f, 0f), 1f, AttackEffectAnimationTriggerName.AttackBool.ToString());

            coolTime = 2f;
            attackObjectUse = 4;
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
                    //待機状態の攻撃オブジェクトを検索
                    for (int i = 0; i < magicBomClassReference.Length; i++)
                    {
                        if (!magicBomClassReference[i].isActive)
                        {
                            magicBomClassReference[i].Attack();
                            break;
                        }
                    }
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 2)
                {
                    //待機状態の攻撃オブジェクトを検索
                    for (int i = 0; i < magicBomClassReference.Length; i++)
                    {
                        if (!magicBomClassReference[i].isActive)
                        {
                            magicBomClassReference[i].Attack();
                            break;
                        }
                    }
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 3)
                {
                    var attackObjectUseCount = 0;
                    //待機状態の攻撃オブジェクトを検索
                    for (int i = 0; i < magicBomClassReference.Length; i++)
                    {
                        if (attackObjectUseCount == attackObjectUse) break;
                        if (!magicBomClassReference[i].isActive)
                        {
                            magicBomClassReference[i].Attack();
                        }
                        attackObjectUseCount++;
                    }
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 4)
                {
                    var attackObjectUseCount = 0;
                    //待機状態の攻撃オブジェクトを検索
                    for (int i = 0; i < magicBomClassReference.Length; i++)
                    {
                        if (attackObjectUseCount == attackObjectUse) break;
                        if (!magicBomClassReference[i].isActive)
                        {
                            magicBomClassReference[i].Attack();
                        }
                        attackObjectUseCount++;
                    }
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
                else if (level == 5)
                {
                    var attackObjectUseCount = 0;
                    //待機状態の攻撃オブジェクトを検索
                    for (int i = 0; i < magicBomClassReference.Length; i++)
                    {
                        if (attackObjectUseCount == attackObjectUse) break;
                        if (!magicBomClassReference[i].isActive)
                        {
                            magicBomClassReference[i].Attack();
                        }
                        attackObjectUseCount++;
                    }
                    //クールタイムのカウントをリセット
                    coolTimeCount = 0f;
                }
            }
        }
    }
}
