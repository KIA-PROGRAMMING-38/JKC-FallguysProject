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
        }
        
        public static AudioClip LoginSFX { get; private set; }
        public static AudioClip[] LobbyMusic { get; private set; }
    }
}