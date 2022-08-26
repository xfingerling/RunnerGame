using UnityEngine;

public class RespawnState : IBaseState
{
    [SerializeField] private float _verticalDistance = 25f;
    [SerializeField] private float _immunityTime = 1f;

    private float _startTime;

    public void Construct(PlayerMotor motor)
    {
        _startTime = Time.time;

        motor.Controller.enabled = false;
        motor.transform.position = new Vector3(0, _verticalDistance, motor.transform.position.z);
        motor.Controller.enabled = true;

        motor.verticalVelocity = 0;
        motor.currentLane = 0;
        motor.Anim?.SetTrigger("Respawn");
    }

    public void Destruct(PlayerMotor motor)
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
    }

    public void ProcessMotion(PlayerMotor motor)
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(PlayerMotor motor)
    {
        if (motor.isGrounded && (Time.time - _startTime) > _immunityTime)
            motor.ChangeState(new RunningState());

        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);
    }
}
