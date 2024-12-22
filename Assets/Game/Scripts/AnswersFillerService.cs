using System.Collections.Generic;
using System.Linq;
using TestAmayaQuiz.Data;
using UnityEngine;

namespace TestAmayaQuiz
{
    public class AnswersFillerService
    {
        private AnswerChooseService _answerChooseService;
        private CellsGeneratorService _cellsGeneratorService;

        private string _rightAnswer;
        private AnswersData _answerData;

        public AnswersFillerService(AnswerChooseService answerChooseService, CellsGeneratorService cellsGeneratorService)
        {
            _answerChooseService = answerChooseService;
            _cellsGeneratorService = cellsGeneratorService;

            _cellsGeneratorService.OnCellsCreated += FillCells;
            _answerChooseService.OnAnswerChosen += (answer, answerData) =>
            {
                _rightAnswer = answer;
                _answerData = answerData;
            };

        }
        private void FillCells(Cell[] cells)
        {
            Dictionary<string, SpriteWithRotation> answersDict = _answerData.AnswersDict;
            int rightAnswerCellIndex = Random.Range(0, cells.Length);
            cells[rightAnswerCellIndex].SetData(_rightAnswer, answersDict[_rightAnswer].Sprite, answersDict[_rightAnswer].RotationAngle);

            Dictionary<string, SpriteWithRotation> usedAnswers = new Dictionary<string, SpriteWithRotation>();
            usedAnswers[_rightAnswer] = answersDict[_rightAnswer];

            for (int i = 0; i < cells.Length; i++)
            {
                if (i == rightAnswerCellIndex)
                {
                    continue;
                }

                var remainedAnswers = answersDict.Except(usedAnswers);
                var answerVariant = remainedAnswers.ElementAt(Random.Range(0, remainedAnswers.Count()));
                cells[i].SetData(answerVariant.Key, answerVariant.Value.Sprite, answerVariant.Value.RotationAngle);
                usedAnswers[answerVariant.Key] = answerVariant.Value;
            }

            usedAnswers.Clear();
        }
    }
}