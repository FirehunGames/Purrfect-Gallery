using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainMenu;

    public Button defaultButton;
    public Slider sliderToSelect;

    private EventSystem eventSystem;

    public GameObject difficulty;

    public Button easyButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventSystem = EventSystem.current;

        // Preselect the default button
        if (eventSystem != null && defaultButton != null)
        {
            eventSystem.SetSelectedGameObject(defaultButton.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
            mainMenu.SetActive(true);
            eventSystem.SetSelectedGameObject(defaultButton.gameObject);
        }

        if (optionsPanel.activeSelf)
        {
            // If optionsPanel is active, preselect the slider
            if (eventSystem.currentSelectedGameObject != sliderToSelect.gameObject)
            {
                eventSystem.SetSelectedGameObject(sliderToSelect.gameObject);
            }
        }
        else
        {
            // Ensure the default button remains selected if no other UI element is selected
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(defaultButton.gameObject);
            }

        }
        if(difficulty != null && easyButton != null)
        {
            if (difficulty.activeSelf && Input.GetButton("Fire2") || difficulty.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                difficulty.SetActive(false);
                eventSystem.SetSelectedGameObject(defaultButton.gameObject);
            }
        }
        
    }

    public void NewGameOnClick()
    {
        difficulty.SetActive(true);
        eventSystem.SetSelectedGameObject(easyButton.gameObject);
    }

}
