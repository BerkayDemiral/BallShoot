using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Balls;
    public GameObject FirePoint;
    public float BallForce;
    int ActiveBallIndex;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
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
