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
        // MonoBehaviour' instances must be instantiated with 'GameObject.AddComponent<T>()' instead of 'new'
        // PlayerFactory playerFactory = new PlayerFactory();
        // TODO: так создавать правильней, чем на строчку выше?
        PlayerFactory playerFactory = gameObject.AddComponent<PlayerFactory>();
        // TODO: зачем мы тут создаем модель, если у нас фабрика умеет создавать модель игрока?
        // PlayerModel playerModel = new PlayerModel();

        // _level = new Level(playerFactory, playerModel);
        _level = new Level(playerFactory);
        _level.Start();
    }
}