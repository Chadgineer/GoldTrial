using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Vector2 topLeftCorner;
    [SerializeField] private Vector2 bottomRightCorner;
    [SerializeField] private int minSpawnCount = 5;
    [SerializeField] private int maxSpawnCount = 10;
    [SerializeField] private float checkRadius = 0.5f;
    [SerializeField] private LayerMask spawnLayer;

    private readonly HashSet<Vector2> occupiedPositions = new();

    private void Start()
    {
        int spawnTotal = Random.Range(minSpawnCount, maxSpawnCount + 1);
        int spawned = 0;

        while (spawned < spawnTotal)
        {
            Vector2 spawnPos = GetRandomPosition();

            if (IsOccupied(spawnPos)) continue;

            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
            Instantiate(prefab, spawnPos, Quaternion.identity);
            occupiedPositions.Add(spawnPos);
            spawned++;
        }
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(topLeftCorner.x, bottomRightCorner.x);
        float y = Random.Range(bottomRightCorner.y, topLeftCorner.y);
        return new Vector2(x, y);
    }


    private bool IsOccupied(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, checkRadius, spawnLayer);
        return hit != null;
    }
}