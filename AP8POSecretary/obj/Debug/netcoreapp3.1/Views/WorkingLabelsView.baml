   M S B A M L   `   `   `  ����I  EAP8POSecretary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:AP8POSecretary.ViewsAP8POSecretary.Views  5	      ;"clr-namespace:AP8POSecretary.UtilsAP8POSecretary.Utils  5      ,    &AP8POSecretary.Views.WorkingLabelsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      a ]GongSolutions.WPF.DragDrop, Version=2.0.0.0, Culture=neutral, PublicKeyToken=91f1945125b7a587ddurn:gong-wpf-dragdrop  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      .local"clr-namespace:AP8POSecretary.Views  5	      .proxy"clr-namespace:AP8POSecretary.Utils  5      .��5      � 
�������  � ColumnDefinitions  5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��5* �6   �� 5      $
��588* �6   �� 5      $��7* �6   .+�5   
   �� -   5        $��Test1��5      $��8,8,8,8q�5      $��1��5       � 	Resources 5      '   !AP8POSecretary.Utils.BindingProxy 	  proxy   Data��%*   &	          5       5   %   �� 5       ��5!      ]� 5"      .+�5#      �� 
�������$
��Bold"�5$       Employee card area6F   �� 5%      $��Right=�6!    �� Orientation$ 
Horizontal=�5&   !   .+�5'      ��  $  MaterialDesignRaisedAccentButton#��[ 6!   $��0 0 5 0q�5(   !   $��25��5)   !   $
��Save��5+   !   ��5*   !   �� *SaveEmployees+5+   !   �� 5,      #��[ 6!   $��25��5-   !   
���  ��
���  ��$��Return labels��51   !   ��50   !   �� *	ReturnLab+51   !   52      53      .��55      �� .��56      �� ��6   �� *	Employees+��57      �� 58      .R�59      � 5:      ��5<      �� 5=      .R�5>      #  MaterialDesignThemes.Wpf.Card  ���#03a9f4 ! PrimaryHueDarkForegroundBrush#���  5?   2   $��0q�5@   2   $��5q�5A   2   )  #GongSolutions.Wpf.DragDrop.DragDrop   IsDropTarget$
 true��5B   2   $	��170��5D   2   $	��185��5E   2      DropHandler 5C   2   �� *DataContext.LabelDropHandler+ �� RelativeSource ��  �� Mode$ FindAncestor=�	 �� AncestorType"	 ��.��5F   "   �  5G   &   �� 5H   *   $
��Auto �68   �� 5I   *   $
��Auto �68   �� 5J   *   $
��Auto �68   .+�5L   &   �� $��0��60   $��
16 16 16 4q�5M   0    $  MaterialDesignHeadline5TextBlock#��[ 5N   0   ��5O   0   �� *	WholeName+�� 5P   &   $��1��60      MaterialDesignLightSeparator#��[ 5Q   0   �� 5R   &   $��2��61   $ Vertical=�5S   1   .+�5T   *   �� $��	16 0 16 0q�64   $��Center=�5U   4   $
��Left=�5V   4      MaterialDesignBody2TextBlock#��[ 5W   4   ��5X   4   �� *Email+�� 5Y   *   $��16(�64   $��16 0 3 0q�5Z   4   ��5[   4   �� *PhoneNumber+�� 5\   *   $��16(�64   $��15 0 0 0q�5]   4   ��5^   4   �� *WorkingPoints+
 �� StringFormat$
 {0:#,#.00} - points��]� 5_   *   .+�5`   .   �� $��16(�68   $��15 0 0 0q�5a   8   ��5b   8   �� *WorkingLabels+$
 
{0} labels�� ! ListOfLabelsToLengthConvertor �� 	Converter# [ �� 5c   .    $  MaterialDesignFlatAccentBgButton#��[ 65    � ToolTip$. (MaterialDesignFlatAccentBackgroundButton��5d   5   )  #MaterialDesignThemes.Wpf.DialogHost   OpenDialogCommand#��Z 5e   5   $��70��5f   5   $��3q�5g   5   $��28��5h   5   $
��Show��5i   5   ��5j   2   �� 5k   6   $��16q�6A   .+�5l   :   �� ��6C   �� *WorkingLabels+ �� View 5m   >   �� 5n   B   .P�5o   F   �� $
��Name��5p   U    �� DisplayMemberBinding 5o   U   �� *Name+5p   U   �� 5q   F   $��Language��5r   U    5q   U   �� *
Language+5r   U   �� 5s   F   $��Students��5t   U    5s   U   �� *StudentsCount+5t   U   �� 5u   F   $��Employement points��5v   U    5u   U   �� *EmploymentPoints+5v   U   �� 5w   F    �� CellTemplate 5x   J   �� 5y   N   .R�5z   R   �� $ 
Horizontal=�6]   .+�5{   V   �� $��Return to the pool:��6`   $��0,8,5,0q�5|   `   $��Medium"�5}   `   	� 5~   V   $
��Move��6b   $��70��5   b    d� 	IsChecked$ FalseY�5�   b   $ Move label back to pool��5�   b    $  MaterialDesignActionToggleButton#��[ 5�   b   ��5�   b   �� *Data.ReturnOneLab+ �� Source# [  ��5�   b   �� *Id+5�   b   5�   S   5�   O   5�   G   5�   C   5�   ;   �� 5�   :    	 MaterialDesignFlatButton#��[	 6A    �� IsCancel$
 True��5�   A      CloseDialogCommand#��Z 5�   A   $��Sample2Cancel��5�   A   $��Center=�5�   A   $��CANCEL��5�   A   5�   7   5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�   
   $��1��6   $��1��5�      $��8q�5�       5�      ��%*   &	          5�       5�   %   �� 5�      ��5�      ]� 5�      .+�5�      �� $��25��6    
