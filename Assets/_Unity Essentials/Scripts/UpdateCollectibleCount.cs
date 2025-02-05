using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText; // Reference to the TextMeshProUGUI component
    public ParkCodeInputField parkCodeInputField; // Reference to the TMP_input on the ParkCodeInputField script
    public TMP_Text congratulations; // Reference to the TMP_Text Congratulations
    private int congrats = 0;
    private string congratsMessage = "";
    private bool gameFinished = false;

    // Reference to the LevelCompleted script
    public LevelCompleted bedroomCompleted;
    public LevelCompleted kitchenCompleted;
    public LevelCompleted roomCompleted;
    public LevelCompleted room2Completed;
    public LevelCompleted parkCompleted;

    // Cached types and total counts
    private Type collectible2DType;
    private Type collectibleBedroomType;
    private Type collectibleKitchenType;
    private Type collectibleRoomType;

    void Start()
    {
        // Cache the TextMeshProUGUI component
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("UpdateCollectibleCount script requires a TextMeshProUGUI component on the same GameObject.");
            return;
        }

        // Hide congratulations text
        HideCongratulationsText();

        // Validate LevelCompleted script references
        ValidateLevelCompletedReferences();

        // Cache the types once
        CacheCollectibleTypes();

        // Initial update on start
        UpdateCollectibleDisplay();
    }

    private void HideCongratulationsText()
    {
        congratulations.gameObject.SetActive(false);
        congratulations.transform.parent.gameObject.SetActive(false);
    }

    private void ValidateLevelCompletedReferences()
    {
        if (bedroomCompleted == null)
            Debug.LogError("Bedroom LevelCompleted script reference is not assigned.");
        if (kitchenCompleted == null)
            Debug.LogError("Kitchen LevelCompleted script reference is not assigned.");
        if (roomCompleted == null)
            Debug.LogError("Room LevelCompleted script reference is not assigned.");
        if (room2Completed == null)
            Debug.LogError("Room LevelCompleted script reference is not assigned.");
        if (parkCompleted == null)
            Debug.LogError("Park LevelCompleted script reference is not assigned.");
    }

    private void CacheCollectibleTypes()
    {
        collectible2DType = Type.GetType("Collectible2D");
        collectibleBedroomType = Type.GetType("CollectibleBedroom");
        collectibleKitchenType = Type.GetType("CollectibleKitchen");
        collectibleRoomType = Type.GetType("CollectibleRoom");
    }

    void Update()
    {
        // Here you might want to add a condition or event to call UpdateCollectibleDisplay, instead of calling it every frame
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        int totalBedroomCollectibles = 0;
        int totalCollectibles = 0;
        int totalKitchenCollectibles = 0;
        int totalRoomCollectibles = 0;

        if (collectible2DType != null)
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;

        if (collectibleBedroomType != null)
            totalBedroomCollectibles += UnityEngine.Object.FindObjectsByType(collectibleBedroomType, FindObjectsSortMode.None).Length;

        if (collectibleKitchenType != null)
            totalKitchenCollectibles += UnityEngine.Object.FindObjectsByType(collectibleKitchenType, FindObjectsSortMode.None).Length;

        if (collectibleRoomType != null)
            totalRoomCollectibles += UnityEngine.Object.FindObjectsByType(collectibleRoomType, FindObjectsSortMode.None).Length;

        HandleCollectibleCounts(totalBedroomCollectibles, totalKitchenCollectibles, totalRoomCollectibles);
    }

    private void HandleCollectibleCounts(int totalBedroomCollectibles, int totalKitchenCollectibles, int totalRoomCollectibles)
    {
        if (totalBedroomCollectibles == 0)
        {
            HandleBedroomCompletion();

            if (totalKitchenCollectibles == 0)
            {
                HandleKitchenCompletion();

                if (totalRoomCollectibles == 0)
                {
                    HandleRoomCompletion();
                }
                else
                {
                    collectibleText.text = $"Coins remaining: {totalRoomCollectibles}";
                }
            }
            else
            {
                collectibleText.text = $"Fish remaining: {totalKitchenCollectibles}";
            }
        }
        else
        {
            collectibleText.text = $"Diamonds remaining: {totalBedroomCollectibles}";
        }
    }

    private void HandleBedroomCompletion()
    {
        if (bedroomCompleted != null)
        {
            bedroomCompleted.OpenDoor();
            if (congrats == 0)
            {
                congrats = 1;
                congratsMessage = "BEDROOM COMPLETED";
                StartCoroutine(CongratulationsMessage(congratsMessage, false));
            }
        }
        else
        {
            Debug.LogError("Bedroom LevelCompleted script reference is null.");
        }
    }

    private void HandleKitchenCompletion()
    {
        if (kitchenCompleted != null)
        {
            kitchenCompleted.OpenDoor();
            if (congrats == 1)
            {
                congrats = 2;
                congratsMessage = "KITCHEN COMPLETED";
                StartCoroutine(CongratulationsMessage(congratsMessage, false));
            }
        }
        else
        {
            Debug.LogError("Kitchen LevelCompleted script reference is null.");
        }
    }

    private void HandleRoomCompletion()
    {
        if (roomCompleted != null && room2Completed != null)
        {
            roomCompleted.OpenDoor();
            room2Completed.OpenDoor();
            if (congrats == 2)
            {
                congratsMessage = "LIVING ROOM COMPLETED";
                StartCoroutine(CongratulationsMessage(congratsMessage, true));
                //HideCollectibleText();
            }

            ShowParkCodeInputField();
        }
        else
        {
            Debug.LogError("Room LevelCompleted script reference is null.");
        }

        /*if (congrats == 3)
        {
            collectibleText.gameObject.SetActive(false);
            collectibleText.transform.parent.gameObject.SetActive(false);
        }*/
    }

    /*private void HideCollectibleText()
    {
        collectibleText.canvasRenderer.SetAlpha(0);
        foreach (CanvasRenderer renderer in collectibleText.transform.parent.GetComponentsInChildren<CanvasRenderer>())
        {
            renderer.SetAlpha(0);
        }
    }*/

    private void ShowParkCodeInputField()
    {
        if (parkCodeInputField != null && !parkCodeInputField.gameOver)
        {
            parkCodeInputField.ShowInputFieldAndImage();
            collectibleText.text = "Find the secret code outside";
        }
        else
        {
            if (gameFinished == false)
            {
                StartCoroutine(HideParkInputField(gameFinished));
            }           
        }
    }

    private IEnumerator CongratulationsMessage(string congratsMessage, bool lastTime)
    {
        //Debug.Log("coroutine started and the congrats should be displayed");

        congratulations.text = congratsMessage;
        congratulations.gameObject.SetActive(true);
        congratulations.transform.parent.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        //Debug.Log("coroutine ended and the congrats shouldn't be displayed");

        congratulations.gameObject.SetActive(false);
        congratulations.transform.parent.gameObject.SetActive(false);
        if (lastTime)
        {
            congrats = 3;
        }
    }

    private IEnumerator HideParkInputField(bool gameFinished)
    {
        gameFinished = true;

        collectibleText.canvasRenderer.SetAlpha(0);
        foreach (CanvasRenderer renderer in collectibleText.transform.parent.GetComponentsInChildren<CanvasRenderer>())
        {
            renderer.SetAlpha(0);
        }

        yield return new WaitForSeconds(2f);

        parkCodeInputField.HideInputFieldAndImage();

        collectibleText.gameObject.SetActive(false);
        collectibleText.transform.parent.gameObject.SetActive(false);
        parkCompleted.gameObject.SetActive(false);
    }
}
