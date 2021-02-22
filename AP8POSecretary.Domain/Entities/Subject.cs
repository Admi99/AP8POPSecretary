using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{
    public enum CompletionType
    {
        EXAM,
        CLASSIFIED
    };
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public int LectureCount { get; set; }
        public int SeminareCount { get; set; }
        public int PractiseCount { get; set; }
        public int Credit { get; set; }
        public int WeeksCount { get; set; }
        public string Language { get; set; }
        public int ClassSize { get; set; }
        public CompletionType CompletionType { get; set; }

    }
}
