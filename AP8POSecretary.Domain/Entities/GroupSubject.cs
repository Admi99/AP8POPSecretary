using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{
    public class GroupSubject : Entity
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
