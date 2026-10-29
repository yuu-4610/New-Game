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
        //アニメーション
        playerViewController.PlayerDieAnimation();
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        //ゲーム終了処理の通知
        Debug.Log("ゲーム終了");
    }
}
