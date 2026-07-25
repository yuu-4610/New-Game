using System.Xml.Serialization;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine<PlayerController, PlayerState>
{
    [Header("移動スピード値")]
    [SerializeField] float moveSpeed = 0f;

    private Animator animator;
    private CharacterController characterController;
    private PlayerInputController playerInputController;
    private PlayerInputActions playerInputActions;
    private PlayerViewController playerViewController;

    public Vector2 moveValues { get; private set; } = Vector2.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerViewController = GetComponent<PlayerViewController>();
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputController = new PlayerInputController(playerInputActions);

        //Stateクラスの追加
        stateList.Add(new PlayerStateIdle(this, playerInputActions, playerViewController));
        stateList.Add(new PlayerStateMove(this, characterController, playerInputActions, playerViewController));

        ObjectManager.Instance.Register(AcquisitionObjectName.Player.ToString(), this.gameObject);
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

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
