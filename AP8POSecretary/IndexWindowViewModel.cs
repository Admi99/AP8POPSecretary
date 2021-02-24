using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary
{
    public class IndexWindowViewModel
    {
        private readonly IDataService<Group> dataService;

        public IndexWindowViewModel(IDataService<Group> dataService)
        {
            this.dataService = dataService;

            Tes();         
        }

        private async void Tes()
        {
            Group groups = new Group
            {
                Shortcut = "bla101",
                StudyYear = 5,
                Language = "d",
                StudentsCount = 5,
                StudyType = StudyType.DAILY,
                SemesterType = SemesterType.SPRING,
                Subjects = null
            };

            var test = await dataService.Create(groups);

        }
    }
}
