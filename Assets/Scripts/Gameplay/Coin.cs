using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator _anim;
    private Chunk _chunk;

    private void Awake()
    {
        _anim = GetComponentInParent<Animator>();
        _chunk = GetComponentInParent<Chunk>();
    }

    private void OnEnable()
    {
        _chunk.OnShowChunkEvent += OnShowChunk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CollectCoin();
    }

    private void OnShowChunk()
    {
        _anim?.SetTrigger("Idle");
    }

    private void CollectCoin()
    {
        _anim?.SetTrigger("Pickup");
        Bank.AddCoins(this);
    }

    private void OnDisable()
    {
        _chunk.OnShowChunkEvent -= OnShowChunk;
    }
}
