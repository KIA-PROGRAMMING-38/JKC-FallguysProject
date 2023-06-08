using UnityEngine;

namespace LiteralRepository
{
    public static class AnimLiteral
    {
        public static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        public static readonly int IsJumping = Animator.StringToHash("IsJumping");
        public static readonly int IsGrab = Animator.StringToHash("IsGrab");
        public static readonly int IsGrabSuccess = Animator.StringToHash("IsGrabSuccess");
        public static readonly int IsDiving = Animator.StringToHash("IsDiving");
        public static readonly int IsFall = Animator.StringToHash("IsFall");
        public static readonly int IsRespawning = Animator.StringToHash("IsRespawning");
    }
}