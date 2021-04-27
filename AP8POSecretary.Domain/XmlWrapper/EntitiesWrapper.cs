using AP8POSecretary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.XmlWrapper
{
    public class EntitiesWrapper
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<WorkingLabel> WorkingLabels { get; set; }
        public IEnumerable<WorkingPointsWeight> WorkingPointsWeights { get; set; }
        public IEnumerable<GroupSubject> GroupSubjects { get; set; }

    }
}
