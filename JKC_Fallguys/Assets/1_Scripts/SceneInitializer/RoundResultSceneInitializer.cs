using System.IO;
using LiteralRepository;
using Photon.Pun;
using ResourceRegistry;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        AudioManager.Instance.Clear();
        AudioManager.Instance.Play(SoundType.MusicIntro, AudioRegistry.RoundResultMusic, 0.3f);
        AudioManager.Instance.Play(SoundType.SFX, AudioRegistry.IncreaseNumbersSFX, 0.5f);
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.RoundResult, "Platform"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.RoundResult, "ResultRoundSetupManager"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.RoundResult, "BackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.RoundResult, "RoundResultViewController"));
        
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                Path.Combine(PathLiteral.Prefabs, PathLiteral.UI, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
