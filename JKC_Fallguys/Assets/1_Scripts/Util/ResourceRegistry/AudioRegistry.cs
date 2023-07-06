using System;
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
            LoginSFX = DataManager.GetAudioClip(PathLiteral.Sounds, PathLiteral.Music, PathLiteral.LoginSound);
            LobbyMusic = Resources.LoadAll<AudioClip>(DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Lobby));
            RoundMusic = Resources.LoadAll<AudioClip>(DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Stage, PathLiteral.RoundMusic));
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            FinalRoundMusic = Resources.LoadAll<AudioClip>(DataManager.SetDataPath(
                PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Stage, PathLiteral.FinalRoundMusic));
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            FallGuySFXOnRoundResult = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.RoundResult));
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            GameResultMusic = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Result ) );
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            VictoryFallGuySFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.GameResult, PathLiteral.Victory ) );
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            LoseFallGuySFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.GameResult, PathLiteral.Lose ) );
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            WalkFootStepSFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Walk));
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            RunFootStepSFX = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.SFX, PathLiteral.Player, PathLiteral.Run));
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
    }
}