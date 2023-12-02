using UnityEngine;

public class PlayerFactory
{
    public PlayerModel CreatePlayerModel() =>
        new ();
    
    public PlayerView CreatePlayerView() => 
        Object.Instantiate(Resources.Load<PlayerView>("Player/Player"));

    public PlayerCollisionDetector CreateCollisionDetector() =>
        Object.Instantiate(Resources.Load<PlayerCollisionDetector>("Player/PlayerCollisionDetector"));
}