�������$
��Bold"�5�       Labels list area6D   � 5�      $��Right=�6     � Orientation$ 
Horizontal=�5�       .+�5�      �� #��[ 6!   $��25��5�   !   $��0 0 5 0q�5�   !   
����ح�$��Generate labels��5�   !   ��5�   !   �� *GenerateLabels+5�   !   �� 5�      #��[ 6!   $��25��5�   !   $��0 0 5 0q�5�   !   
����ح�$��Regenerate labels��5�   !   ��5�   !   �� *RegenerateLabels+5�   !   �� 5�      #��[ 6!   $. (MaterialDesignFlatAccentBackgroundButton��5�   !   #��Z 5�   !   $��25��5�   !   $��0 5 5 0q�5�   !   $	��130��5�   !   
����ح�$��Right=�5�   !   $��Add new label��5�   !   ��5�      �� 5�   "   $��16q�6-   .+�5�   &   �� $��1��6/   $��Add new working label��5�   /   $��10,10,10,10q�5�   /   .��5�   *   �� .+�5�   .   � $ Vertical=�68   $��90��5�   8   .+�5�   2   �� $��	10 0 10 0q�6:   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Select label name��5�   :    %
 !MaterialDesignFloatingHintTextBox#��[
 5�   :   $��Center=�5�   :   ��5�   :   �� *Name+5�   :   �� 5�   2   $��
10 12 10 0q�6;   $	��125��5�   ;   $
��Left=�5�   ;   $ Czech��5�   ;    �� SelectedItem 5�   ;   �� *
Language+  ComboboxItemToLanguageEnum# [  �� UpdateSourceTrigger$ PropertyChanged=�.@�5�   6   �� $��Czech��6C   �� 5�   6   $��English��6C   5�   3   �� 5�   2   -   5        $��	10 0 10 0q�5�   :   $ Employment points��5�   :   #��[
 5�   :   $��Center=�5�   :   $	��125��5�   :   ��5�   :   �� *EmploymentPoints+�� 5�   2   -   5        $��	10 0 10 0q�5�   :   $ Students��5�   :   #��[
 5�   :   $��Center=�5�   :   ��5�   :   �� *StudentsCount+�� 5�   2   -   5        $��	10 0 10 0q�5�   :   $ Weeks��5�   :   #��[
 5�   :   $��Center=�5�   :   ��5�   :   �� *	WeekCount+�� 5�   2   -   5        $��	10 0 10 0q�5�   :   $ Hours��5�   :   #��[
 5�   :   $��Center=�5�   :   ��5�   :   �� *
