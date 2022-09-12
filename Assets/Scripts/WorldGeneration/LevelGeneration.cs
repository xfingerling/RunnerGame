using System.Collections.Generic;
using UnityEngine;

public abstract class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int _firstChunkSpawnPosition = 20;
    [SerializeField] private int _chunkOnScreen = 5;
    [SerializeField] private float _despawnDistance = 5f;
    [SerializeField] private List<GameObject> _chunkPrefabs;

    private float _chunkSpawnZ;
    private Queue<Chunk> _activeChunks;
    private List<Chunk> _chunkPool;
    private Transform _player;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;

        _activeChunks = new Queue<Chunk>();
        _chunkPool = new List<Chunk>();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.player.transform;

        ResetWorld();
    }

    public void ResetWorld()
    {
        _chunkSpawnZ = _firstChunkSpawnPosition;

        for (int i = _activeChunks.Count; i != 0; i--)
            DeleteLastChunk();

        for (int i = 0; i < _chunkOnScreen; i++)
            SpawnNewChunk();
    }

    public void UpdateLevel()
    {
        float playerZ = _player.position.z;
        Chunk lastChunk = _activeChunks.Peek();

        if (playerZ >= lastChunk.transform.position.z + lastChunk.ChunkLength + _despawnDistance)
        {
            DeleteLastChunk();
            SpawnNewChunk();
        }
    }

    private void SpawnNewChunk()
    {
        int randomIndex = Random.Range(0, _chunkPrefabs.Count);

        Chunk chunk = _chunkPool.Find(x => !x.gameObject.activeSelf && x.name == $"{_chunkPrefabs[randomIndex].name}(Clone)");

        if (!chunk)
        {
            GameObject go = Instantiate(_chunkPrefabs[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        chunk.gameObject.transform.position = new Vector3(0, 0, _chunkSpawnZ);
        _chunkSpawnZ += chunk.ChunkLength;

        _activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = _activeChunks.Dequeue();
        chunk.HideChunk();
        _chunkPool.Add(chunk);
    }
}
