using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoruInput : MonoBehaviour
{

    public Joystick joystick;
    public LoruInputAButton aButton;
    public float joystickRawThreshold = 0.3f;


    // Update is called once per frame

    public static bool GetButton(string button)
    {
        if (button == "Jump")
        {
            bool btn = Input.GetButton("Jump") || instance.aButton.pressed; // check 'pressed'

            return btn;
        }
        return false;

    }
    public static bool GetButtonUp(string button)
    {
        if (button == "Jump")
        {
            bool btn = Input.GetButtonUp("Jump") || instance.aButton.released; // check 'released'

            return btn;
        }
        return false;
    }
    public static float GetAxis(string axis)
    {
        if (axis == "Horizontal")
        {
            float x = Input.GetAxis("Horizontal");
            x += instance.joystick.Horizontal;
            return x;
        }
        else if (axis == "Vertical")
        {
            float y = Input.GetAxis("Vertical");
            y += instance.joystick.Vertical;
            return y;
        }
        return 0;
    }

    public static float GetAxisRaw(string axis)
    {
        if (axis == "Horizontal")
        {
            float x = Input.GetAxisRaw("Horizontal");
            if (instance.joystick.Horizontal > instance.joystickRawThreshold) x = 1;
            if (instance.joystick.Horizontal < -instance.joystickRawThreshold) x = -1;
            return x;
        }
        else if (axis == "Vertical")
        {
            float y = Input.GetAxisRaw("Vertical");
            if (instance.joystick.Vertical > instance.joystickRawThreshold) y = 1;
            if (instance.joystick.Vertical < -instance.joystickRawThreshold) y = -1;
            return y;
        }
        return 0;
    }

    public static LoruInput instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


}
