   M S B A M L   `   `   `  ����I  EProject-phase2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Project_phase2Project_phase2  5   	       Project_phase2.AddCourse   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Project_phase2     5   	     � Title$  	AddCourse��5   	   $��649.454��6   $��1059.563��6,   .��5	      � $��0,0,38.2,31.8q�6   .+�5
   
   �� $	��ID:��6   $
��Left=�6   $��	10,10,0,0q�69   $	��Top=�6L   $��84��6d   $��33��6o   �� 5   
   $��Department:��6   $
��Left=�6&   $��	10,48,0,0q�6A   $	��Top=�6T   $	��102��6l   �� 5   
   $��Number:��6   $
��Left=�6"   $��	10,91,0,0q�6=   $	��Top=�6P   $	��102��6h   �� 5   
   $��Name:��6   $
��Left=�6    $��
10,134,0,0q�6;   $	��Top=�6O   $	��102��6g   �� 5   
   $��Description:��6   $
��Left=�6'   $��
10,179,0,0q�6B   $	��Top=�6V   $	��102��6n   �� 5   
   $��Prerequisite:��6   $
��Left=�6(   $��
10,222,0,0q�6C   $	��Top=�6W   $	��102��6o   �� 5   
   $��Instructor first name:��6   $
��Left=�61   $��
10,290,0,0q�6L   $	��Top=�6`   $	��119��6x   $��34��6�   �� 5   
   $��Status:��6   $
��Left=�6"   $��
10,413,0,0q�6=   $	��Top=�6Q   $	��102��6i   �� 5   
   $��
Class Size��6   $
��Left=�6%   $��
10,447,0,0q�6@   $	��Top=�6T   $	��102��6l   �� 5   
   $��	Enrolled:��6   $
��Left=�6$   $��
10,486,0,0q�6?   $	��Top=�6S   $	��102��6k   �� 5   
   $��	Semester:��6   $
��Left=�6$   $��
449,17,0,0q�6?   $	��Top=�6S   $	��102��6k   �� 5   
   $��Year:��6   $
��Left=�6    $��
449,56,0,0q�6;   $	��Top=�6O   $	��102��6g   �� 5   
   $��Section:��6   $
��Left=�6#   $��449,107,0,0q�6>   $	��Top=�6S   $	��102��6k   �� 5   
   $��Credit Hours:��6   $
��Left=�6(   $��449,148,0,0q�6C   $	��Top=�6X   $	��102��6p   �� 5   
   $��Class Days:��6   $
��Left=�6&   $��449,195,0,0q�6A   $	��Top=�6V   $	��102��6n   �� 5   
   $��Class Time:��6   $
��Left=�6&   $��449,247,0,0q�6A   $	��Top=�6V   $	��102��6n   �� 5   
   $��Building and Room number:��6   $
��Left=�64   $��449,290,0,0q�6O   $	��Top=�6d   $	��169��6|   �� 5   
   -   5         ��Name$ AddCoursebtn��5      $	��Add��6'   $
��Left=�65   $��
10,530,0,0q�6P   $	��Top=�6d   $	��119��6|   $��37��6�   6�   �� 5   
   -   5         ��Name$ CourseID��5      $
��Left=�6$   $��22��6?   $��	54,10,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��101��6�   �� 5   
   -   5        $ 	CourseDpt��5      $
��Left=�6%   $��34��6@   $��	99,48,0,0q�6L   $
 Wrap=�6_   $	��Top=�6s   $	��119��6�   �� 5   
   -   5        $ 	CourseNum��5      $
��Left=�6%   $��34��6@   $��	99,91,0,0q�6L   $
 Wrap=�6_   $	��Top=�6s   $	��119��6�   �� 5   
   -   5        $ 
CourseName��5      $
��Left=�6&   $��34��6A   $��
99,134,0,0q�6M   $
 Wrap=�6a   $	��Top=�6u   $	��119��6�   �� 5    
   -   5        $ CourseDscrpt��5       $
��Left=�6(   $��34��6C   $��
99,179,0,0q�6O   $
 Wrap=�6c   $	��Top=�6w   $	��270��6�   �� 5!   
   -   5        $ 	CoursePre��5!      $
��Left=�6%   $��34��6@   $��
99,222,0,0q�6L   $
 Wrap=�6`   $	��Top=�6t   $	��119��6�   �� 5"   
   -   5        $ CourseInstID��5"      $
��Left=�6(   $��34��6C   $��160,290,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��119��6�    J� RenderTransformOrigin$ 0.597,0.579&�6�   �� 5#   
   -	   5        $ CourseStatus��5#      $
��Left=�6(   $��34��6C   $��
99,400,0,0q�6O   $
 Wrap=�6c   $	��Top=�6w   $	��119��6�   �� 5$   
   -
   5        $ CourseClassSize��5$      $
��Left=�6+   $��34��6F   $��
99,439,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��119��6�   �� 5%   
   -   5        $ CourseEnrolled��5%      $
��Left=�6*   $��34��6E   $��
99,478,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $	��119��6�   �� 5&   
   -   5        $ 	CourseSem��5&      $
��Left=�6%   $��34��6@   $��	539,9,0,0q�6L   $
 Wrap=�6_   $	��Top=�6s   $	��119��6�   �� 5'   
   -   5        $ 
CourseYear��5'      $
��Left=�6&   $��34��6A   $��
539,56,0,0q�6M   $
 Wrap=�6a   $	��Top=�6u   $	��119��6�   �� 5(   
   -   5        $ CourseSection��5(      $
��Left=�6)   $��34��6D   $��539,103,0,0q�6P   $
 Wrap=�6e   $	��Top=�6y   $	��119��6�   �� 5)   
   -   5        $ CourseCreditHrs��5)      $
��Left=�6+   $��34��6F   $��539,148,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��119��6�   �� 5*   
   -   5        $ CourseClassDays��5*      $
��Left=�6+   $��34��6F   $��539,196,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��119��6�   �� 5+   
   -   5        $ CourseClassTime��5+      $
��Left=�6+   $��34��6F   $��539,239,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��119��6�   �� 5,   
   -   5        $ CourseRoomNum��5,      $
��Left=�6)   $��34��6D   $��618,290,0,0q�6P   $
 Wrap=�6e   $	��Top=�6y   $	��119��6�   �� 5-   
   -   5        $ saveCoursebtn��5-      $��Save Course��6(   $
��Left=�6>   $��160,530,0,0q�6Y   $	��Top=�6n   $	��122��6�   $��37��6�   6�   �� 5.   
   $��Instructor Last name:��6   $
��Left=�60   $��
10,336,0,0q�6K   $	��Top=�6_   $	��119��6w   $��34��6�   �� 5/   
   -   5        $ CourseLastname��5/      $
��Left=�6*   $��34��6E   $��160,336,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��119��6�   $ 0.597,0.579&�6�   �� 50   
   -   5        $��Delete by ID��50      $
��Left=�6(   $��322,530,0,0q�6C   $	��Top=�6X   $	��132��6p   $��37��6|   6�   52      53      