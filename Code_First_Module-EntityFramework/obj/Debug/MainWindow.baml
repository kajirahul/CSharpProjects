   M S B A M L   `   `   `  ����I  EProject-phase2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Project_phase2Project_phase2  5   	       Project_phase2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Project_phase2     5   	     � Title$  
MainWindow��5   	   $��695.916��6   $��1313.908��6-   .��5	      � -   5         �Name$ 	addCourse��5	      $��0,0,25.6,11.8q�6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgrid��5
      $
��Left=�6"   $	��454��6=   $��
10,114,0,0q�6J   $	��Top=�6^   $
��1029��6v      
IsReadOnly .    AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6<   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *ID+6<     5      $ 
Department��6D    6%   �� *
Department+6D     5      $ Number��6@    6%   �� *Number+6@     5      $
 Name��6>    6%   �� *Name+6>     5      $ Description��6E    6%   �� *Description+6E     5      $ Prerequisite��6F    6%   �� *Prerequisite+6F     5      $ 
Instructor��6E    6%   �� *Instructors+6E     5      $ Status��6@    6%   �� *Status+6@     5      $ 
Class Size��6C    6%   �� *	ClassSize+6C     5      $ Enrolled��6B    6%   �� *
Enrolled+6B     5      $ Semester��6B    6%   �� *
Semester+6B     5      $
 Year��6>    6%   �� *Year+6>     5      $ Section��6A    6%   �� *	Section+6A     5      $ Credit Hours��6D    6%   �� *	CreditHrs+6D     5      $ 
Class Days��6C    6%   �� *	ClassDays+6C     5      $ 
Class Time��6>    6%   �� *Time+6>     5      $ Building and Room no.��6A    6%   �� *	RoomNum+6A   5      �� 5   
   -   5         ��Name$ 
addCourse1��5      $&�� Add new course / Update / Delete��6%   $
��Left=�6P   $��
28,598,0,0q�6k   $	��Top=�6   $	��322��6�   $��36��6�   6�   �� 5    
   -   5        $ addInstructor��5       $*��$Add new instructor / Update / Delete��6(   $
��Left=�6W   $��398,598,0,0q�6r   $	��Top=�6�   $	��282��6�   $��36��6�   6�   �� 5!   
   -   5        	 ��Name$	 	searchtxt��5!      $
��Left=�6%   $��75��6@   $��	10,10,0,0q�6L   
 �� TextWrapping$

 Wrap=�6_   $	��Top=�6s   $	��395��6�   �� 5"   
   -   5        $ 	searchbtn��5"      $��Search��6$   $
��Left=�65   $��
440,29,0,0q�6P   $	��Top=�6d   $	��133��6|   $��39��6�   6�   5$      5%      