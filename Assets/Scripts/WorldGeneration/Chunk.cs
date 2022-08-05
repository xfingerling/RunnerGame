using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private float _chunkLength;

    public float ChunkLength => _chunkLength;

    public Chunk ShowChunk()
    {
        gameObject.SetActive(true);
        return this;
    }

    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
