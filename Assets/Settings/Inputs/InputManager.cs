using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public static InputManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new InputManager();
            return _instance;
        }
    }
    private static InputManager _instance;

    public bool tap { get; private set; }
    public Vector2 touchPosition { get; private set; }
    public bool swipeLeft { get; private set; }
    public bool swipeRight { get; private set; }
    public bool swipeUp { get; private set; }
    public bool swipeDown { get; private set; }


    private float sqrSwipeDeadzone = 50f;
    private RunnerInputAction _actionScheme;
    private Vector2 _startDrag;

    public InputManager()
    {
        SetupControl();
    }

    public void ResetInputs()
    {
        instance.tap = instance.swipeDown = instance.swipeUp = instance.swipeLeft = instance.swipeRight = false;
    }

    private void SetupControl()
    {
        _actionScheme = new RunnerInputAction();

        _actionScheme.Gameplay.Tap.performed += context => OnTap(context);
        _actionScheme.Gameplay.TouchPosition.performed += context => OnPosition(context);
        _actionScheme.Gameplay.StartDrag.performed += context => OnStartDrag(context);
        _actionScheme.Gameplay.EndDrag.performed += context => OnEndDrag(context);

        _actionScheme.Enable();
    }

    private void OnEndDrag(InputAction.CallbackContext context)
    {
        Vector2 delta = touchPosition - _startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > sqrSwipeDeadzone)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);

            if (x > y) //Left of right
            {
                if (delta.x > 0)
                    swipeRight = true;
                else
                    swipeLeft = true;
            }
            else //Up or Down
            {
                if (delta.y > 0)
                    swipeUp = true;
                else
                    swipeDown = true;
            }
        }

        _startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext context)
    {
        _startDrag = touchPosition;
    }

    private void OnPosition(InputAction.CallbackContext context)
    {
        touchPosition = context.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        tap = true;
    }
}
