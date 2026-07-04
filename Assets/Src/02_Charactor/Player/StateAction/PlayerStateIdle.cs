using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateIdle : State<PlayerController>
{
    private PlayerInputActions playerInputActions;
    private PlayerViewController playerViewController;
    public PlayerStateIdle(PlayerController owner, PlayerInputActions playerInputActions, PlayerViewController playerViewController) : base(owner)
    {
        this.playerInputActions = playerInputActions;
        this.playerViewController = playerViewController;
    }

    public override void Enter()
    {
        //InputAction‚ÌƒCƒxƒ“ƒg“o˜^
        playerInputActions.Player.Move.performed += TransitionStateIdleToMove;
    }

    public override void Execute()
    {
        playerViewController.PlayerIdleAnimation();
    }

    public override void Exit()
    {
        playerInputActions.Player.Move.performed -= TransitionStateIdleToMove;
    }

    //ˆÚ“®—p‚Ì“ü—Í‚ðŒŸ’m‚µ‚½‚ç‘JˆÚ
    private void TransitionStateIdleToMove(InputAction.CallbackContext context)
    {
        owner.ChangeStateCall(PlayerState.Move);
    }
}
