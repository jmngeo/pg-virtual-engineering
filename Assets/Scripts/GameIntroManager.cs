using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameIntroManager : MonoBehaviour
{
    [Header("GameIntroTexts")]
    public List<GameObject> GameIntroTexts = new List<GameObject>();

    public TextMeshProUGUI PageNumText;
    public Button NextMethodButton;
    public Button PreviousMethodButton;


    void Start()
    {
        GlobalVariables.setBackgroundColorForCanvas();
        GlobalVariables.setBackgroundColorForCanvasChild("introBackground");
    }

    public void RoleSelectionScreen()
    {
        SceneManager.LoadScene("RoleSelectionScreen");
    }

    public void OnNextButtonClick()
    {
        for (int i = 0; i < GameIntroTexts.Count; i++)
        {
            if (GameIntroTexts[i].activeSelf)
            {
                GameIntroTexts[i].SetActive(false);
                if (i + 1 < GameIntroTexts.Count)
                {
                    GameIntroTexts[i + 1].SetActive(true);
                    SetPageNumText(i + 2);
                    PreviousMethodButton.gameObject.SetActive(true);
                    PreviousMethodButton.interactable = true;
                }
                else
                {
                    SceneManager.LoadScene("RoleSelectionScreen");
                }
                break;

            }
        }
    }

    public void OnPreviousButtonClick()
    {
        for (int i = 0; i < GameIntroTexts.Count; i++)
        {
            if (GameIntroTexts[i].activeSelf)
            {
                if (i - 1 >= 0)
                {
                    GameIntroTexts[i].SetActive(false);
                    GameIntroTexts[i - 1].SetActive(true);
                    SetPageNumText(i);
                }
                if (i == 1)
                {
                    // Debug.Log("value of i: " + i);
                    PreviousMethodButton.gameObject.SetActive(false);
                    PreviousMethodButton.interactable = false;
                }
                break;
            }
        }
    }

    public void SetPageNumText(int i)
    {
        PageNumText.text = "(" + i.ToString() + "/3)";
    }



}
