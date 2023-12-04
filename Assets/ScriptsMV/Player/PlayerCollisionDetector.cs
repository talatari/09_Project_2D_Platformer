using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public PlayerModel PlayerModel { get; private set; }

    public void Init(PlayerModel playerModel) => 
        PlayerModel = playerModel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            if (TryGetComponent(out Collision collision))
                PlayerModel.Collision(collision);
    }
}