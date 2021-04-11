using AP8POSecretary.Domain.Constants;
using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.State.Navigators;
using AP8POSecretary.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly ISecretaryViewModelAbstractFactory _secretaryViewModelAbstractFactory;
        private readonly IDataService<WorkingPointsWeight> _settingsDataService;

        public IList<WorkingPointsWeight> WorkingPointsWeights { get; } = new List<WorkingPointsWeight>
        {
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.Lecture, Value = WorkingPointsWeightConst.LECTURE },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.Practise, Value = WorkingPointsWeightConst.PRACTISE },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.Seminare, Value = WorkingPointsWeightConst.SEMINARE },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.LectureEng, Value = WorkingPointsWeightConst.LECTURE_ENG },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.PractiseEng, Value = WorkingPointsWeightConst.PRACTISE_ENG },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.SeminareEng, Value = WorkingPointsWeightConst.SEMINARE_ENG },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.Credit, Value = WorkingPointsWeightConst.CREDIT },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.ClassifiedCredit, Value = WorkingPointsWeightConst.CLASSIFIEDCREDIT },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.Exam, Value = WorkingPointsWeightConst.EXAM },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.CreditEng, Value = WorkingPointsWeightConst.CREDIT_ENG },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.ClassifiedCreditEng, Value = WorkingPointsWeightConst.CLASSIFIEDCREDIT_ENG },
            new WorkingPointsWeight(){ WorkingWeightTypes = WorkingWeightTypes.ExamEng, Value = WorkingPointsWeightConst.EXAM_ENG },
        };

        public MainViewModel(INavigator navigator, ISecretaryViewModelAbstractFactory secretaryViewModelAbstractFactory, IDataService<WorkingPointsWeight> settingsDataService)
        {
            Navigator = navigator;
            _secretaryViewModelAbstractFactory = secretaryViewModelAbstractFactory;
            ViewType viewType = ViewType.Subjects;
            Navigator.CurrentViewModel = _secretaryViewModelAbstractFactory.CreateViewModel(viewType);

            _settingsDataService = settingsDataService;

            _settingsDataService.AddAllIfTableEmpty(WorkingPointsWeights);

        }



       
    }
}