HoursCount+�� 5�   2   $��
10 12 10 0q�6;   $	��125��5�   ;   $
��Left=�5�   ;   $ Lecture��5�   ;    5�   ;   �� *	EventType+  ComboboxItemToEventType# [ $ PropertyChanged=�.@�5�   6   �� $��Lecture��6C   �� 5�   6   $��Seminare��6C   �� 5�   6   $��Practise��6C   �� 5�   6   $
��Exam��6C   �� 5�   6   $��Credit��6C   �� 5�   6   $��Classified credit��6C   5�   3   5�   /   �� 5�   .     MaterialDesignRaisedButton#��[ 65   3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�5�   5      IsIndicatorVisible$
 True��5�   5   $	��Add��5  5   $��10q�5  5   ��5   5   �� *Data.AddLabel+# [  5  5   5  +   5  '   �� 5  &   #��[	 6-   $
 True��5  -   #��Z 5  -   $��Sample2Cancel��5  -   $��Center=�5	  -   $��CANCEL��5
  -   5  #   5     �� 5     #��[ 6!   $��25��5  !   $	��145��5  !   $��0 5 0 0q�5  !   
���  ��
���  ��$��
Delete all��5  !   ��5  !   �� *DeleteAllLabels+5  !   5     5     .��5     �� .��5     ��    IsDragSource$
 true��5     $
 true��5     ��5     �� *WorkingLabels+��5     �� 5     .R�5      � 5!     ��5#     �� 5$     .R�5%       ���#03a9f4#���  5&  2   $��0q�5'  2   $��5q�5(  2   $	��170��5)  2   $	��185��5*  2   .��5+  "   �  5,  &   �� 5-  *   $
