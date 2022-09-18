using UnityEngine;

public class PlayerStateFalling : PlayerStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.Anim?.SetTrigger("Fall");
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

        if (player.isGrounded)
            player.SetStateRun();
    }
}
