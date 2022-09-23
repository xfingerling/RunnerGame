using UnityEngine;

public class MainApp : MonoBehaviour
{
    void Awake()
    {
        Game.Run();

        AdManager.instance.AdInit();
    }


    private void LateUpdate()
    {
        InputManager.instance.ResetInputs();
    }
}
