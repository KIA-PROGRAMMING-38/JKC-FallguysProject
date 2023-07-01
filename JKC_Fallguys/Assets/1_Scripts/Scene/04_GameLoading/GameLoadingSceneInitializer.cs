using LiteralRepository;
using Photon.Pun;

public class GameLoadingSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "GameLoadingSceneManager"));

        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "MapSelectionManager");
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }

    protected override void InitializeModel()
    {
        Model.GameLoadingSceneModel.SetStatusLoadingSceneUI(true);
    }
}
