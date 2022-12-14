using UnityEngine;

public class PlayerStateJumping : PlayerStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.Anim?.SetTrigger("Jump");
        player.verticalVelocity = player.JumpForce;
    }

    public override void Destruct()
    {

    }

    public override void ProcessMotion()
    {
        player.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = player.SnapToLane();
        m.y = player.verticalVelocity;
        m.z = player.baseRunSpeed;

        player.moveVector = m;
    }

    public override void Transition()
    {
        if (InputManager.instance.SwipeLeft)
            player.ChangeLane(-1);

        if (InputManager.instance.SwipeRight)
            player.ChangeLane(1);

        if (player.verticalVelocity < 0)
            player.SetStateFall();
    }
}
