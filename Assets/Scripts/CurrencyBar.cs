using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

// this class manages the currency displayed on the UI, as well as saving and loading its value
public class CurrencyBar : MonoBehaviour
{
    int currentMoney;
    TextMeshProUGUI currencyText;
    private string saveFilePath;

    private void Awake()
    {
        // determine the path to save the currency data using the app's persistent data path
        saveFilePath = Path.Combine(Application.persistentDataPath, "currencySave.json");
        // load the saved currency value from the file
        LoadCurrency();
    }

    void Start()
    {
        // get the reference to the TextMeshPro component
        currencyText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        // display the current currency value
        DisplayCurrency();
    }

    /// <summary>
    /// Add or deducts money by the amount given and dipslays the updated currency value in the currency bar
    /// </summary>
    /// <param name="amount">The amount to change the currency by.</param>
    public void AddOrDeductMoney(int amount)
    {
        // update the current money value
        currentMoney += amount;
        // save the updated money value to the file
        SaveCurrency();
        // display the updated currency value
        DisplayCurrency();
    }

    /// <summary>
    /// Helper method to update the displayed currency value
    /// </summary>
    private void DisplayCurrency()
    {
        // Convert the current money value to a string format
        string newCurrencyAmount = "£" + currentMoney.ToString();
        // Update the TextMeshPro component to display the new value
        currencyText.text = newCurrencyAmount;
    }
        
    /// <summary>
    /// Save the current money value to a file
    /// </summary>
    private void SaveCurrency()
    {
        // convert the money value to a JSON string format.
        string dataAsJson = JsonUtility.ToJson(new CurrencyData(currentMoney));
        // write the JSON string to a file.
        File.WriteAllText(saveFilePath, dataAsJson);
    }

    /// <summary>
    /// Load the saved money value from a file
    /// </summary>
    private void LoadCurrency()
    {
        // check if the save file exists
        if (File.Exists(saveFilePath))
        {
            // read the JSON string from the file
            string dataAsJson = File.ReadAllText(saveFilePath);
            // convert the JSON string back to a CurrencyData object
            CurrencyData loadedData = JsonUtility.FromJson<CurrencyData>(dataAsJson);
            // assign the loaded money value to currentMoney
            currentMoney = loadedData.money;
        }
    }

    // nested class to hold the money data for saving and loading
    [System.Serializable]
    private class CurrencyData
    {
        public int money;

        public CurrencyData(int money)
        {
            this.money = money;
        }
    }
}
