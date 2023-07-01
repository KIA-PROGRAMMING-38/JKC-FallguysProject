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
        }
        
        public static AudioClip LoginSFX { get; private set; }
        public static AudioClip[] LobbyMusic { get; private set; }
        public static AudioClip[] RoundMusic { get; private set; } 
        public static AudioClip[] FinalRoundMusic { get; private set; }
    }
}