using UnityEngine;

public class FallingState : IBaseState
{
    public void Construct(PlayerMotor motor)
    {
        motor.Anim?.SetTrigger("Fall");
    }

    public void Destruct(PlayerMotor motor)
    {

    }

    public void ProcessMotion(PlayerMotor motor)
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.BaseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(PlayerMotor motor)
    {
        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);

        if (motor.isGrounded)
            motor.ChangeState(new RunningState());
    }
}
