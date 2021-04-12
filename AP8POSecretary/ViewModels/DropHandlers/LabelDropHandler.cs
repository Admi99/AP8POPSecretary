using AP8POSecretary.Domain.Entities;
using GongSolutions.Wpf.DragDrop;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace AP8POSecretary.ViewModels.DropHandlers
{
    public class LabelDropHandler : IDropTarget
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<WorkingLabel> WorkingLabels { get; set; }
        public IList<WorkingLabel> DeletedWorkingLabels { get; set; }

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
            dropInfo.Effects = DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var fromWorkingLabel = dropInfo.Data as WorkingLabel;
            var card = dropInfo.VisualTarget as Card;
            var toEmployee = card.DataContext as Employee;

            var index = Employees.IndexOf(toEmployee);
            var employee = Employees.ElementAt(index);

            if(fromWorkingLabel.Language == SubjectLanguage.CZECH.ToString())
            {
                employee.WorkingPoints += fromWorkingLabel.EmploymentPoints;
            }

            employee.WorkingPointsWithEng += fromWorkingLabel.EmploymentPoints;
          
            if (employee.WorkingLabels == null)
            {
                employee.WorkingLabels = new List<WorkingLabel>();
                employee.WorkingLabels.Add(fromWorkingLabel);
            }
            else employee.WorkingLabels.Add(fromWorkingLabel);

            Employees.RemoveAt(index);
            Employees.Insert(index, employee);

            WorkingLabels.Remove(fromWorkingLabel);
            DeletedWorkingLabels.Add(fromWorkingLabel);
        }
    }
}
