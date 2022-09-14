public class PlayerStateIdle : PlayerStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.ResetPlayer();
    }

    public override void Destruct()
    {

    }

    public override void ProcessMotion()
    {

    }

    public override void Transition()
    {

    }
}
