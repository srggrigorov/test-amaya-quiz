using System.Collections.Generic;
using TestAmayaQuiz.Data;
using UnityEngine;
using Zenject;

namespace TestAmayaQuiz.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private List<LevelData> _levelDataList = new List<LevelData>();
        [SerializeField]
        private List<AnswersData> _answersDataList = new List<AnswersData>();
        [SerializeField]
        private Transform _cellsContainer;
        [SerializeField]
        private Cell _cellPrefab;
        [SerializeField]
        private Camera _camera;

        private LevelChangerService _levelChangerService;

        public override void InstallBindings()
        {
            _levelChangerService = new LevelChangerService(_levelDataList);
            Container.Bind<LevelChangerService>().FromInstance(_levelChangerService).AsSingle().NonLazy();
            Container.Bind<CellsGeneratorService>().AsSingle().WithArguments(_cellsContainer, _cellPrefab).NonLazy();
            Container.Bind<AnswerChooseService>().AsSingle().WithArguments(_answersDataList).NonLazy();
            Container.Bind<AnswersFillerService>().AsSingle().NonLazy();
            Container.Bind<AnswerComparerService>().AsSingle().NonLazy();
            Container.Bind<CellSelector>().AsSingle().WithArguments(_camera).NonLazy();
        }
        public override void Start()
        {
            _levelChangerService.ChangeLevel();
        }
    }
}
