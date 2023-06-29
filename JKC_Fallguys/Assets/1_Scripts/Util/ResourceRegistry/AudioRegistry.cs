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
            LobbyMusic =
                Resources.LoadAll<AudioClip>(DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.Music,
                    PathLiteral.Lobby));
        }
        
        public static AudioClip LoginSFX { get; private set; }
        public static AudioClip[] LobbyMusic { get; private set; }
    }
}