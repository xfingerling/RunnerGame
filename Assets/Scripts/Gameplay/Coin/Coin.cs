using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collectParticle;
    [SerializeField] private AudioSource _collectSound;

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
        _collectParticle.Play();
        PlayPickupSound();
        Bank.AddCoins(this);
    }

    private void OnDisable()
    {
        _chunk.OnShowChunkEvent -= OnShowChunk;
    }

    private void PlayPickupSound()
    {
        _collectSound.pitch = Random.Range(0.8f, 1.2f);
        _collectSound.Play();
    }
}
