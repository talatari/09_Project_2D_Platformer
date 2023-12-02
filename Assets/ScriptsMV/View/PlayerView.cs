using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerModel PlayerModel { get; private set; }

    public void Init(PlayerModel playerModel)
    {
        PlayerModel = playerModel;
        PlayerModel.Collided += OnCollided;
    }

    public void OnDestroy() =>
        PlayerModel.Collided -= OnCollided;

    private void OnCollided(Collision collision)
    {
        print("PlayerView узнала о столкновении модели и что-то делает");
    }
}