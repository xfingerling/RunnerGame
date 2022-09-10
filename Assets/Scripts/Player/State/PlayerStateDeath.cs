using UnityEngine;

public class PlayerStateDeath : IPlayerState
{
    private Vector3 _currentKnockback;

    public void Construct(Player motor)
    {
        motor.Anim?.SetTrigger("Death");
        _currentKnockback = motor.KnockbackForce;
    }

    public void Destruct(Player motor)
    {

    }

    public void ProcessMotion(Player motor)
    {
        Vector3 m = _currentKnockback;

        _currentKnockback = new Vector3(0, _currentKnockback.y -= motor.Gravity * Time.deltaTime, _currentKnockback.z += 2f * Time.deltaTime);

        if (_currentKnockback.z > 0)
        {
            _currentKnockback.z = 0;
            //GameFlow.Instance.SetStateDeath();
        }

        motor.moveVector = m;
    }

    public void Transition(Player motor)
    {

    }
}
