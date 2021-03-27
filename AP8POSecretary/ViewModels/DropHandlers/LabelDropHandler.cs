using AP8POSecretary.Domain.Entities;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace AP8POSecretary.ViewModels.DropHandlers
{
    public class LabelDropHandler : IDropTarget
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
            dropInfo.Effects = DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            
        }
    }
}
