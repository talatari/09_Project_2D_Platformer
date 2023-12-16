namespace Enemies
{
    public static class AnimatorData
    {
        public static class Params
        {
            public static readonly int Move = UnityEngine.Animator.StringToHash(nameof(Move));
            public static readonly int Attack = UnityEngine.Animator.StringToHash(nameof(Attack));
        }
    }
}