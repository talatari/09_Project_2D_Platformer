using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public static Bootstrapper Instance;
    
    private Level _level;

    private void Awake()
    {
        InitializeSingleton();
        CreateLevel();
    }

    private void InitializeSingleton()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        
        if (Instance is null)
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void CreateLevel()
    {
        PlayerFactory playerFactory = new PlayerFactory();
        PlayerModel playerModel = new PlayerModel();
        
        PlayerView playerView = playerFactory.CreatePlayerView(playerModel);

        _level = new Level(playerView);
        _level.Start();
    }
}