using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.Log("Kazandin");
        }
        if (CurrentBallCount == 0 && EnteredBallCount != BallTargetCount)
        {
            Debug.Log("Kaybettin");
        }
        if ((CurrentBallCount + EnteredBallCount) < BallTargetCount)
        {
            Debug.Log("Kaybettin");
        }

    }

    public void BallMissed()
    {
        if (CurrentBallCount == 0)
        {
            Debug.Log("Kaybettin");
        }
        if ((CurrentBallCount + EnteredBallCount) < BallTargetCount)
        {
            Debug.Log("Kaybettin");
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
}
