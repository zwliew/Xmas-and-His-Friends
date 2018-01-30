using UnityEngine;

/**
 * This is the global data controller for the entire game.
 * This must be attached to a scene (called Persistent) that
 * will be persisted via DontDestroyOnLoad().
 */
public class PlayerDataController : MonoBehaviour
{
    private PlayerData playerData;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadPlayerData();
    }

    public void UpdatePlayerName(string name)
    {
        playerData.name = name;
        SavePlayerData();
    }

    /**
     * Updates the number of coins in the player's inventory
     * given a parameter 'delta'.
     * 
     * If the resulting number of coins is negative, this method
     * returns _false_ and does _not_ update the inventory.
     * Otherwise, it returns _true_.
     */
    public bool UpdatePlayerCoins(int delta)
    {
        int initial = playerData.coins;
        int final = initial + delta;
        if (final < 0)
        {
            return false;
        }
        playerData.coins = final;
        SavePlayerData();
        return true;
    }

    private void LoadPlayerData()
    {

        if (PlayerPrefs.HasKey("name"))
        {
            playerData.name = PlayerPrefs.GetString("name");
        } else
        {
            playerData.name = null;
        }

        if (PlayerPrefs.HasKey("coins"))
        {
            playerData.coins = PlayerPrefs.GetInt("coins");
        } else
        {
            playerData.coins = 0;
        }
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetInt("coins", playerData.coins);
        PlayerPrefs.SetString("name", playerData.name);
    }
}
