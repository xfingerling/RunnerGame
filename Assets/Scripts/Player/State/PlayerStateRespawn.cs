using UnityEngine;

public class PlayerStateRespawn : PlayerStateBase
{
    private float _startTime;

    public override void Construct()
    {
        base.Construct();

        _startTime = Time.time;

        player.Controller.enabled = false;
        player.transform.position = new Vector3(0, player.VerticalDistance, player.transform.position.z);
        player.Controller.enabled = true;

        player.verticalVelocity = 0;
        player.currentLane = 0;
        player.Anim?.SetTrigger("Respawn");
        player.ResumePlayer();
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
        m.z = player.baseRunSpeed * player.speedFactor;

        player.moveVector = m;
    }

    public override void Transition()
    {
        if (player.isGrounded && (Time.time - _startTime) > player.ImmunityTime)
            gameController.SetStateGame();

        if (InputManager.instance.swipeLeft)
            player.ChangeLane(-1);

        if (InputManager.instance.swipeRight)
            player.ChangeLane(1);
    }
}
