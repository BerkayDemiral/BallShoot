using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CylinderController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool ButtonPressed;
    public GameObject CylinderObject;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private string Way;

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;
    }

    void Update()
    {
        if (ButtonPressed)
        {
            if (Way == "Left")
            {
                CylinderObject.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0, Space.Self);
            }
            else
            {
                CylinderObject.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0, Space.Self);
            }
        }
    }
}
