using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Material _material;
    public float offsetSpeed = 0.25f;

    private void Update()
    {
        transform.position = Vector3.forward * _player.transform.position.z;
        _material.SetVector("_offset", new Vector2(0, -transform.position.z * offsetSpeed));
    }
}
