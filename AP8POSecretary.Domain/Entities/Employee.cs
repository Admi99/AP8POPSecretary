using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Domain.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WholeName { get; set; }
        public string Email { get; set; }
        public string PersonalEmail { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDoctorant { get; set; }
        public double CommitmentRate { get; set; }
        public IList<WorkingLabel> WorkingLabels { get; set; }
    }
}
