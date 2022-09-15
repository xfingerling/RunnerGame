using UnityEngine;

public class PlayerStateDeath : PlayerStateBase
{
    private Vector3 _currentKnockback;

    public override void Construct()
    {
        base.Construct();

        player.Anim?.SetTrigger("Death");
        _currentKnockback = player.KnockbackForce;
    }

    public override void Destruct()
    {
        UIController.HideHUD();
    }

    public override void ProcessMotion()
    {
        Vector3 m = _currentKnockback;

        _currentKnockback = new Vector3(0, _currentKnockback.y -= player.gravity * Time.deltaTime, _currentKnockback.z += 2f * Time.deltaTime);

        if (_currentKnockback.z > 0)
        {
            _currentKnockback.z = 0;
            player.PausePlayer();
        }

        player.moveVector = m;
    }

    public override void Transition()
    {

    }
}
