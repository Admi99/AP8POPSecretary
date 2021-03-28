using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{
    public enum WorkingWeightTypes
    {
        Lecture,
        Practise,
        Seminare,
        LectureEng,
        PractiseEng,
        SeminareEng,
        Credit,
        ClassifiedCredit,
        Exam,
        CreditEng,
        ClassifiedCreditEng,
        ExamEng
    };
    public class WorkingPointsWeight : Entity
    {
        public WorkingWeightTypes WorkingWeightTypes { get; set; }
        public double Value { get; set; }
    }
}
