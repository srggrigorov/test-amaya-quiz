using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Quiz/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    public int rows;           
    public int columns;        
    public Sprite[] objects;   
    public Sprite correctAnswer; 
}
