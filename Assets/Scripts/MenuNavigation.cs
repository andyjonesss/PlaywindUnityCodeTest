using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class handles the UI navigation between different menu sections.
public class MenuNavigation : MonoBehaviour
{
    // references to different UI sections
    GameObject shopUI;
    GameObject gameUI;
    GameObject settingsUI;
    GameObject otherUI;
    GameObject shapes;

    // references to the images of the main navigation buttons
    Image shopButtonImage;
    Image gameButtonImage;
    Image settingsButtonImage;
    Image otherButtonImage;

    // colors for the selected and unselected navigation buttons
    Color selectedColour = new Color(0.7f, 0.7f, 0.7f);
    Color unselectedColour = new Color(1, 1, 1);

    // initialization method called by Unity when the object starts
    void Start()
    {
        // find and assign the UI sections based on their names
        shopUI = GameObject.Find("Shop UI");
        gameUI = GameObject.Find("Game UI");
        settingsUI = GameObject.Find("Settings UI");
        otherUI = GameObject.Find("Other UI");
        shapes = GameObject.Find("Shapes");

        // find and assign the images of the navigation buttons based on their names
        shopButtonImage = GameObject.Find("Shop button").GetComponent<Image>();
        gameButtonImage = GameObject.Find("Game button").GetComponent<Image>();
        settingsButtonImage = GameObject.Find("Settings button").GetComponent<Image>();
        otherButtonImage = GameObject.Find("Other button").GetComponent<Image>();

        // close all UI at start
        OpenCloseShopUi();
    }

    /// <summary>
    /// Toggle the visibility of the Shop UI, and ensure other sections are closed.
    /// </summary>
    public void OpenCloseShopUi()
    {
        shopUI.SetActive(!shopUI.activeSelf);
        gameUI.SetActive(false);
        settingsUI.SetActive(false);
        otherUI.SetActive(false);
        shapes.SetActive(false);

        // update the colors of the navigation buttons based on which section is active
        SelectedButtonColours();
    }

    /// <summary>
    /// Toggle the visibility of the Game UI, and ensure other sections are closed.
    /// </summary>
    public void OpenCloseGameUi()
    {
        gameUI.SetActive(!gameUI.activeSelf);
        shopUI.SetActive(false);
        settingsUI.SetActive(false);
        otherUI.SetActive(false);
        shapes.SetActive(false);

        // update the colors of the navigation buttons based on which section is active
        SelectedButtonColours();
    }

    /// <summary>
    /// Toggle the visibility of the Settings Menu, and ensure other sections are closed.
    /// </summary>
    public void OpenCloseSettingsMenu()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
        shopUI.SetActive(false);
        gameUI.SetActive(false);
        otherUI.SetActive(false);
        shapes.SetActive(false);

        // update the colors of the navigation buttons based on which section is active
        SelectedButtonColours();
    }

    /// <summary>
    /// Toggle the visibility of the 'other' Menu, and ensure other sections are closed.
    /// </summary>
    public void OpenCloseOtherMenu()
    {
        otherUI.SetActive(!otherUI.activeSelf);
        shapes.SetActive(!shapes.activeSelf);
        shopUI.SetActive(false);
        gameUI.SetActive(false);
        settingsUI.SetActive(false);


        // update the colors of the navigation buttons based on which section is active
        SelectedButtonColours();
    }

    /// <summary>
    /// Update the colors of the navigation buttons based on the active UI section.
    /// </summary>
    void SelectedButtonColours()
    {
        // set the color of the Shop button
        shopButtonImage.color = shopUI.activeSelf ? selectedColour : unselectedColour;

        // set the color of the Game button
        gameButtonImage.color = gameUI.activeSelf ? selectedColour : unselectedColour;

        // set the color of the Settings button
        settingsButtonImage.color = settingsUI.activeSelf ? selectedColour : unselectedColour;

        // set the color of the Settings button
        otherButtonImage.color = otherUI.activeSelf ? selectedColour : unselectedColour;
    }
}
