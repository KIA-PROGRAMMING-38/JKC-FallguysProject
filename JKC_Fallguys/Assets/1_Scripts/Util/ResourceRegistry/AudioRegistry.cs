using System;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using UnityEngine;

namespace ResourceRegistry
{
    public static class AudioRegistry
    {
        static AudioRegistry()
        {
            SetAudioClips().Forget();
        }

        private static async UniTaskVoid SetAudioClips()
        {
            LoginSFX = DataManager.GetAudioClip( PathLiteral.Sounds, PathLiteral.Music, PathLiteral.LoginSound );
            LobbyMusic = Resources.LoadAll<AudioClip>( DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Lobby ) );
            RoundMusic = Resources.LoadAll<AudioClip>( DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Stage, PathLiteral.RoundMusic ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            FinalRoundMusic = Resources.LoadAll<AudioClip>( DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Stage, PathLiteral.FinalRoundMusic ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            FallGuySFXOnRoundResult = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.RoundResult ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            GameResultMusic = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Result ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            VictoryFallGuySFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.GameResult, PathLiteral.Victory ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            LoseFallGuySFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.GameResult, PathLiteral.Lose ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            WalkFootStepSFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Walk ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            RunFootStepSFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Run ) );

            await UniTask.Delay( TimeSpan.FromSeconds( 0.3f ) );

            JumpSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Jump, "F_Footsteps_Jump_Soft_01" ) );
            DiveSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Dive, "F_Footsteps_Dive_01"));
            FallSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Fall, "SFX_ElimBoard_CartoonHit"));
            RespawnSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, "SFX_Respawn_3D"));
        }

        public static AudioClip LoginSFX { get; private set; }
        public static AudioClip[] LobbyMusic { get; private set; }
        public static AudioClip[] RoundMusic { get; private set; } = new AudioClip[3];
        public static AudioClip[] FinalRoundMusic { get; private set; }
        public static AudioClip[] FallGuySFXOnRoundResult { get; private set; }
        public static AudioClip[] GameResultMusic { get; private set; }
        public static AudioClip[] VictoryFallGuySFX { get; private set; }
        public static AudioClip[] LoseFallGuySFX { get; private set; }
        public static AudioClip[] WalkFootStepSFX { get; private set; }
        public static AudioClip[] RunFootStepSFX { get; private set; }
        public static AudioClip JumpSFX { get; private set; }
        public static AudioClip DiveSFX { get; private set; }
        public static AudioClip FallSFX { get; private set; }
        public static AudioClip RespawnSFX { get; private set; }
    }
}