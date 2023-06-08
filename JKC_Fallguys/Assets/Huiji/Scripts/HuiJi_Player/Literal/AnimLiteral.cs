using UnityEngine;

namespace Literal
{
    public static class AnimLiteral
    {
        public static readonly int MOVESPEED = Animator.StringToHash("MoveSpeed");
        public static readonly int ISJUMPING = Animator.StringToHash("IsJumping");
        public static readonly int ISGRAB = Animator.StringToHash("IsGrab");
        public static readonly int ISGRABSUCCESS = Animator.StringToHash("IsGrabSuccess");
        public static readonly int ISDIVING = Animator.StringToHash("IsDiving");
        public static readonly int ISFALL = Animator.StringToHash("IsFall");
        public static readonly int ISRESPAWNING = Animator.StringToHash("IsRespawning");
    }
}