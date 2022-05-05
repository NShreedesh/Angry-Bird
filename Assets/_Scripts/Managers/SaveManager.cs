using UnityEngine;

public static class SaveManager
{
    public static readonly string LevelKey = "Level";

    public static void Save(int level)
    {
        PlayerPrefs.SetInt("Level", level);
    }

    public static int Load()
    {
        if (PlayerPrefs.HasKey(LevelKey))
        {
            return PlayerPrefs.GetInt(LevelKey);
        }
        return 1;
    }
}
