using AP8POSecretary.Domain.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AP8POSecretary.EPPlus
{
    public class EmployeeSpreadSheetGenerator
    {
        public EmployeeSpreadSheetGenerator()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void GenerateEmployeeSpreadSheet(Employee employee, string toFolder)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("MySheet");
                ws.Cells["A1"].Value = "Employee: " + employee.WholeName;
                ws.Cells["A2"].Value = "School email: " + employee.Email;
                ws.Cells["A3"].Value = "Personal email: " + employee.PersonalEmail;
                ws.Cells["A4"].Value = "Phone number: " + employee.PhoneNumber;
                ws.Cells["A5"].Value = "Is doctorant: " + employee.isDoctorant;
                ws.Cells["A6"].Value = "Commitment rate: " + employee.CommitmentRate;
                ws.Cells["A7"].Value = "Working points: " + Math.Round(employee.WorkingPoints, 2);
                ws.Cells["A7"].Style.Font.Bold = true;
                ws.Cells["D7"].Value = "Working points with eng: " + Math.Round(employee.WorkingPointsWithEng);
                ws.Cells["D7"].Style.Font.Bold = true;

                ws.Cells["A9"].Value = "Working labels: ";
                ws.Cells["H9"].Value = "Working label subjects: ";
                int index = 11;
                foreach (var item in employee.WorkingLabels)
                {
                    if(item.Subject == null)
                    {
                        item.Subject = SpecialEvent();
                    }

                    ws.Cells["A" + index].Value = "Name: " + item.Name;

                    ws.Cells["H" + index].Value = "Shortcut: " + item.Subject.Shortcut;
                    ws.Cells["J" + index++].Value = "Name: " + item.Subject.Name;

                    ws.Cells["A" + index].Value = "Language: " + item.Language;

                    ws.Cells["H" + index++].Value = "Language: " + item.Subject.Language;

                    ws.Cells["A" + index].Value = "Event type: " + item.EventType.ToString();

                    ws.Cells["H" + index++].Value = "Completation type: " + item.Subject.CompletionType.ToString();

                    ws.Cells["A" + index].Value = "Hours: " + item.HoursCount;

                    ws.Cells["H" + index].Value = "Lectures: " + item.Subject.LectureCount;
                    ws.Cells["J" + index].Value = "Practises: " + item.Subject.PractiseCount;
                    ws.Cells["L" + index].Value = "Seminared: " + item.Subject.SeminareCount;

                    ws.Cells["C" + index++].Value = "Weeks: " + item.WeekCount;

                    ws.Cells["A" + index].Value = "Students: " + item.StudentsCount;
                    ws.Cells["C" + index].Value = "Employment points: " + item.EmploymentPoints;

                    ws.Cells["H" + index].Value = "Credits: " + item.Subject.Credit;
                    ws.Cells["J" + index].Value = "Weeks: " + item.Subject.WeeksCount;
                    ws.Cells["L" + index].Value = "Class size: " + item.Subject.ClassSize;


                    ws.Cells["C" + index++].Style.Font.Bold = true;
                    ++index;
                }
                p.SaveAs(new FileInfo(toFolder + @"\" + employee.Id + employee.FirstName + employee.LastName + ".xlsx"));
            }
        }

        private Subject SpecialEvent() => new Subject()
        {
            Name = "Special event",
            Shortcut = "Se",
            ClassSize = 0,
            WeeksCount = 0,
            CompletionType = CompletionType.EXAM,
            LectureCount = 0,
            SeminareCount = 0,
            PractiseCount = 0,
            Credit = 0,
            Language = SubjectLanguage.CZECH
        };
    }
}
