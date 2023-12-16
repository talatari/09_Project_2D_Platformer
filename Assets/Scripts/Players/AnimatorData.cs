namespace Players
{
    public static class AnimatorData
    {
        public static class Params
        {
            public static readonly int Run = UnityEngine.Animator.StringToHash(nameof(Run));
            public static readonly int Jump = UnityEngine.Animator.StringToHash(nameof(Jump));        
            public static readonly int Attack = UnityEngine.Animator.StringToHash(nameof(Attack));

        }
    }
}