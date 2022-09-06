using UnityEngine;

public class PlayerStateJumping : IPlayerState
{
    public void Construct(PlayerMotor motor)
    {
        motor.Anim?.SetTrigger("Jump");
        motor.verticalVelocity = motor.JumpForce;
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

        if (motor.verticalVelocity < 0)
            motor.SetStateFall();
    }
}
