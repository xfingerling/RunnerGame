using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator _anim;
    private Chunk _chunk;

    private void Start()
    {
        _anim = GetComponentInParent<Animator>();
        _chunk = GetComponentInParent<Chunk>();
        _chunk.OnShowChunkEvent += OnShowChunk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PickupCoin();
    }

    private void OnShowChunk()
    {
        _anim?.SetTrigger("Idle");
    }

    private void PickupCoin()
    {
        _anim?.SetTrigger("Pickup");
        GameStats.Instance.CollectCoin();
    }

    private void OnDisable()
    {
        _chunk.OnShowChunkEvent -= OnShowChunk;
    }
}