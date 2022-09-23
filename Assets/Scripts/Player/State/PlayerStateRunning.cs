using UnityEngine;

public class PlayerStateRunning : PlayerStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.Anim.SetTrigger("Running");
        player.ResumePlayer();
        player.transform.rotation = Quaternion.identity;
        player.verticalVelocity = 0;

        player.dustParticle.Play();
    }

    public override void Destruct()
    {
        player.dustParticle.Stop();
    }

    public override void ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = player.SnapToLane();
        m.y = -2f;
        m.z = player.baseRunSpeed * player.speedFactor;

        player.moveVector = m;
    }

    public override void Transition()
    {
        if (InputManager.instance.swipeLeft)
            player.ChangeLane(-1);

        if (InputManager.instance.swipeRight)
            player.ChangeLane(1);

        if (InputManager.instance.swipeUp && player.isGrounded)
            player.SetStateJump();

        if (InputManager.instance.swipeDown && player.isGrounded)
            player.SetStateSlide();

        if (!player.isGrounded)
            player.SetStateFall();
    }
}
