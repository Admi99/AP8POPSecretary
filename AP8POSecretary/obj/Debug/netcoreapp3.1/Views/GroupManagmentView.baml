   M S B A M L   `   `   `  ����I  EAP8POSecretary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:AP8POSecretary.ViewsAP8POSecretary.Views  5      -    'AP8POSecretary.Views.GroupManagmentView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .local"clr-namespace:AP8POSecretary.Views  5      a ]GongSolutions.WPF.DragDrop, Version=2.0.0.0, Culture=neutral, PublicKeyToken=91f1945125b7a587ddurn:gong-wpf-dragdrop  5      .��5      � 
�������  � ColumnDefinitions  5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��5* �6   �� 5      $
��588* �6   �� 5      $��7* �6   .+�5   
   �� $��8,8,8,8q�5      $��1��5      ��5      ]� 5      .+�5      �� 
�������$
��Bold"�63   Groups card area6W   �� 5      $��Right=�6!    �� Orientation$ 
Horizontal=�6=   .+�5      ��  $   MaterialDesignRaisedAccentButton#��[  6!   $��0 0 5 0q�5   !   $��25��5    !   $
��Save��5"   !   ��5!   !   �� *SaveSubjects+5"   !   �� 5#      #��[  6!   $��25��5$   !   $��0 0 5 0q�5%   !   
����ح�$��Revert��5(   !   ��5'   !   �� *RevertSubjects+5(   !   �� 5)      #��[  6!   $��25��5*   !   
���  ��
���  ��$��Delete subjects��5.   !   ��5-   !   �� *DeleteGroupSubjects+5.   !   5/      52      .��54      �� .��55      �� ��6   �� *Groups+��56      �� 57      .R�58      � 59      ��5;      �� 5<      .R�5=      #  MaterialDesignThemes.Wpf.Card  ���#03a9f4 ! PrimaryHueDarkForegroundBrush#���  5>   2   $��0q�5?   2   $��5q�5@   2   )  #GongSolutions.Wpf.DragDrop.DragDrop   DragDropContext$ 10��5A   2      IsDropTarget$
 true��5B   2   $	��150��5D   2   $	��185��5E   2      DropHandler 5C   2   �� *DataContext.CardDropHandler+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" ��.��5F   "   �  5G   &   �� 5H   *   $
��Auto �68   �� 5I   *   $
��Auto �68   �� 5J   *   $
��Auto �68   .+�5L   &   �� $��0��60   $��
16 16 16 4q�5M   0    $  MaterialDesignHeadline5TextBlock#��[ 5N   0   ��5O   0   �� *
Shortcut+�� 5P   &   $��1��60      MaterialDesignLightSeparator#��[ 5Q   0   �� 5R   &   $��2��61   $ Vertical=�5S   1   .+�5T   *   ]� .+�5U   .   �� $��	16 0 16 0q�68   $��Center=�5V   8   $
��Left=�5W   8      MaterialDesignBody2TextBlock#��[ 5X   8   ��5Y   8   �� *
Language+�� 5Z   .   $��Right=�68   $��16(�5[   8   $��0 0 15 0q�5\   8   ��5]   8   �� *	StudyYear+	 �� StringFormat$	 
{0}st year��5^   +   �� 5_   *   $��16(�64   $��15 0 0 0q�5`   4   ��5a   4   �� *StudentsCount+$	 {0} students��]� 5b   *   .+�5c   .   �� $��16(�68   $��15 0 0 0q�5d   8   ��5e   8   �� *GroupSubjects+$	 {0} subjects�� # SubjectToSubjectLenghtConverter
 �� 	Converter#
 [ �� 5f   .    $  MaterialDesignFlatAccentBgButton#��[ 65    � ToolTip$. (MaterialDesignFlatAccentBackgroundButton��5g   5   )  #MaterialDesignThemes.Wpf.DialogHost   OpenDialogCommand#��Z 5h   5   $��70��5i   5   $��3q�5j   5   $��28��5k   5   $
��Show��5l   5   ��5m   2   �� 5n   6   $��16q�6A   .+�5o   :   �� ��6C   �� *GroupSubjects+ �� View 5p   >   �� 5q   B   .P�5r   F   �� $
��Name��5s   U    �� DisplayMemberBinding 5r   U   �� *Subject.Name+5s   U   �� 5t   F   $��Shortcut��5u   U    5t   U   �� *Subject.Shortcut+5u   U   �� 5v   F   $��Language��5w   U    5v   U   �� *Subject.Language+5w   U   �� 5x   F   $��
Class size��5y   U    5x   U   �� *Subject.ClassSize+5y   U   5z   C   5|   ;   �� 5}   :     MaterialDesignFlatButton#��[ 6A    �� IsCancel$
 True��5~   A      CloseDialogCommand#��Z 5   A   $��Sample2Cancel��5�   A   $��Center=�5�   A   $��CANCEL��5�   A   5�   7   5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�   
   $��1��6   $��1��6    $��8q�5�      ��5�      �� 5�      $��25��6   
�������$
��Bold"�5�       Subjects list area6F   .��5�      ��    IsDragSource$
 true��5�      $
 true��5�      ��5�      �� *
Subjects+ 5�      �� 5�      .P�5�      �� $
��Name��5�   )    5�   )   �� *Name+5�   )   �� 5�      $��Shortcut��5�   )    5�   )   �� *
Shortcut+5�   )   �� 5�      $��Language��5�   )    5�   )   �� *
Language+5�   )   �� 5�      $��
Class size��5�   )    5�   )   �� *	ClassSize+5�   )   5�      5�      5�      5�      5�      