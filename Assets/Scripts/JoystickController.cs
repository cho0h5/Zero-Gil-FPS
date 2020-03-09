using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    public Transform Stick;

    Vector3 InitialPosition;
    public Vector3 JoystickDirection;
    float Radius;

    public bool mouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        Radius = 40;
        InitialPosition = Stick.transform.position;
        Debug.Log($"Joystick Direction {JoystickDirection}");
    }

    public void Drag(BaseEventData _Data)
    {
        mouseDown = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;
        JoystickDirection = (Pos - InitialPosition).normalized;
        float Distance = Vector3.Distance(Pos, InitialPosition);
        if (Distance < Radius) Stick.position = InitialPosition + JoystickDirection * Distance;
        else Stick.position = InitialPosition + JoystickDirection * Radius;
        //Debug.Log($"Joystick Direction {JoystickDirection}");
    }

    public void EndDrag()
    {
        mouseDown = false;
        Stick.position = InitialPosition;
        JoystickDirection = Vector3.zero;
    }
}
