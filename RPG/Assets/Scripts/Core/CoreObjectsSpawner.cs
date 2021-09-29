using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject coreObjectPrefab;
    private void Awake()
    {
        var existObj = FindObjectsOfType<CoreObjects>();
        if(existObj.Length == 0)
        {
            var spawnPos = new Vector3(0, 0, 0);
            var grid = FindObjectOfType<Grid>();
            if(grid != null)
            {
                spawnPos = grid.transform.position;
            }

            Instantiate(coreObjectPrefab, spawnPos, Quaternion.identity);
        }
    }
}
