using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public void GenerateGrid(int rows, int columns, GameObject cellPrefab)
    {
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int offset = 6;
                
                Vector3 position = new Vector3(col * offset, -row * offset, 0);
                
                Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
