using System;
using TestAmayaQuiz.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TestAmayaQuiz
{
    public class CellSelector : IDisposable
    {
        private Controls _controls;
        private LevelChangerService _levelChangerService;
        private AnswerComparerService _answererComparerService;
        private Camera _camera;
        private RaycastHit2D[] hits = new RaycastHit2D[1];
        private Action _onAnimationComplete;

        public CellSelector(LevelChangerService levelChangerService, AnswerComparerService answerComparerService, Camera camera)
        {
            _levelChangerService = levelChangerService;
            _answererComparerService = answerComparerService;
            _camera = camera;

            _controls = new Controls();
            _controls.Enable();

            _controls.PlayerMap.Select.performed += CheckCell;
        }

        private void CheckCell(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(_controls.PlayerMap.Position.ReadValue<Vector2>());
            Physics2D.RaycastNonAlloc(ray.origin, ray.direction, hits, Mathf.Infinity);
            if (hits.Length > 0)
            {
                var selectedCell = hits[0].collider.GetComponent<Cell>();
                if (_answererComparerService.IsRightAnswer(selectedCell.Answer))
                {
                    _onAnimationComplete += ChangeLevel;
                    selectedCell.PlayRightAnswerAnim(_onAnimationComplete);
                    _controls.Disable();
                }
                else
                {
                    selectedCell.PlayWrongAnswerAnim();
                }
            }
        }

        private void ChangeLevel()
        {
            _onAnimationComplete -= ChangeLevel;
            _levelChangerService.ChangeLevel();
            _controls.Enable();
        }

        public void Dispose()
        {
            _controls?.Dispose();
        }
    }
}
