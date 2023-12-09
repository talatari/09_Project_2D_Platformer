using UnityEngine;

public static class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int Move = Animator.StringToHash(nameof(Move));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}