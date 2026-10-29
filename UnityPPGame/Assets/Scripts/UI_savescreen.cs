using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_savescreen : MonoBehaviour
{
    private int stage = 0;

    private bool canUseReturn = true;
    private int horizontalnum = 0;

    [Header("Save File Info")]
    public TMP_Text playerNameText;
    public TMP_Text locationText;
    public TMP_Text playtimeText;

    [Header("Menu Objects")]
    public GameObject savemenu;
    public GameObject arrow;

    [Header("Arrow")]
    public Transform arrowtransform;
    public Transform leftpos;
    public Transform rightpos;

    [Header("Text")]
    public GameObject firsttext;
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
        if (stage != 0)
        {
            return;
        }

        horizontalnum = 0;

        UpdateSaveSlotText();

        FreezePlayer();
        SetStage(1);

        canUseReturn = false;
    }

    void Update()
    {
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
        if (stage == 2)
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
            horizontalnum = 0;
            SetStage(2);
        }

        else if (stage == 2)
        {
            if (horizontalnum == 0)
            {
                Save();
                SetStage(3);
            }
            else
            {
                SetStage(1);
            }
        }

        else if (stage == 3)
        {
            CloseSaveScreen();
        }

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
            CloseSaveScreen();
        }
        else if (stage == 2)
        {
            SetStage(1);
        }
        else if (stage == 3)
        {
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

            firsttext.SetActive(false);
            savetext.SetActive(false);
            canceltext.SetActive(false);
            thirdtext.SetActive(false);

            horizontalnum = 0;

            return;
        }

        savemenu.SetActive(true);

        if (stage == 1)
        {

        playerNameText.text = SaveData.playerName;
        int hours = Mathf.FloorToInt(SaveData.playtime / 3600f);
        int minutes = Mathf.FloorToInt((SaveData.playtime % 3600f) / 60f);

        playtimeText.text = hours.ToString("00") + ":" + minutes.ToString("00");
            arrow.SetActive(false);

            firsttext.SetActive(true);

            savetext.SetActive(false);
            canceltext.SetActive(false);
            thirdtext.SetActive(false);
        }

        else if (stage == 2)
        {
            arrow.SetActive(true);

            firsttext.SetActive(false);

            savetext.SetActive(true);
            canceltext.SetActive(true);
            thirdtext.SetActive(false);

            MoveArrowToHorizontalPosition();
        }
        else if (stage == 3)
        {
            arrow.SetActive(false);

            firsttext.SetActive(false);
            savetext.SetActive(false);
            canceltext.SetActive(false);
            thirdtext.SetActive(true);
        }
    }

    void CloseSaveScreen()
    {
        SetStage(0);
        UnfreezePlayer();

        GameObject savepoint = GameObject.Find("savepoint");

        if (savepoint != null)
        {
            savepoint.GetComponent<save>().savescreenon = false;
        }
    }

    void FreezePlayer()
    {
        if (playerRigidbody == null)
        {
            return;
        }

        playerRigidbody.constraints =
            RigidbodyConstraints2D.FreezeAll;
    }

    void UnfreezePlayer()
    {
        if (playerRigidbody == null)
        {
            return;
        }

        playerRigidbody.constraints = originalConstraints;
    }

    void MoveArrowToHorizontalPosition()
    {
        if (arrowtransform == null)
        {
            return;
        }

        Transform selectedPosition =
            horizontalnum == 0 ? leftpos : rightpos;

        if (selectedPosition == null)
        {
            return;
        }

        arrowtransform.position =
            selectedPosition.position;
    }

    void Save()
    {
        if (playerTransform == null)
        {
            Debug.LogError(
                "Player Transform has not been assigned."
            );
            return;
        }

        SaveData.currentScene =
            SceneManager.GetActiveScene().buildIndex;

        SaveData.currentPosition =
            playerTransform.position;

        SaveData.hasSaveFile = true;

        SaveData.SaveAll();

        UpdateSaveSlotText();

        Debug.Log("Saved");
    }

    void UpdateSaveSlotText()
    {
        if (playerNameText != null)
        {
            playerNameText.text =
                SaveData.playerName;
        }

        if (locationText != null)
        {
            locationText.text =
                SaveData.displayedLocation;
        }

        int hours = Mathf.FloorToInt(
            SaveData.playtime / 3600f
        );

        int minutes = Mathf.FloorToInt(
            (SaveData.playtime % 3600f) / 60f
        );

        if (playtimeText != null)
        {
            playtimeText.text =
                hours.ToString("00") +
                ":" +
                minutes.ToString("00");
        }
    }
}