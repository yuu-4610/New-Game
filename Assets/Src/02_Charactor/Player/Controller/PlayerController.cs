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

    public Vector2 moveValues { get; private set; } = Vector2.zero;

    private void Awake()
    {
        playerViewController = GetComponent<PlayerViewController>();
        playerInputActions = new PlayerInputActions();
        playerInputController = new PlayerInputController(playerInputActions);

        //Stateクラスの追加
        stateList.Add(new PlayerStateIdle(this, playerInputActions, playerViewController));
        stateList.Add(new PlayerStateMove(this, playerInputActions, playerViewController));

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

    public void Dmageable(float damage)
    {
        
    }

    public void SetExperiencePoint(int point)
    {
        //Debug.Log($"{point}ポイント取得");
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
