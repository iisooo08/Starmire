using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_savescreen : MonoBehaviour
{
    // 0: save screen closed
    // 1: vertical save-slot selection
    // 2: horizontal Save/Cancel selection
    // 3: final "saved" text
    private int stage = 0;

    private bool canUseReturn = true;

    private int verticalnum = 0;
    private int horizontalnum = 0;

    [Header("Menu Objects")]
    public GameObject savemenu;
    public GameObject arrow;

    [Header("Arrow")]
    public Transform arrowtransform;
    public List<Transform> verticalpos;
    public Transform leftpos;
    public Transform rightpos;

    [Header("Text")]
    public List<GameObject> firsttexts;
    public GameObject savetext;
    public GameObject canceltext;
    public GameObject thirdtext;

    [Header("Player")]
    public GameObject player;
    public Transform playerTransform;

    private Rigidbody2D playerRigidbody;
    private RigidbodyConstraints2D originalConstraints;

    void Awake()
    {
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();

            if (playerTransform == null)
            {
                playerTransform = player.transform;
            }
        }

        if (playerRigidbody != null)
        {
            originalConstraints = playerRigidbody.constraints;
        }

        SetStage(0);
    }

    public void SaveScreenFunct()
    {
        // Prevent the menu from reopening while already open.
        if (stage != 0)
        {
            return;
        }

        verticalnum = 0;
        horizontalnum = 0;

        FreezePlayer();
        SetStage(1);

        // Prevent the Return press that opened the menu
        // from immediately selecting the first option.
        canUseReturn = false;
    }

    void Update()
    {
        // Allow Return again only after it has been released.
        if (!canUseReturn && Input.GetKeyUp(KeyCode.Return))
        {
            canUseReturn = true;
        }

        if (stage == 0)
        {
            return;
        }

        HandleMovement();
        HandleEscape();

        if (canUseReturn)
        {
            HandleReturn();
        }
    }

    void HandleMovement()
    {
        // Stage 1: move between save slots vertically.
        if (stage == 1)
        {
            if (
                Input.GetKeyDown(KeyCode.UpArrow) &&
                verticalnum > 0
            )
            {
                verticalnum--;
                MoveArrowToVerticalPosition();
            }
            else if (
                Input.GetKeyDown(KeyCode.DownArrow) &&
                verticalnum < verticalpos.Count - 1
            )
            {
                verticalnum++;
                MoveArrowToVerticalPosition();
            }
        }

        // Stage 2: choose Save or Cancel horizontally.
        else if (stage == 2)
        {
            if (
                Input.GetKeyDown(KeyCode.RightArrow) &&
                horizontalnum == 0
            )
            {
                horizontalnum = 1;
                MoveArrowToHorizontalPosition();
            }
            else if (
                Input.GetKeyDown(KeyCode.LeftArrow) &&
                horizontalnum == 1
            )
            {
                horizontalnum = 0;
                MoveArrowToHorizontalPosition();
            }
        }
    }

    void HandleReturn()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
        {
            return;
        }

        if (stage == 1)
        {
            // A save slot was selected.
            horizontalnum = 0;
            SetStage(2);
        }
        else if (stage == 2)
        {
            if (horizontalnum == 0)
            {
                // Left option: Save.
                Save();
                SetStage(3);
            }
            else
            {
                // Right option: Cancel.
                SetStage(1);
            }
        }
        else if (stage == 3)
        {
            // Close the menu after the final saved message.
            CloseSaveScreen();
        }

        // Require Return to be released before another action.
        canUseReturn = false;
    }

    void HandleEscape()
    {
        if (!Input.GetKeyDown(KeyCode.RightShift))
        {
            return;
        }

        if (stage == 1)
        {
            // Exit the save screen.
            CloseSaveScreen();
        }
        else if (stage == 2)
        {
            // Return to save-slot selection.
            SetStage(1);
        }
        else if (stage == 3)
        {
            // Return to Save/Cancel selection.
            SetStage(2);
        }
    }

    void SetStage(int newStage)
    {
        stage = newStage;

        if (stage == 0)
        {
            savemenu.SetActive(false);
            arrow.SetActive(false);

            SetAllFirstTexts(false);
            savetext.SetActive(false);
            canceltext.SetActive(false);
            thirdtext.SetActive(false);

            verticalnum = 0;
            horizontalnum = 0;

            return;
        }

        savemenu.SetActive(true);

        if (stage == 1)
        {
            arrow.SetActive(true);

            SetAllFirstTexts(true);

            savetext.SetActive(false);
            canceltext.SetActive(false);
            thirdtext.SetActive(false);

            MoveArrowToVerticalPosition();
        }
        else if (stage == 2)
        {
            arrow.SetActive(true);

            SetAllFirstTexts(true);

            // Replace the selected row with Save/Cancel text.
            firsttexts[verticalnum].SetActive(false);

            savetext.SetActive(true);
            canceltext.SetActive(true);
            thirdtext.SetActive(false);

            MoveObjectToSelectedRow(savetext);
            MoveObjectToSelectedRow(canceltext);

            MoveArrowToHorizontalPosition();
        }
        else if (stage == 3)
        {
            arrow.SetActive(false);

            SetAllFirstTexts(true);

            // Replace the selected row with the final text.
            firsttexts[verticalnum].SetActive(false);

            savetext.SetActive(false);
            canceltext.SetActive(false);
            thirdtext.SetActive(true);

            MoveObjectToSelectedRow(thirdtext);
        }
    }

    void CloseSaveScreen()
    {
        SetStage(0);
        UnfreezePlayer();
    }

    void FreezePlayer()
    {
        if (playerRigidbody == null)
        {
            return;
        }
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void UnfreezePlayer()
    {
        if (playerRigidbody == null)
        {
            return;
        }

        playerRigidbody.constraints = originalConstraints;
    }

    void SetAllFirstTexts(bool active)
    {
        foreach (GameObject textObject in firsttexts)
        {
            if (textObject != null)
            {
                textObject.SetActive(active);
            }
        }
    }

    void MoveArrowToVerticalPosition()
    {
        if (
            arrowtransform == null ||
            verticalpos.Count == 0 ||
            verticalnum >= verticalpos.Count
        )
        {
            return;
        }

        arrowtransform.position = new Vector3(
            verticalpos[verticalnum].position.x,
            verticalpos[verticalnum].position.y,
            arrowtransform.position.z
        );
    }

    void MoveArrowToHorizontalPosition()
    {
        if (
            arrowtransform == null ||
            verticalpos.Count == 0 ||
            verticalnum >= verticalpos.Count
        )
        {
            return;
        }

        Transform selectedPosition =
            horizontalnum == 0 ? leftpos : rightpos;

        arrowtransform.position = new Vector3(
            selectedPosition.position.x,
            verticalpos[verticalnum].position.y,
            arrowtransform.position.z
        );
    }

    void MoveObjectToSelectedRow(GameObject objectToMove)
    {
        if (
            objectToMove == null ||
            verticalpos.Count == 0 ||
            verticalnum >= verticalpos.Count
        )
        {
            return;
        }

        objectToMove.transform.position = new Vector3(
            objectToMove.transform.position.x,
            verticalpos[verticalnum].position.y,
            objectToMove.transform.position.z
        );
    }

    void Save()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform has not been assigned.");
            return;
        }

        SaveData.currentScene =
            SceneManager.GetActiveScene().buildIndex;

        SaveData.currentPosition = playerTransform.position;
        SaveData.hasSaveFile = true;

        SaveData.SaveAll();

        Debug.Log("Saved");
    }
}