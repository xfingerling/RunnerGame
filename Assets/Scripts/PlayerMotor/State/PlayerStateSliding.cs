using UnityEngine;

public class PlayerStateSliding : IPlayerState
{
    private Vector3 _initialCenter;
    private float _initialSize;
    private float _slideStart;

    public void Construct(PlayerMotor motor)
    {
        motor.Anim?.ResetTrigger("Running");
        motor.Anim?.SetTrigger("Slide");
        _slideStart = Time.time;

        _initialSize = motor.Controller.height;
        _initialCenter = motor.Controller.center;

        motor.Controller.height = _initialSize * 0.5f;
        motor.Controller.center = _initialCenter * 0.5f;
    }

    public void Destruct(PlayerMotor motor)
    {
        motor.Controller.height = _initialSize;
        motor.Controller.center = _initialCenter;

        motor.Anim?.SetTrigger("Running");
    }

    public void ProcessMotion(PlayerMotor motor)
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1f;
        m.z = motor.BaseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(PlayerMotor motor)
    {
        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);

        if (!motor.isGrounded)
            motor.SetStateFall();

        if (InputManager.Instance.SwipeUp)
            motor.SetStateJump();

        if (Time.time - _slideStart > motor.SlideDuration)
            motor.SetStateRun();
    }
}
