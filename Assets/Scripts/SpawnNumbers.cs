using UnityEngine;

public class SpawnNumbers : MonoBehaviour
{
    [SerializeField] private GameObject numberPrefab;
    public void SpawnNumber()
    {
        Instantiate(numberPrefab, transform.position, Quaternion.identity, transform);
    }
}
