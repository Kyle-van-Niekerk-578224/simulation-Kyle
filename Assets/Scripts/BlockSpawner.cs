using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject blockPrefab;
    public Transform blockSpawnPoint;

    public void Update()
    {
        if (Input.GetKey(KeyCode.ScrollLock))
        {
            Instantiate(blockPrefab, blockSpawnPoint.position, Quaternion.identity, transform);
        }
    }

    public void SpawnBlock()
    {
        GameObject newblock = Instantiate(blockPrefab, blockSpawnPoint.position, blockSpawnPoint.rotation, transform);
    }
}
