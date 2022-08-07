using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    [SerializeField] private float sqrSwipeDeadzone = 50f;

    //Action scheme
    private RunnerInputAction _actionScheme;

    #region public properties
    public bool Tap { get { return _tap; } }
    public Vector2 TouchPosition { get { return _touchPosition; } }
    public bool SwipeLeft { get { return _swipeLeft; } }
    public bool SwipeRight { get { return _swipeRight; } }
    public bool SwipeUp { get { return _swipeUp; } }
    public bool SwipeDown { get { return _swipeDown; } }
    #endregion

    #region private properties
    private bool _tap;
    private Vector2 _touchPosition;
    private Vector2 _startDrag;
    private bool _swipeLeft;
    private bool _swipeRight;
    private bool _swipeUp;
    private bool _swipeDown;
    #endregion

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }

    private void LateUpdate()
    {
        ResetInputs();
    }

    public void OnEnable()
    {
        _actionScheme.Enable();
    }

    public void OnDisable()
    {
        _actionScheme.Disable();
    }

    private void ResetInputs()
    {
        _tap = _swipeDown = _swipeUp = _swipeLeft = _swipeRight = false;
    }

    private void SetupControl()
    {
        _actionScheme = new RunnerInputAction();

        _actionScheme.Gameplay.Tap.performed += context => OnTap(context);
        _actionScheme.Gameplay.TouchPosition.performed += context => OnPosition(context);
        _actionScheme.Gameplay.StartDrag.performed += context => OnStartDrag(context);
        _actionScheme.Gameplay.EndDrag.performed += context => OnEndDrag(context);
    }

    private void OnEndDrag(InputAction.CallbackContext context)
    {
        Vector2 delta = _touchPosition - _startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > sqrSwipeDeadzone)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);

            if (x > y) //Left of right
            {
                if (delta.x > 0)
                    _swipeRight = true;
                else
                    _swipeLeft = true;
            }
            else //Up or Down
            {
                if (delta.y > 0)
                    _swipeUp = true;
                else
                    _swipeDown = true;
            }
        }

        _startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext context)
    {
        _startDrag = _touchPosition;
    }

    private void OnPosition(InputAction.CallbackContext context)
    {
        _touchPosition = context.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        _tap = true;
    }
}
