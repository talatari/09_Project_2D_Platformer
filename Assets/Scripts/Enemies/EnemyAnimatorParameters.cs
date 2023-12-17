using UnityEngine;

namespace Enemies
{
    public static class EnemyAnimatorParameters
    {
        public static readonly int Move = Animator.StringToHash(nameof(Move));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}