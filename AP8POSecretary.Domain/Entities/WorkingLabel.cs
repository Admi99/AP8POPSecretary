using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{//přednáška, cvičení, seminář, zápočet, klasifikovaný zápočet zkouška
    public enum EventType
    {
        LECTURE,
        SEMINARE,
        PRACTISE,
        EXAM,
        CREDIT,
        CLASSIFIEDCREDIT
    };
    public class WorkingLabel : Entity
    {
        public string Name { get; set; }
        public int StudentsCount { get; set; }
        public int EmploymentPoints { get; set; }
        public EventType EventType { get; set; }
        public int HoursCount { get; set; }
        public int WeekCount { get; set; }
        public string Language { get; set; }
        public int SubjectId { get; set; }
        public int EmployeeId { get; set; }
        public Subject Subject { get; set; }
        public Employee Employee { get; set; }

    }
}
