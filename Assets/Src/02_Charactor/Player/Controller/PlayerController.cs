using System.Xml.Serialization;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine<PlayerController, PlayerState>, IPlayer
{
    [Header("移動スピード値")]
    [SerializeField] float moveSpeed = 0f;

    private CharacterController characterController;
    private PlayerInputController playerInputController;
    private PlayerInputActions playerInputActions;
    private PlayerViewController playerViewController;

    public Vector2 moveValues { get; private set; } = Vector2.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerViewController = GetComponent<PlayerViewController>();
        playerInputActions = new PlayerInputActions();
        playerInputController = new PlayerInputController(playerInputActions);

        //Stateクラスの追加
        stateList.Add(new PlayerStateIdle(this, playerInputActions, playerViewController));
        stateList.Add(new PlayerStateMove(this, characterController, playerInputActions, playerViewController));

        //このオブジェクトの参照を登録
        //ObjectManager.Instance.Register(AcquisitionObjectName.Player.ToString(), this.gameObject);
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
        //Move();
        //継承元クラスのUpdate()を実行
        base.Update();

        //Debug.Log($"base.currentState{base.currentState}");
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

    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
