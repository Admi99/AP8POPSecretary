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
    public class CardDropHandler : IDropTarget 
    {

        public ObservableCollection<Group> Groups { get; set; }

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
            dropInfo.Effects = DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var fromSubject = dropInfo.Data as Subject; 
            var card = dropInfo.VisualTarget as Card;
            var toGroup = card.DataContext as Group;

            var isSame = toGroup.GroupSubjects.Where(item => item.Subject.Id == fromSubject.Id);
            if (isSame.Count() == 0)
            {
                var index = Groups.IndexOf(toGroup);
                var group = Groups.ElementAt(index);

                var newGroupSub = new GroupSubject() { Subject = fromSubject };
                if (group.GroupSubjects == null)
                {
                    group.GroupSubjects = new List<GroupSubject>();
                    group.GroupSubjects.Add(newGroupSub);
                }
                else group.GroupSubjects.Add(newGroupSub);

                Groups.RemoveAt(index);
                Groups.Insert(index, group);
            }                  
        }
    }
}
