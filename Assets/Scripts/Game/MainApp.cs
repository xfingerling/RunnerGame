using UnityEngine;

public class MainApp : MonoBehaviour
{
    void Start()
    {
        Game.Run();

        AdManager.instance.AdInit();
    }


    private void LateUpdate()
    {
        InputManager.instance.ResetInputs();
    }
}
