using UnityEngine;

public class PlayerStateMove : State<PlayerController>
{
    private CharacterController characterController;
    private PlayerInputActions playerInputActions;
    private PlayerViewController playerViewController;
    private float moveSpeed = 2;
    private bool isMove = default;

    public PlayerStateMove(PlayerController owner, CharacterController characterController, PlayerInputActions playerInputActions, PlayerViewController playerViewController) : base(owner)
    {
        this.characterController = characterController;
        this.playerInputActions = playerInputActions;
        this.playerViewController = playerViewController;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Enter()
    {
        isMove = true;
    }

    public override void Execute()
    {
        //移動処理
        characterController.Move(owner.moveValues * moveSpeed * Time.deltaTime);
        //Debug.Log($"owner.moveValues：{owner.moveValues}");
        //Debug.Log($"moveSpeed：{moveSpeed}");

        //アニメーション
        playerViewController.PlayerMoveAnimation(owner.moveValues.x);

        //
        if (owner.moveValues == Vector2.zero)
        {
            //左右キーの連続入力を可能にする
            if (!isMove)
            {
                owner.ChangeStateCall(PlayerState.Idle);
            }

            isMove = false;
        }
    }

    public override void Exit()
    {

    }
}
