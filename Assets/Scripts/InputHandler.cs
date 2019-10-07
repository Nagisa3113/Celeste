using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    protected static InputHandler s_Instance;

    public static InputHandler Instance
    {
        get { return s_Instance; }
    }

    public InputButton SlideButton = new InputButton(ButtonType.SlideButton);
    public InputButton DashButton = new InputButton(ButtonType.DashButton);
    public InputButton JumpButton = new InputButton(ButtonType.JumpButton);

    public Vector2 DirAxis;

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");
    }

    void Update()
    {
        GetInputs();
    }


    void GetInputs()
    {
        SlideButton.Get();
        DashButton.Get();
        JumpButton.Get();
        DirAxis = AxisGet();
    }

    Vector2 AxisGet()
    {
        Vector2 dir;
        dir.x = Input.GetKey(KeyCode.RightArrow) ?
                Input.GetKey(KeyCode.LeftArrow) ? 0 : 1 :
                Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;

        dir.y = Input.GetKey(KeyCode.UpArrow) ?
                Input.GetKey(KeyCode.DownArrow) ? 0 : 1 :
                Input.GetKey(KeyCode.DownArrow) ? -1 : 0;

        return dir;
    }

}



public enum ButtonType
{
    SlideButton,
    DashButton,
    JumpButton,
}

public class InputButton
{
    ButtonType type;

    /// <summary>
    /// The input buffer counter.
    /// </summary>
    int buffer_counter;
    int buffer_max = 5;

    public bool Down { get; protected set; }
    public bool Held { get; protected set; }
    public bool Up { get; protected set; }
    public bool Enabled
    {
        get { return m_Enabled; }
    }

    [SerializeField]
    protected bool m_Enabled = true;
    protected bool m_GettingInput = true;

    //This is used to change the state of a button (Down, Up) only if at least a FixedUpdate happened between the previous Frame
    //and this one. Since movement are made in FixedUpdate, without that an input could be missed it get press/release between fixedupdate
    bool m_AfterFixedUpdateDown;
    bool m_AfterFixedUpdateHeld;
    bool m_AfterFixedUpdateUp;

    public InputButton(ButtonType buttonType)
    {
        this.type = buttonType;
    }

    protected static readonly Dictionary<ButtonType, KeyCode> k_ButtonsToName = new Dictionary<ButtonType, KeyCode>
            {
                {ButtonType.SlideButton,KeyCode.Z},
                {ButtonType.DashButton,KeyCode.X},
                {ButtonType.JumpButton,KeyCode.C},

            };
    public void Get()
    {
        Down = Input.GetKeyDown(k_ButtonsToName[type]);
        Held = Input.GetKey(k_ButtonsToName[type]);
        Up = Input.GetKeyUp(k_ButtonsToName[type]);


        if (Down)
        {
            buffer_counter = 0;
        }

        if (buffer_counter < buffer_max)
        {
            buffer_counter++;
            Down = true;
        }
    }

}

[Serializable]
public class InputAxis
{
    public KeyCode positive;
    public KeyCode negative;

    public float Value { get; protected set; }
    public bool ReceivingInput { get; protected set; }
    public bool Enabled
    {
        get { return m_Enabled; }
    }

    protected bool m_Enabled = true;
    protected bool m_GettingInput = true;

}