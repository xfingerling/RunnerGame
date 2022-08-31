using UnityEngine;

public class RunningState : IBaseState
{
    public void Construct(PlayerMotor motor)
    {
        motor.transform.rotation = Quaternion.identity;
        motor.verticalVelocity = 0;
    }

    public void Destruct(PlayerMotor motor)
    {

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

        if (InputManager.Instance.SwipeUp && motor.isGrounded)
            motor.ChangeState(new JumpingState());

        if (InputManager.Instance.SwipeDown && motor.isGrounded)
            motor.ChangeState(new SlidingState());

        if (!motor.isGrounded)
            motor.ChangeState(new FallingState());
    }
}
