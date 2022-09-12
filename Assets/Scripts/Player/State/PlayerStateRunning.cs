using UnityEngine;

public class PlayerStateRunning : IPlayerState
{
    public void Construct(Player motor)
    {
        motor.ResumePlayer();
        motor.transform.rotation = Quaternion.identity;
        motor.verticalVelocity = 0;
    }

    public void Destruct(Player motor)
    {

    }

    public void ProcessMotion(Player motor)
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1f;
        m.z = motor.BaseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(Player motor)
    {
        if (InputManager.instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.instance.SwipeRight)
            motor.ChangeLane(1);

        if (InputManager.instance.SwipeUp && motor.isGrounded)
            motor.SetStateJump();

        if (InputManager.instance.SwipeDown && motor.isGrounded)
            motor.SetStateSlide();

        if (!motor.isGrounded)
            motor.SetStateFall();
    }
}
