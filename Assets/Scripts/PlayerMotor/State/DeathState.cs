using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 _knockbackForce = new Vector3(0, 4, -3);

    public override void Construct()
    {
        motor.Anim?.SetTrigger("Death");
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 m = _knockbackForce;

        _knockbackForce = new Vector3(0, _knockbackForce.y -= motor.gravity * Time.deltaTime, _knockbackForce.z += 2f * Time.deltaTime);

        if (_knockbackForce.z > 0)
        {
            _knockbackForce.z = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }

        return m;
    }
}
