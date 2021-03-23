using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{
    public enum StudyType
    {
        DAILY,
        DISTANCE
    };

    public enum SemesterType
    {
        SPRING,
        WINTER
    };
    public class Group : Entity
    {
        public string Shortcut { get; set; }
        public int StudyYear { get; set; }
        public string Language { get; set; }
        public int StudentsCount { get; set; }
        public StudyType StudyType { get; set; }
        public SemesterType SemesterType { get; set; }
        public virtual IList<GroupSubject> GroupSubjects { get; set; }

    }
}
