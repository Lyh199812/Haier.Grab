   M S B A M L   `   `   `  ����O  KBase.Client.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Base.Client.ControlsBase.Client.Controls  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ProductionInfoCardStyle  �� 
TargetType-    'Base.Client.Controls.ProductionInfoCard �� Property �� Value �� 
TargetType �� Padding �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   	Direction	   ShadowDepth
   Opacity   
BlurRadius � RowDefinitions �� Orientation � Value'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� RepeatBehavior �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingPointKeyFrame $� KeyTime $� Value J� Opacity ��Name �Name � ColumnDefinitions ��Name J� RenderTransformOrigin �� ScaleY �� ScaleX �� AngleY  �� AngleX! �� Angle" (� StretchDirection# J� SnapsToDevicePixels$ �� RelativeSource% �� Path& �� StringFormat�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,local"clr-namespace:Base.Client.Controls  5      %,  &	        �� 5      "    6,   .�5   
   ��  � �� 5      �� 5	      "   6"   .R�5
      �� 
�������$ 16q�60   $ 20��6=   $��12q�6O   ���#cccccc$��1q�6q    5        5      $ #000000��6/   $	 315Q�6?   $	 5Q�6O   $	
 0.2Q�6_   $ 10Q�6m   ._�5      �  5      �� 5   "   $
��1.2* �60   �� 5   "   $
��1.5* �60   �� 5   "   $��* �60   .+�5      �� $ 
Horizontal=�6)   $��0��6B   .+�5   "   �� $��生产数量：��6,   ���#33369   �� 5   "   ���#808080��6,   �� *ProductionCount+6M   �� 5   "   $��不合格数量：��6,   ���#333$��50,0,5,0q�6L   �� 5   "   
���  ����6,   �� *DefectiveCount+6L   5      ]� 5      $��1��6(   .+�5   "   �� $��   合格率：��6,   ���#333$��Center=�6M   � 5   "    6.   �� *
PassRate+��5    &   �� 5!   *   "  �60   .�5"   .   ��  �@�FocusVisualStyle 6Q   Z� �� 5#   .    �@J�SnapsToDevicePixels$
 True��6T   �� 5$   .    � ��$ 15��6G   �� 5%   .    � �� �#6fae5f6K   �� 5&   .    � ��$ 10(�6I   �� 5'   .    � ��$	 5,0q�6H   �� 5(   .    � �� 5)   2   �� 5*   6   " �6F   .R�5+   :   � ���	#00000000 5,   >   �� 5-   B   $
��Auto �6P    5/   >     50   B   CommonStates��. 51   F     Determinate��6R     52   F   Indeterminate��. 53   J   �� $ Forever��6U   .��54   N   *� $ 	Animation��6k   $' !(UIElement.RenderTransformOrigin)�6�   .'�55   R     $ 0:0:0��6f   $ 0.5,0.5&�6v     56   R   $ 0:0:1.5��6f   $ 1.95,0.5&�6x     57   R   $ 0:0:3��6f   $ 0.5,0.5&�6v   58   O   59   K   5:   G   5;   C   .+�5=   >   � #��z��6C   .+�5>   B   �� ���#000000$	 7.5��6^   $
 0.05Q�6q   �� 5?   B   ���#000000$��1q�6_   $	 7.5��6s   $	 0.1Q�6�   � 5@   B   #��z��6G   .+�5A   F   �� $ 
PART_Track��6M   � 5B   F   $ PART_Indicator��6K   $
}�True��6c   $
��Left=�6w    5C   J   �� 5D   N   $ width1��6_   �� 5E   N   $ width2��6_   $��0 �6o   .+�5G   J   � $ 	Animation��6O   $ 0.5,0.5&�6b   z�5H   N   V� 5I   R   .~�5J   V   �� $ -1Q�6e   $ 1Q�6q   �� 5K   V   $ 0Q�6d   $  0Q�6o   �� 5L   V   $	! 180Q�6f   T� 5M   V   5N   S   .+�5P   N   �� #��z��6U   $	 7.5��6   ._�5Q   R   (� $
��Left=�6Z   $" DownOnly=�6u   #��z��6�   $
# True��6�   .��5R   V   �� ���#ffffff$
# True��6u   #��z��6�   $��Center=�6�   $ 0.5,0.5&�61  ��6�   �� $ �� *TemplatedParent+$% Value�$
& {0}%��z�5S   Z   V� 5T   ^   .~�5U   b   �� $ 1Q�6q   $ -1Q�6|   �� 5V   b   $ 0Q�6p   $  0Q�6{   �� 5W   b   $! 0Q�6r   T� 5X   b   5Y   _   5[   W   5\   S   5]   O   �� 5^   N   ���#000000$��1q�6k   $	 7.5��6   $	 0.1Q�6�   5_   K   5`   G   5a   C   5b   ?   5c   ;   5d   7   5f   /   5h   +   5j   #   5l      �� 5n      $ 
Horizontal=�6)   $��2��6B   $��5��6O   .+�5o   "   �� $��更新时间：��6,   ���#33369   �� 5p   "   ���#808080��6,   �� *
UpdateTime+6H   �� 5q   "   $��开始时间：��6,   ���#333$��30,0,0,0q�6K   �� 5r   "   ���#808080��6,   �� *	StartTime+6G   5s      5t      5u      5v      5x      5y      5Z     