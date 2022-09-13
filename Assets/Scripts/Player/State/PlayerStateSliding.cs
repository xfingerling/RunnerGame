using UnityEngine;

public class PlayerStateSliding : IPlayerState
{
    private Vector3 _initialCenter;
    private float _initialSize;
    private float _slideStart;

    public void Construct(Player motor)
    {
        motor.Anim?.ResetTrigger("Running");
        motor.Anim?.SetTrigger("Slide");
        _slideStart = Time.time;

        _initialSize = motor.Controller.height;
        _initialCenter = motor.Controller.center;

        motor.Controller.height = _initialSize * 0.5f;
        motor.Controller.center = _initialCenter * 0.5f;
    }

    public void Destruct(Player motor)
    {
        motor.Controller.height = _initialSize;
        motor.Controller.center = _initialCenter;

        motor.Anim?.SetTrigger("Running");
    }

    public void ProcessMotion(Player motor)
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1f;
        m.z = motor.baseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(Player motor)
    {
        if (InputManager.instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.instance.SwipeRight)
            motor.ChangeLane(1);

        if (!motor.isGrounded)
            motor.SetStateFall();

        if (InputManager.instance.SwipeUp)
            motor.SetStateJump();

        if (Time.time - _slideStart > motor.slideDuration)
            motor.SetStateRun();
    }
}
