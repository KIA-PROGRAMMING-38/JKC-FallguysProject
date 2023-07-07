using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using UnityEngine;
using UnityEngine.Audio;

namespace ResourceRegistry
{
    public static class AudioRegistry
    {
        static AudioRegistry()
        {
            SetAudioClips();
        }

        private static void SetAudioClips()
        {
            GameAudioMixer = Resources.Load<AudioMixer>(DataManager.SetDataPath(
                PathLiteral.Sounds, "AudioMixer"));
            LoginSFX = DataManager.GetAudioClip(PathLiteral.Sounds, PathLiteral.Music, PathLiteral.LoginSound);
            LobbyMusic = Resources.LoadAll<AudioClip>(DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Lobby));
            RoundMusic = Resources.LoadAll<AudioClip>(DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Stage, PathLiteral.RoundMusic));
            
            FinalRoundMusic = Resources.LoadAll<AudioClip>(DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Stage, PathLiteral.FinalRoundMusic));
            
            FallGuySFXOnRoundResult = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.RoundResult));

            
            GameResultMusic = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Result ) );
            

            VictoryFallGuySFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.GameResult, PathLiteral.Victory ) );

            LoseFallGuySFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.GameResult, PathLiteral.Lose ) );

            WalkFootStepSFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Walk));
            

          
            RunFootStepSFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Run ) );



            JumpSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Jump, "F_Footsteps_Jump_Soft_01" ) );
            DiveSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Dive, "F_Footsteps_Dive_01"));
            FallSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Fall, "SFX_ElimBoard_CartoonHit"));
            RespawnSFX = Resources.Load<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, "SFX_Respawn_3D"));
        }

        public static AudioMixer GameAudioMixer { get; private set; }
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