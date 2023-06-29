using LiteralRepository;
using UnityEngine;

namespace ResourceRegistry
{
    public static class AudioRegistry
    {
        static AudioRegistry()
        {
            SetAudioClips();
            
            // AudioManager 접근
            AudioManager.Instance.lobbyMusic = Resources.LoadAll<AudioClip>(
                DataManager.SetDataPath(PathLiteral.Sounds, PathLiteral.Music, PathLiteral.Lobby));
        }

        private static void SetAudioClips()
        {
            LoginSound = DataManager.GetAudioClip(PathLiteral.Sounds, PathLiteral.Music, PathLiteral.LoginSound);
        }
        
        public static AudioClip LoginSound { get; private set; }
    }
}