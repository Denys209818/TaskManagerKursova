   M S B A M L   `   `   `  ����H  DTaskManagerUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TaskManagerUITaskManagerUI  5   	   ;"clr-namespace:TaskManagerUI.ModelsTaskManagerUI.Models  5   	   B >App.Lib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullB.clr-namespace:Apps.Lib.Models;assembly=App.LibApps.Lib.Models 5   	       TaskManagerUI.BoardWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TaskManagerUI     5   	   5models"clr-namespace:TaskManagerUI.Models     5   	   @tasks.clr-namespace:Apps.Lib.Models;assembly=App.Lib     5   	   -   5          � Title$  Робочий стіл��5
   	   $	��450��6   $	��800��6+    � 	Resources 5         btnBoxStyle �� 
TargetType �� Property �� Value  templateTask �� DataType  Apps.Lib.Models.TaskModel
 � Tag �� UpdateSourceTrigger��%c  &	        &	 J     7   �� 5   
   " ��6$   .�5      ��  � ��
 �����6+   5      �� 5   
   "  6,   .R�5      �� -   5        8��  ��5      �� 5      $��1q�6   
����+��$	��450��5   Z   $��70��6f    5   3   �� *Id+$ PropertyChanged=���6k   �� *	Comment+$ PropertyChanged=�.��5      ]� .+�5      �� -   5        $��Center=�5   '   $
��Left=�6I    5   C   �� *Id+5   I   �� 5      
�������$��24(�5   :   $��5q�6H   ��5   (   �� *
TaskText+$ PropertyChanged=�5   H   5      5      5      5      5!      .��5"      � ��5#   
   �� 5$      -   5        imgBrush�� j� Stretch$
 Fill=�5%      	 � RowDefinitions	 5'   
   �� 5(      $��6* �6   �� 5)      $��* �6   .+�5+   
   �� $��0��6   .+�5,      �� $
��0,30q�6   $	��500��6*   $	��300��66   $��Visible=�6C   $��Disabled=�6i   .��5-      �� -   5        $��listBoxTasks��5/      #��[ 5-      
���������5.      ��   	dataTasks
 �� Source#
 [ 50      51      52      � 53   
   $��15q�6   $��1��6    .+�54      �� -   5        $��btnChangeBack��54      $2��,Змінити фоновий малюнок��55   0   $	��0,0q�6R   $��14(�56      $��5q�6#   �� 57      -   5        $��btnAdd��57      $!��Додати елемент��58   *   $
��10,0q�6C   $��14(�59      $��5q�6#   �� 5:      -   5        $��txtCode��5:      $��Код: ��6#   
�������$
��Bold"�6G   $��15(�6Y   $
��20,0q�6g   5;      5<      5=      