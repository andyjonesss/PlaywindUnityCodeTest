using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This class represents the main game mechanics and interactions.
public class Game : MonoBehaviour
{
    // reference to display a random amount in the game's UI
    TextMeshProUGUI randomAmountText;
    // reference to the CurrencyBar script to interact with the player's currency
    public CurrencyBar currencyBarScript;

    // Initialization method called by Unity when the object starts.
    void Start()
    {
        // find and assign the TextMeshProUGUI component located at the second child's first child of this object
        randomAmountText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Initiates the game's primary action. It generates a random cost, displays it, and then deducts this amount from the player's total currency.
    /// </summary>
    public void PlayGame()
    {
        // generate a random amount between 0 and 50 inclusive
        int randomAmount = Random.Range(0, 51);
        // display the generated random amount on the UI
        randomAmountText.text = "£" + randomAmount.ToString();
        // deduct the random amount from the player's currency total
        currencyBarScript.AddOrDeductMoney(-randomAmount);
    }
}
