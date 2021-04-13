using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{
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
        public double EmploymentPoints { get; set; }
        public EventType EventType { get; set; }
        public int HoursCount { get; set; }
        public int WeekCount { get; set; }
        public string Language { get; set; }
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        

    }
}
