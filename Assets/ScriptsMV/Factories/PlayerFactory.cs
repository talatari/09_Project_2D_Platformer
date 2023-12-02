using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    public PlayerModel CreatePlayerModel() =>
        new ();

    public PlayerView CreatePlayerView() =>
        Instantiate(Resources.Load<PlayerView>(
            "Player/Player"));

    public PlayerCollicionDetector CreateCollisionDetector() =>
        Instantiate(Resources.Load<PlayerCollicionDetector>(
            "Player/PlayerCollicionDetector"));
}