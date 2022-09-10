using UnityEngine;

public class PlayerStateJumping : IPlayerState
{
    public void Construct(Player motor)
    {
        motor.Anim?.SetTrigger("Jump");
        motor.verticalVelocity = motor.JumpForce;
    }

    public void Destruct(Player motor)
    {

    }

    public void ProcessMotion(Player motor)
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.BaseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(Player motor)
    {
        if (InputManager.instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.instance.SwipeRight)
            motor.ChangeLane(1);

        if (motor.verticalVelocity < 0)
            motor.SetStateFall();
    }
}
