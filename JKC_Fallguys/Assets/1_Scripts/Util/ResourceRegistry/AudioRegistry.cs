using LiteralRepository;
using UnityEngine;

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
        }
        
        public static AudioClip LoginSFX { get; private set; }
        public static AudioClip[] LobbyMusic { get; private set; }
        public static AudioClip[] RoundMusic { get; private set; } 
        public static AudioClip[] FinalRoundMusic { get; private set; }
        public static AudioClip[] FallGuySFXOnRoundResult { get; private set; }
        public static AudioClip[] GameResultMusic { get; private set; }
    }
}