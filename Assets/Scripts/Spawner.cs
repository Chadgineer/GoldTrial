using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrefabChance
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float probability;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<PrefabChance> prefabChances;
    [SerializeField] private float leftOffset = 0f;
    [SerializeField] private float rightOffset = 0f;
    [SerializeField] private float topOffset = 0f;
    [SerializeField] private float bottomOffset = 0f;
    [SerializeField] private int minSpawnCount = 5;
    [SerializeField] private int maxSpawnCount = 10;
    [SerializeField] private float checkRadius = 0.5f;
    [SerializeField] private LayerMask spawnLayer;

    private readonly HashSet<Vector2> occupiedPositions = new();

    private void Start()
    {
        int spawnTotal = UnityEngine.Random.Range(minSpawnCount, maxSpawnCount + 1);
        int spawned = 0;

        while (spawned < spawnTotal)
        {
            Vector2 spawnPos = GetRandomPosition();
            if (IsOccupied(spawnPos)) continue;

            GameObject prefab = GetRandomPrefab();
            Instantiate(prefab, spawnPos, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f)));
            occupiedPositions.Add(spawnPos);
            spawned++;
        }
    }

    private GameObject GetRandomPrefab()
    {
        float total = 0f;
        foreach (var pc in prefabChances)
            total += pc.probability;

        float random = UnityEngine.Random.Range(0f, total);
        float cumulative = 0f;

        foreach (var pc in prefabChances)
        {
            cumulative += pc.probability;
            if (random <= cumulative)
                return pc.prefab;
        }

        // Fallback (should not hit)
        return prefabChances[0].prefab;
    }

    private Vector2 GetRandomPosition()
    {
        Camera cam = Camera.main;
        Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float x = UnityEngine.Random.Range(min.x + leftOffset, max.x - rightOffset);
        float y = UnityEngine.Random.Range(min.y + bottomOffset, max.y - topOffset);
        return new Vector2(x, y);
    }

    private bool IsOccupied(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, checkRadius, spawnLayer);
        return hit != null;
    }
}