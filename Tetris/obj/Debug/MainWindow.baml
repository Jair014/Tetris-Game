   M S B A M L   `   `   `  ����A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris  5   	       Tetris.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Tetris     5   	     � Title$  
MainWindow��5   	   $	��800��6   $	��950��6)    � 
ResizeMode$ CanMinimize=�65   .��5	      �� -   5         ��Name$ 
HomeScreen��5	      $
��Left=�6!   $	��642��6<   $	��Top=�6I   $	��938��6a   
�������$��0,58,0,0q�6�   .+�5
   
   �� -   5         ��Name$ 
TetrisLogo��5
      $	��155��6$    �� Left$	 226��61   $��tetrislogo.jpg��6C   $
��Fill=�6[   
 �� Top$ 10��6j   $	��457��6z   �� 5   
   -   5         ��Name$ StartButton��5      $��Start��6&   $	 185��66   $	��207��6G   $��63��6S   $��36(�6_   $��Rockwell Extra Bold)�6m   ���	#FF28E05A$
��Left=�6�   $	��Top=�6�   $	 342��6�   �� 5   
   -   5        $ AboutButton��5      $��About��6&   $	 278��66   $	��207��6G   $��63��6S   $��36(�6_   $��Rockwell Extra Bold)�6m   ���	#FF509CF7$��Center=�6�   $	 342��6�   �� 5   
   -   5        $ RedBlock��5      $	��133��6"   $��
Image1.png��6/   $
��Fill=�6C   $	��203��6R   $	 668��6^   $	 181��6p    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.083Q�6&   T� 5      5      5      �� 5   
   -   5        $ PurpleBlock��5      $	��157��6%   $��
Image2.png��62   $
��Fill=�6F   $	��205��6U   $	 677��6a   $	 437��6s   �� 5   
   -   5        $ 	BlueBlock��5      $	��245��6#   $��
Image3.png��60   $
��Fill=�6D   $	��150��6S   $	 361��6_   $	 498��6q   $ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5      $ 89.876Q�6&   T� 5      5      5!      �� 5"   
   -   5        $ LightBlueBlock��5"      $	��237��6(   $��
Image5.png��65   $
��Fill=�6I   $��93��6X   $ 63��6c   �� 5#   
   -	   5        $ 
GreenBlock��5#      $	��213��6$   $ 63��61   $ ��image-removebg-preview.png��6B   $
��Fill=�6f   $	 285��6u   $	��161��6�   �� 5$   
   -
   5        $ YellowBlock��5$      $	��134��6%   $��
Image7.png��62   $
��Fill=�6F   $	��140��6U   $	 732��6a   $	 -21��6s   �� 5%   
   -   5        $ 
ExitButton��5%      $
��Exit��6%   $	 368��64   $	��207��6E   $��63��6Q   $��36(�6]   $��Rockwell Extra Bold)�6k   ���	#FFF94B4B$
��Left=�6�   $	��Top=�6�   $	 342��6�   �� 5&   
   -   5        $ SettingsButton��5&      $��Settings��6)   $	��207��6<   $��63��6H   $
��Left=�6T   $	��Top=�6o   $��Rockwell Extra Bold)�6�   $��36(�6�   ���	#FF8A8B8A$	 342��6�   $	 454��6�   5'      5(      