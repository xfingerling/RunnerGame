using System;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public event Action OnShowChunkEvent;

    public Transform beginChunk { get; private set; }
    public Transform endChunk { get; private set; }

    private void Awake()
    {
        beginChunk = transform.Find("Begin");
        endChunk = transform.Find("End");
    }

    public Chunk ShowChunk()
    {
        gameObject.SetActive(true);
        OnShowChunkEvent?.Invoke();
        return this;
    }

    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
