using UnityEngine;

public class PlayerStateRunning : PlayerStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.ResumePlayer();
        player.transform.rotation = Quaternion.identity;
        player.verticalVelocity = 0;
    }

    public override void Destruct()
    {

    }

    public override void ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = player.SnapToLane();
        m.y = -1f;
        m.z = player.baseRunSpeed;

        player.moveVector = m;
    }

    public override void Transition()
    {
        if (InputManager.instance.SwipeLeft)
            player.ChangeLane(-1);

        if (InputManager.instance.SwipeRight)
            player.ChangeLane(1);

        if (InputManager.instance.SwipeUp && player.isGrounded)
            player.SetStateJump();

        if (InputManager.instance.SwipeDown && player.isGrounded)
            player.SetStateSlide();

        if (!player.isGrounded)
            player.SetStateFall();
    }
}
