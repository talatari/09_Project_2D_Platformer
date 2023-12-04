using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerFactory
{
    private GameObjectCreator _gameObjectCreator = new ();

    public PlayerView CreatePlayerView(PlayerModel playerModel)
    {
        PlayerView playerView = _gameObjectCreator.Create<PlayerView>();
        playerView.name = "PlayerView";
        playerView.Init(playerModel);
        CreateCollisionDetector(playerModel).transform.SetParent(playerView.transform);
        CreatePlayerAnimator().transform.SetParent(playerView.transform);
        return playerView;
    }

    private PlayerCollisionDetector CreateCollisionDetector(PlayerModel playerModel)
    {
        PlayerCollisionDetector playerCollisionDetector = _gameObjectCreator.Create<PlayerCollisionDetector>();
        playerCollisionDetector.name = "PlayerCollisionDetector";
        playerCollisionDetector.Init(playerModel);
        return playerCollisionDetector;
    }
    
    private PlayerAnimator CreatePlayerAnimator()
    {
        PlayerAnimator playerAnimator = _gameObjectCreator.Create<PlayerAnimator>();
        playerAnimator.name = "PlayerAnimator";
        return playerAnimator;
    }
}

public class GameObjectCreator
{
    public T Create<T>() where T : Object => 
        Object.Instantiate(Resources.Load<T>(typeof(T).Name));
}