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
            LoginSound = DataManager.GetAudioClip(PathLiteral.Sounds, PathLiteral.Music, PathLiteral.LoginSound);
        }
        
        public static AudioClip LoginSound { get; private set; }
    }
}