using UnityEngine;
using Zenject;
using TMPro;

namespace TestAmayaQuiz
{
    public class RightAnswerView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _answerText;

        [Inject]
        public void Construct(AnswerChooseService answerChooseService)
        {
            answerChooseService.OnAnswerChosen += (answer, answerData) => _answerText.text = answer;
        }
    }
}
