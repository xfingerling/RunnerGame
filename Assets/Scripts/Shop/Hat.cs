using UnityEngine;

[CreateAssetMenu(fileName = "Hat")]
public class Hat : ScriptableObject
{
    public string ItemName;
    public int ItemPrice;
    public Sprite IconHat;
    public GameObject Model;
}
