using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PhaseIntroductionManager : MonoBehaviour
{
    [Header("GameIntroTexts")]
    public GameObject Methods;
    public TextMeshProUGUI clickToZoomText;

    public Button NextMethodButton;
    public Button PreviousMethodButton;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.setBackgroundColorForCanvas();
        GlobalVariables.setBackgroundColorForCanvasChild("introBackground");

        // Methods.transform.GetChild(0).gameObject.SetActive(true);
        // PreviousMethodButton.gameObject.SetActive(false);
    }

    public void GoToInvestmentsScreen()
    {

        SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "Investments");

    }
    public void GoToMethodsIntroScreen()
    {
        SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "methodsIntroduction");
    }

    public void GotoPhaseIntroductionScreen()
    {
        SceneManager.LoadScene("phase" + GlobalVariables.PhaseCounter + "-" + "Introduction");
    }

    public void OnNextButtonClick()
    {
        //get all child gameobjects of Methods gameobject
        for (int i = 0; i < Methods.transform.childCount; i++)
        {
            GameObject method = Methods.transform.GetChild(i).gameObject;

            if (method.activeSelf)
            {
                if (i == Methods.transform.childCount - 1)
                {
                    GoToInvestmentsScreen();
                }
                else
                {
                    method.SetActive(false);
                    Methods.transform.GetChild(i + 1).gameObject.SetActive(true);
                    PreviousMethodButton.gameObject.SetActive(true);
                }
                break;
            }
        }
    }

    public void OnPreviousButtonClick()
    {
        //get all child gameobjects of Methods gameobject


        for (int i = 0; i < Methods.transform.childCount; i++)
        {
            GameObject method = Methods.transform.GetChild(i).gameObject;

            if (method.activeSelf)
            {
                if (i == 0)
                {
                    GotoPhaseIntroductionScreen();

                }
                else if (i == 1)
                {
                    method.SetActive(false);
                    Methods.transform.GetChild(i - 1).gameObject.SetActive(true);
                    // PreviousMethodButton.gameObject.SetActive(false);
                }
                else
                {
                    method.SetActive(false);
                    Methods.transform.GetChild(i - 1).gameObject.SetActive(true);
                }
                break;
            }
        }
    }

    public void OnEnlarge()
    {
        PreviousMethodButton.gameObject.SetActive(false);
        NextMethodButton.gameObject.SetActive(false);
        clickToZoomText.gameObject.SetActive(false);
    }

    public void OnCloseZoom()
    {

        PreviousMethodButton.gameObject.SetActive(true);
        NextMethodButton.gameObject.SetActive(true);
        clickToZoomText.gameObject.SetActive(true);
    }



}
