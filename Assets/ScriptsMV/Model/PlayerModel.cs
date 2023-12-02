using System;
using UnityEngine;

public class PlayerModel
{
    public event Action<Collision> Collided;
    
    public void Collision(Collision other) =>
        Collided?.Invoke(other);
}