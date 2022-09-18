using UnityEngine;

public class PlayerStateJumping : PlayerStateBase
{
    public override void Construct()
    {
        base.Construct();
        player.jumpSound.Play();
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
        if (InputManager.instance.swipeLeft)
            player.ChangeLane(-1);

        if (InputManager.instance.swipeRight)
            player.ChangeLane(1);

        if (player.verticalVelocity < 0)
            player.SetStateFall();
    }
}
