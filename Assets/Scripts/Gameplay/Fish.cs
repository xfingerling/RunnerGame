using UnityEngine;

public class Fish : MonoBehaviour
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
            PickupFish();
    }

    private void OnShowChunk()
    {
        _anim?.SetTrigger("Idle");
    }

    private void PickupFish()
    {
        _anim?.SetTrigger("Pickup");
    }

    private void OnDisable()
    {
        _chunk.OnShowChunkEvent -= OnShowChunk;
    }
}
