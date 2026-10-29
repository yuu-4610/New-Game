using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine<PlayerController, PlayerState>, IPlayer
{
    [Header("移動スピード値")]
    [SerializeField] float moveSpeed = 0f;
    private PlayerInputController playerInputController;
    private PlayerInputActions playerInputActions;
    private PlayerViewController playerViewController;
    private int level = 0;
    private int restHP;
    private int power;

    public Vector2 moveValues { get; private set; } = Vector2.zero;

    private void Awake()
    {
        playerViewController = GetComponent<PlayerViewController>();
        playerInputActions = new PlayerInputActions();
        playerInputController = new PlayerInputController(playerInputActions);


        //Stateクラスの追加
        stateList.Add(new PlayerStateIdle(this, playerInputActions, playerViewController));
        stateList.Add(new PlayerStateMove(this, playerInputActions, playerViewController));
        stateList.Add(new PlayerStateDie(this, playerViewController));

        if (gameObject.tag != AcquisitionObjectName.Player.ToString()) gameObject.tag = AcquisitionObjectName.Player.ToString();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        ChangeStateCall(PlayerState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        //入力値の検知
        moveValues = playerInputController.MoveValue().normalized;
        //継承元クラスのUpdate()を実行
        base.Update();
    }

    //
    public void ChangeStateCall(PlayerState playerState)
    {
        base.ChangeState(stateList[(int)playerState]);
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    public void UpLeveling(int hp, int power)
    {
        ++level;
        LevelSetting(hp, power);
    }

    private void LevelSetting(int maxHP, int power)
    {
        //残HPの更新＝全回復
        restHP = maxHP;
        this.power = power;

        //HPバーの設定
        EventManager.Instance.PlayerHPBarSettingEvent(restHP);
    }

    public void Damageable(float damage)
    {
        //ダメージ減少
        restHP -= (int)damage;
        Debug.Log($"残りHP{restHP}");
        //HPバーの設定（HP減少）
        EventManager.Instance.PlayerRestHpBarSettingEvent(restHP);

        if(restHP <= 0) ChangeStateCall(PlayerState.Die);
    }

    public void SetExperiencePoint(int point)
    {
        //Debug.Log($"{point}ポイント取得");
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
