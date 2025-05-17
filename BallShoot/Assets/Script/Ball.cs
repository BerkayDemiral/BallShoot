using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager _GameManager;
    Rigidbody rb;
    Renderer _color;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _color = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Bucket"))
        {
            TechinalOperation();
            _GameManager.BallEntered();
        }
        else if (other.CompareTag("LowerObject"))
        {

            TechinalOperation();
            _GameManager.BallMissed();
        }

        void TechinalOperation()
        {
            _GameManager.ParcEfect(gameObject.transform.position, _color.material.color);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }

    }
}