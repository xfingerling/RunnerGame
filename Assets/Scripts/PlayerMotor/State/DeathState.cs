using UnityEngine;

public class DeathState : IBaseState
{
    [SerializeField] private Vector3 _knockbackForce = new Vector3(0, 4, -3);

    private Vector3 _currentKnockback;

    public void Construct(PlayerMotor motor)
    {
        motor.Anim?.SetTrigger("Death");
        _currentKnockback = _knockbackForce;
    }

    public void Destruct(PlayerMotor motor)
    {

    }

    public void ProcessMotion(PlayerMotor motor)
    {
        Vector3 m = _currentKnockback;

        _currentKnockback = new Vector3(0, _currentKnockback.y -= motor.gravity * Time.deltaTime, _currentKnockback.z += 2f * Time.deltaTime);

        if (_currentKnockback.z > 0)
        {
            _currentKnockback.z = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }

        motor.moveVector = m;
    }

    public void Transition(PlayerMotor motor)
    {

    }
}
