using System;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int TextureIndex;
    public int Score;

    public PlayerData(string playerName, int textureIndex, int score)
    {
        PlayerName = playerName;
        TextureIndex = textureIndex;
        Score = score;
    }
}
