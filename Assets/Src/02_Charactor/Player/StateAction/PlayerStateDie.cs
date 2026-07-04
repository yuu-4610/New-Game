using UnityEngine;

public class PlayerStateDie : State<PlayerController>
{
    private PlayerViewController playerViewController;
    public PlayerStateDie(PlayerController owner, PlayerViewController playerViewController) : base(owner)
    {
        this.playerViewController = playerViewController;
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        //アニメーション
        playerViewController.PlayerDieAnimation();
    }
}
