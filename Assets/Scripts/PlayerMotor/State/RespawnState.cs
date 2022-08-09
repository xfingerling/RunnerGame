using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float _verticalDistance = 25f;
    [SerializeField] private float _immunityTime = 1f;

    private float _startTime;

    public override void Construct()
    {
        _startTime = Time.time;

        motor.Controller.enabled = false;
        motor.transform.position = new Vector3(0, _verticalDistance, motor.transform.position.z);
        motor.Controller.enabled = true;

        motor.verticalVelocity = 0;
        motor.currentLane = 0;
        motor.Anim?.SetTrigger("Respawn");
    }

    public override Vector3 ProcessMotion()
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (motor.isGrounded && (Time.time - _startTime) > _immunityTime)
            motor.ChangeState(GetComponent<RunningState>());

        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);
    }
}
