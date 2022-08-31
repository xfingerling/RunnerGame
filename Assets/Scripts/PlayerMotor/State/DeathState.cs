using UnityEngine;

public class DeathState : IBaseState
{
    private Vector3 _currentKnockback;

    public void Construct(PlayerMotor motor)
    {
        motor.Anim?.SetTrigger("Death");
        _currentKnockback = motor.KnockbackForce;
    }

    public void Destruct(PlayerMotor motor)
    {

    }

    public void ProcessMotion(PlayerMotor motor)
    {
        Vector3 m = _currentKnockback;

        _currentKnockback = new Vector3(0, _currentKnockback.y -= motor.Gravity * Time.deltaTime, _currentKnockback.z += 2f * Time.deltaTime);

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
