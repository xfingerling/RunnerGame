using UnityEngine;

public class MainApp : MonoBehaviour
{
    void Start()
    {
        Game.Run();
    }

    private void LateUpdate()
    {
        InputManager.ResetInputs();
    }
}
