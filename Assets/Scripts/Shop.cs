using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents a shop in the game.
public class Shop : MonoBehaviour
{   
    // reference to the coin vfx script, so the coin VFX can be triggered
    public CoinVFX coinVFXScript;

    private void Start()
    {
        // get reference to the coin VFX script
        coinVFXScript = GameObject.Find("Coin VFX").GetComponent<CoinVFX>();
    }

    /// <summary>
    /// This function is called when a purchase of in-game currency is made. 
    /// It updates the player's total currency and plays a visual effect.
    /// </summary>
    /// <param name="amountPurchased">The amount of currency purchased.</param>
    public void BuyCurrency(int amountPurchased)
    {
        if (coinVFXScript.coinVFXPlaying == false)
        {
            // Play a visual effect indicating the addition of new coins, add the coins to the currency total
            coinVFXScript.NewCoinBurst(amountPurchased, 0.02f);
        }
    }
}
