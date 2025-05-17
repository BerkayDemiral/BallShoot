using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("BALL SETTINGS")]
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private float BallForce;
    int ActiveBallIndex;
    [SerializeField] private Animator _Cannon;
    [SerializeField] private ParticleSystem ShootBallEfect;
    [SerializeField] private ParticleSystem[] BallEfects;
    int ActiveBallEfectIndex;
    [SerializeField] private AudioSource[] BallSounds;
    int ActiveBallSoundIndex;

    [Header("LEVEL SETTINGS")]
    [SerializeField] private int CurrentBallCount;
    [SerializeField] private int BallTargetCount;
    int EnteredBallCount;
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI BallsLeft_Text;

    [Header("UI SETTINGS")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI StarCount;
    [SerializeField] private TextMeshProUGUI WinLevelCount;
    [SerializeField] private TextMeshProUGUI LoseLevelCount;

    [Header("OTHER SETTINGS")]
    [SerializeField] private Renderer TransparentBucket;
    float BucketStartValue;
    float BucketStepValue;
    [SerializeField] private AudioSource[] OtherSounds;

    string LevelName;
    void Start()
    {
        ActiveBallEfectIndex = 0;
        ActiveBallSoundIndex = 0;

        LevelName = SceneManager.GetActiveScene().name;

        BucketStartValue = .5f;
        BucketStepValue = .25f / BallTargetCount;

        LevelSlider.maxValue = BallTargetCount;
        BallsLeft_Text.text = CurrentBallCount.ToString();

    }

    public void BallEntered()
    {
        EnteredBallCount++;
        LevelSlider.value = EnteredBallCount;

        BucketStartValue -= BucketStepValue;
        TransparentBucket.material.SetTextureScale("_MainTex", new Vector2(1f, BucketStartValue));

        BallSounds[ActiveBallSoundIndex].Play();
        ActiveBallSoundIndex++;

        if (ActiveBallSoundIndex == BallSounds.Length - 1)
        {
            ActiveBallSoundIndex = 0;
        }

        if (EnteredBallCount == BallTargetCount)
        {
            Time.timeScale = 0;
            OtherSounds[0].Play();
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 15);
            StarCount.text = PlayerPrefs.GetInt("Star").ToString();
            WinLevelCount.text = "Level: " + LevelName;
            Panels[1].SetActive(true);
        }

        int number = 0;
        foreach (var item in Balls)
        {
            if (item.activeInHierarchy)
            {
                number++;
            }
        }

        if (number == 0)
        {
            if (CurrentBallCount == 0 && EnteredBallCount != BallTargetCount)
            {
                Lose();
            }
            if ((CurrentBallCount + EnteredBallCount) < BallTargetCount)
            {
                Lose();
            }
        }
    }

    public void BallMissed()
    {

        int number = 0;
        foreach (var item in Balls)
        {
            if (item.activeInHierarchy)
            {
                number++;
            }
        }

        if (number ==0)
        {
            if (CurrentBallCount == 0)
            {
                Lose();
            }
            if ((CurrentBallCount + EnteredBallCount) < BallTargetCount)
            {
                Lose();
            }
        }
        
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                CurrentBallCount--;
                BallsLeft_Text.text = CurrentBallCount.ToString();
                _Cannon.Play("Cannon");
                ShootBallEfect.Play();
                OtherSounds[2].Play();
                Balls[ActiveBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
                Balls[ActiveBallIndex].SetActive(true);
                Balls[ActiveBallIndex].GetComponent<Rigidbody>().AddForce(Balls[ActiveBallIndex].transform.TransformDirection(90, 90, 0) * BallForce, ForceMode.Force);

                if (Balls.Length - 1 == ActiveBallIndex)
                    ActiveBallIndex = 0;
                else
                    ActiveBallIndex++;
            }
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

    public void ParcEfect(Vector3 _Position, Color _Color)
    {
        BallEfects[ActiveBallEfectIndex].transform.position = _Position;
        var main = BallEfects[ActiveBallEfectIndex].main;
        main.startColor = _Color;

        BallEfects[ActiveBallEfectIndex].gameObject.SetActive(true);
        ActiveBallEfectIndex++;

        if (ActiveBallEfectIndex == BallEfects.Length - 1)
        {
            ActiveBallEfectIndex = 0;
        }

    }

    void Lose()
    {
        Time.timeScale = 0;
        OtherSounds[1].Play();
        LoseLevelCount.text = "Level: " + LevelName;
        Panels[2].SetActive(true);
    }
}
