using UnityEngine;

public class FireController : MonoBehaviour, IAttackControl
{
    private GameObject followObject;
    private Fire[] fireClassReference = new Fire[4];
    
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
            fireClassReference[i] = attackObject[i].GetComponent<Fire>();
            //Fireクラスが保持する参照の取得
            //処理順を考慮しここに明言
            fireClassReference[i].SetClassReference();
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
        if(level == 1)
        {
            //Fireオブジェクトを１つ用意
            fireClassReference[0].InitializeProcess(followObject, 0);
            fireClassReference[0].ChangeParameter(new Vector2(8f, 8f), 5f, 1f, AttackEffectAnimationTriggerName.AttackBool.ToString(), AttackEffectAnimationTriggerName.AttackTrigger.ToString());

            fireClassReference[1].gameObject.SetActive(false);
            fireClassReference[2].gameObject.SetActive(false);
            fireClassReference[3].gameObject.SetActive(false);

            coolTime = 4f;
        }
        else if(level == 2)
        {
            
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
                if(level == 1)
                {
                    Debug.Log("発射");
                    fireClassReference[0].Attack();

                    coolTimeCount = 0f;
                }
            }
        }
    }
}
