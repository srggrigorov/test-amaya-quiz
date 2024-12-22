using System.Collections.Generic;
using System.Linq;
using TestAmayaQuiz.Data;
using UnityEngine;


namespace TestAmayaQuiz
{
    public class AnswerChooseService
    {
        public delegate void OnAnswerChosenDelegate(string answer, AnswersData answersData);
        public event OnAnswerChosenDelegate OnAnswerChosen;

        private CellsGeneratorService _cellsGenerator;
        private Dictionary<AnswersData, Dictionary<string, SpriteWithRotation>> _usedAnswersDict = new Dictionary<AnswersData, Dictionary<string, SpriteWithRotation>>();

        public AnswerChooseService(List<AnswersData> answersDataList, CellsGeneratorService cellsGenerator)
        {
            _cellsGenerator = cellsGenerator;

            foreach (var answersData in answersDataList)
            {
                _usedAnswersDict[answersData] = new Dictionary<string, SpriteWithRotation>();
            }

            _cellsGenerator.OnCellsCreated += ChooseAnswer;
        }

        private void ChooseAnswer(Cell[] cells)
        {
            var remainedAnswersDatas = _usedAnswersDict.Where(pair => pair.Key.AnswersDict.Count >= cells.Length && pair.Key.AnswersDict.Count > pair.Value.Count);
            var randomAnswersData = remainedAnswersDatas.ElementAt(Random.Range(0, remainedAnswersDatas.Count()));
            var remainedAnswers = randomAnswersData.Key.AnswersDict.Except(randomAnswersData.Value);
            var randomAnswer = remainedAnswers.ElementAt(Random.Range(0, remainedAnswers.Count()));
            _usedAnswersDict[randomAnswersData.Key].Add(randomAnswer.Key, randomAnswer.Value);
            OnAnswerChosen?.Invoke(randomAnswer.Key, randomAnswersData.Key);
        }
    }
}