��Auto �68   �� 5.  *   $
��Auto �68   �� 5/  *   $
��Auto �68   .+�51  &   �� $��0��60   $��	16 8 16 4q�52  0    $  MaterialDesignHeadline6TextBlock#��[ 53  0   $��15(�54  0   $��40��55  0   $
��Wrap=�56  0   ��57  0   �� *Name+�� 58  &   $��1��60   #��[ 59  0   �� 5:  &   $��2��61   $ Vertical=�5;  1   .+�5<  *   �� $��	16 0 16 0q�64   $��Center=�5=  4   $
��Left=�5>  4   #��[ 5?  4   ��5@  4   �� *
Language+�� 5A  *   $��16(�64   $��16 0 3 0q�5B  4   ��5C  4   �� *StudentsCount+$
 {0} students��]� 5D  *   .+�5E  .   �� $��16(�68   $��15 0 0 0q�5F  8   ��5G  8   �� *EmploymentPoints+$
 
{0} points���� 5H  .   $��0 0 16 0q�68   $��Center=�5I  8   $��Right=�5J  8   $��NoWrap=�5K  8   $��CharacterEllipsis=�5L  8   #��[ 5M  8   ��5N  8   �� *	EventType+  EnumConverter# [ 5O  +   ]� 5P  *   .+�5Q  .   �� #��[ 65   $. (MaterialDesignFlatAccentBackgroundButton��5R  5   #��Z 5S  5   $��75��5T  5   $��10 5 0 0q�5U  5   $
��Left=�5V  5   $��28��5W  5   $��Details��5X  5   ��5Y  2   �� 5Z  6   $��16q�6A   .+�5\  :   �� $��1��6C   $��Add subject��5]  C   $��10,10,10,10q�5^  C   .��5_  >   �� .+�5`  B   � $ Vertical=�6L   $��90��5a  L   .+�5b  F   �� $��	10 0 10 0q�6N   $ Select label name��5d  N   #��[
 5e  N   $��Center=�5f  N   ��5c  N   �� *Name+5f  N   �� 5g  F   $��
10 12 10 0q�6O   $	��125��5h  O   $
��Left=�5i  O   $ Czech��5k  O    5j  O   �� *
Language+# [ $ PropertyChanged=�.@�5l  J   �� $��Czech��6W   �� 5m  J   $��English��6W   5n  G   �� 5o  F   -   5        $��	10 0 10 0q�5o  N   $ Employment points��5p  N   #��[
 5q  N   $��Center=�5r  N   $	��125��5s  N   ��5u  N   �� *EmploymentPoints+�� 5v  F   -   5        $��	10 0 10 0q�5v  N   $ Students��5w  N   #��[
 5x  N   $��Center=�5y  N   ��5{  N   �� *StudentsCount+�� 5|  F   -   5        $��	10 0 10 0q�5|  N   $ Weeks��5}  N   #��[
 5~  N   $��Center=�5  N   ��5�  N   �� *	WeekCount+�� 5�  F   -	   5        $��	10 0 10 0q�5�  N   $ Hours��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *
HoursCount+5�  C   ]� 5�  B   .+�5�  F   �� #��[ 6M   $ -1Q�5�  M   $
 True��5�  M   $��Update label��5�  M   $
��Left=�5�  M   $��10q�5�  M   ��5�  M   �� *Data.UpdateLabel+# [  ��5�  M   �� *Id+5�  M   �� 5�  F   #��[ 6M   $ -1Q�5�  M   $
 True��5�  M   $��Right=�5�  M   $	��150��5�  M   $��Delete label��5�  M   $��	300 0 0 0q�5�  M   ��5�  M   �� *Data.DeleteSpecificLabel+# [  ��5�  M   �� *Id+5�  M   5�  C   5�  ?   5�  ;   �� 5�  :   #��[	 6A   $
 True��5�  A   #��Z 5�  A   $��Sample2Cancel��5�  A   $��Center=�5�  A   $��CANCEL��5�  A   5�  7   5�  /   �� 5�  .   #��[ 65   $. (MaterialDesignFlatAccentBackgroundButton��5�  5   #��Z 5�  5   $��80��5�  5   $��0 5 10 0q�5�  5   $��Right=�5�  5   $��28��5�  5   $��Subject��5�  5   ��5�  2   �� 5�  6   $��16q�6A   .+�5�  :   �� $��1��6C   $��Subject��5�  C   $��10,10,10,10q�5�  C   .��5�  >   �� .+�5�  B   �� $��MyCombo��5�  K   $ Select subject��5�  K   $	��250��5�  K   $��10 0 0 0q�5�  K   $
��Left=�5�  K      HintOpacity$	 .26Q�5�  K    �� DisplayMemberPath$
 Name��5�  K    �� 
IsEditable$
 True��5�  K   ��5�  K   �� *Data.Subjects+# [  .  (System.Windows.Interactivity.Interaction   Triggers 5�  F   /  )System.Windows.Interactivity.EventTrigger  5�  J       	EventName$  SelectionChanged��6Y   .	  (System.Windows.Interactivity.TriggerBase! 	  Actions.! 5�  N   6
  0System.Windows.Interactivity.InvokeCommandAction
  " 
  Command" 6d   �� * Data.OnSubjectSelectionChanged+# [  # 
  CommandParameter# 5�  d   �� $ �� Path$$ SelectedItem� �� $ FindAncestor=�	 ComboBox5�  K   5�  C   � 5�  B   $ Vertical=�6L   $��90��5�  L   .+�5�  F   �� $��	10 0 10 0q�6N   % �� 
IsReadOnly$
% True��5�  N   $
 Name��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.Name+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ Language��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.Language+# [ 5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ Shortcut��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.Shortcut+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $
 Type��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.CompletionType+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ Lectures��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.LectureCount+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ 	Seminared��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.SeminareCount+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ 	Practeses��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.PractiseCount+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ Credit��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.Credit+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5�  N   $ Weeks��5�  N   #��[
 5�  N   $��Center=�5�  N   ��5�  N   �� *Subject.WeeksCount+5�  N   �� 5�  F   $��	10 0 10 0q�6N   $
% True��5   N   $ 
Class size��5  N   #��[
 5  N   $��Center=�5  N   ��5�  N   �� *Subject.ClassSize+5  N   5  C   �� 5  B   #��[ 6I   $ -1Q�5  I   $
 True��5  I   $��Update subject��5
  I   $
��Left=�5  I   $��10q�5  I   ��5  I   �� *Data.OnSubjectChangeForLabels+# [  ��5	  I   �� *Id+5  I   5  ?   5  ;   �� 5  :   #��[	 6A   $
 True��5  A   #��Z 5  A   $��Sample2Cancel��5  A   $��Center=�5  A   $��CANCEL��5  A   5  7   5  /   5  +   5  '   5  #   5     5     5     5     5      5!     5"     