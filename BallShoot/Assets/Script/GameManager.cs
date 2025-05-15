using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("BALL SETTINGS")]
    public GameObject[] Balls;
    public GameObject FirePoint;
    [SerializeField] private float BallForce;
    int ActiveBallIndex;

    [Header("LEVEL SETTINGS")]
    [SerializeField] private int CurrentBallCount;
    [SerializeField] private int BallTargetCount;
    int EnteredBallCount;
    public Slider LevelSlider;
    public TextMeshProUGUI BallsLeft_Text;

    [Header("UI SETTINGS")]
    public GameObject[] Panels;
    public TextMeshProUGUI StarCount;
    public TextMeshProUGUI WinLevelCount;
    public TextMeshProUGUI LoseLevelCount;

    void Start()
    {
        LevelSlider.maxValue = BallTargetCount;
        BallsLeft_Text.text = CurrentBallCount.ToString();
    }

    public void BallEntered()
    {
        EnteredBallCount++;
        LevelSlider.value = EnteredBallCount;


        if (EnteredBallCount == BallTargetCount)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 15);
            StarCount.text = PlayerPrefs.GetInt("Star").ToString();
            WinLevelCount.text = "Level: " + SceneManager.GetActiveScene().name;
            Panels[1].SetActive(true);
        }
        if (CurrentBallCount == 0 && EnteredBallCount != BallTargetCount)
        {
            LoseLevelCount.text = "Level: " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);
        }
        if ((CurrentBallCount + EnteredBallCount) < BallTargetCount)
        {
            LoseLevelCount.text = "Level: " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);
        }

    }

    public void BallMissed()
    {
        if (CurrentBallCount == 0)
        {
            LoseLevelCount.text = "Level: " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);
        }
        if ((CurrentBallCount + EnteredBallCount) < BallTargetCount)
        {
            LoseLevelCount.text = "Level: " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CurrentBallCount--;
            BallsLeft_Text.text = CurrentBallCount.ToString();
            Balls[ActiveBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            Balls[ActiveBallIndex].SetActive(true);
            Balls[ActiveBallIndex].GetComponent<Rigidbody>().AddForce(Balls[ActiveBallIndex].transform.TransformDirection(90, 90, 0) * BallForce, ForceMode.Force);

            if (Balls.Length - 1 == ActiveBallIndex)
                ActiveBallIndex = 0;
            else
                ActiveBallIndex++;
        }
    }

    public void PauseGame()
    {
        Panels[0].SetActive(true);
        Time.timeScale = 0;
    }

    public void PanelButtonAction(string ButtonAction)
    {
        switch (ButtonAction)
        {
            case "Resume":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Settings":
                //Im just coding the game ^_^
                break;
            case "Retry":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
