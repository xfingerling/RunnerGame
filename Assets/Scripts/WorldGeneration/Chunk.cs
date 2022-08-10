using System;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public event Action OnShowChunkEvent;

    [SerializeField] private float _chunkLength;

    public float ChunkLength => _chunkLength;

    public Chunk ShowChunk()
    {
        OnShowChunkEvent?.Invoke();
        gameObject.SetActive(true);
        return this;
    }

    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
