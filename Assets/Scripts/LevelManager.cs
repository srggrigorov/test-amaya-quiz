using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GridGenerator gridGenerator;   
    public LevelData[] levels;            
    public GameObject cellPrefab;         
    private int currentLevel = 0;         

    private void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length) return;

      
        LevelData levelData = levels[levelIndex];
        currentLevel = levelIndex;

        
        gridGenerator.GenerateGrid(levelData.rows, levelData.columns, cellPrefab);

        
        AssignObjectsToCells(levelData.objects, levelData.correctAnswer);
    }

    private void AssignObjectsToCells(Sprite[] objects, Sprite correctAnswer)
    {
        
        Transform[] cells = gridGenerator.transform.GetComponentsInChildren<Transform>();

        
        if (objects.Length > cells.Length - 1)
        {
            Debug.LogError(" оличество объектов превышает количество €чеек!");
            return;
        }

        for (int i = 0; i < objects.Length; i++)
        {
            SpriteRenderer spriteRenderer = cells[i + 1].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = objects[i]; 
            }
            else
            {
                Debug.LogWarning($"SpriteRenderer не найден в €чейке {cells[i + 1].name}");
            }
        }
    }
}
