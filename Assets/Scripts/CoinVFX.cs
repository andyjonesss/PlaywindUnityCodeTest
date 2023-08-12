using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// This class manages the visual effects related to coins
public class CoinVFX : MonoBehaviour
{
    // refernece to the currency bar script, to update the currency figure
    public CurrencyBar currencyBarScript;
    // reference to the coin Visual Effect
    VisualEffect CoinVFXSystem;
    // flag for whether the VFX is playing
    public bool coinVFXPlaying;

    private void Start()
    {
        CoinVFXSystem = GetComponent<VisualEffect>();
        currencyBarScript = GameObject.Find("Currency bar").GetComponent<CurrencyBar>();
    }

    /// <summary>
    /// starts the coin burst coroutine, sets a flag as to whether the VFX is currently playing
    /// </summary>
    public void NewCoinBurst(int coinCount, float delayInSeconds)
    {
        // set the flag
        coinVFXPlaying = true;

        // start the coroutine to handle the bursts
        StartCoroutine(CoinBurstCoroutine(coinCount, delayInSeconds));        
    }

    /// <summary>
    /// Starts a new burst of coins at the mouse current position.
    /// Adds the coins to the currency total figure.
    /// </summary>
    /// <param name="coinCount">The number of coins to include in the burst.</param>
    /// <param name="delayInSeconds">The delay between each new coin spawning.</param>
    /// <returns></returns>
    private IEnumerator CoinBurstCoroutine(int coinCount, float delayInSeconds)
    {
        // convert the current mouse position from screen space to world space
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // adjust the Z position for the visual effect
        spawnPos.z = 100;
        // set the spawn position for the visual effect using the calculated position
        CoinVFXSystem.SetVector3("SpawnPos", spawnPos);

        for (int i = 0; i < coinCount; i++)
        {
            // trigger the CoinBurst event in the visual effect system
            CoinVFXSystem.SendEvent("CoinBurst");
            currencyBarScript.AddOrDeductMoney(1);

            float timer = 0f;
            while (timer < delayInSeconds)
            {
                timer += Time.deltaTime;  // increment the timer by the time since the last frame
                yield return null;  // wait for the next frame
            }
        }
        coinVFXPlaying = false;
    }
}
