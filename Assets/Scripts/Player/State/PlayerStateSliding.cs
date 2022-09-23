using UnityEngine;

public class PlayerStateSliding : PlayerStateBase
{
    private Vector3 _initialCenter;
    private float _initialSize;
    private float _slideStart;

    public override void Construct()
    {
        base.Construct();

        player.Anim?.SetTrigger("Slide");
        _slideStart = Time.time;

        _initialSize = player.Controller.height;
        _initialCenter = player.Controller.center;

        player.Controller.height = _initialSize * 0.3f;
        player.Controller.center = _initialCenter * 0.3f;
    }

    public override void Destruct()
    {
        player.Controller.height = _initialSize;
        player.Controller.center = _initialCenter;
    }

    public override void ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = player.SnapToLane();
        m.y = -10f;
        m.z = player.baseRunSpeed * player.speedFactor;

        player.moveVector = m;
    }

    public override void Transition()
    {
        if (InputManager.instance.swipeLeft)
            player.ChangeLane(-1);

        if (InputManager.instance.swipeRight)
            player.ChangeLane(1);

        if (InputManager.instance.swipeUp)
            player.SetStateJump();

        if (Time.time - _slideStart > player.slideDuration)
            player.SetStateRun();
    }
}
