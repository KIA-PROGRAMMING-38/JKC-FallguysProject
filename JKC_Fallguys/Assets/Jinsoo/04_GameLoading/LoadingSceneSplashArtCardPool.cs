using LiteralRepository;
using UnityEngine;
using UnityEngine.Pool;

public class LoadingSceneSplashArtCardPool
{
    public ObjectPool<LoadingSceneSplashArtCard> CardPoolInstance { get; private set; }

    private GameObject _parentObejct;

    public LoadingSceneSplashArtCardPool(GameObject parent)
    {
        _parentObejct = parent;

        CardPoolInstance = new ObjectPool<LoadingSceneSplashArtCard>
        (CreateCard, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            true, 20, 20);
    }

    private LoadingSceneSplashArtCard CreateCard()
    {
        LoadingSceneSplashArtCard card = GameObject.Instantiate(GetPrefab(), _parentObejct.transform);
        card.PoolOwner = this;

        return card;
    }

    private LoadingSceneSplashArtCard GetPrefab()
    {
        return Resources.Load<LoadingSceneSplashArtCard>
        (DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "LoadingSceneSplashArtCard"));
    }
    
    private void ActionOnGet(LoadingSceneSplashArtCard card)
    {
        card.SetActive(true);
    }

    private void ActionOnRelease(LoadingSceneSplashArtCard card)
    {
        card.SetActive(false);
    }

    private void ActionOnDestroy(LoadingSceneSplashArtCard card)
    {
        card.Destroy();
    }
}

