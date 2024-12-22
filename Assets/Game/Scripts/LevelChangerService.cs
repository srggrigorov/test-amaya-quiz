using System;
using System.Collections.Generic;
using TestAmayaQuiz.Data;

public class LevelChangerService
{
    public event Action<LevelData> OnLevelChanged;

    private List<LevelData> _levelDataList = new List<LevelData>();
    private int _levelIndexToLoad;

    public LevelChangerService(List<LevelData> levelDataList)
    {
        _levelDataList = levelDataList;
    }

    public void ChangeLevel()
    {
        if (_levelIndexToLoad >= _levelDataList.Count)
        {
            return;
        }

        OnLevelChanged?.Invoke(_levelDataList[_levelIndexToLoad]);
        _levelIndexToLoad++;
    }
}
