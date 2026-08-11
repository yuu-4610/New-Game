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
        //MoveInput = playerInputActions.Player.Move.ReadValue<Vector2>();


        MoveInput = new Vector2(
            (Input.GetKey(KeyCode.D)) ? 1 : (Input.GetKey(KeyCode.A)) ? -1 : 0,
            (Input.GetKey(KeyCode.W)) ? 1 : (Input.GetKey(KeyCode.S)) ? -1 : 0
            );

        return MoveInput;
    }
}
