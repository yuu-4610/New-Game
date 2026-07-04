using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController
{
    private PlayerInputActions playerInputActions;
    public Vector2 MoveInput { get; private set; }


    public PlayerInputController(PlayerInputActions playerInputActions)
    {
        this.playerInputActions = playerInputActions;
        playerInputActions.Enable();
    }

    //“ü—Í‚ðˆÚ“®’l‚É•ÏŠ·
    public Vector2 MoveValue()
    {
        MoveInput = playerInputActions.Player.Move.ReadValue<Vector2>();

        return MoveInput;
    }
}
