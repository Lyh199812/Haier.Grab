   M S B A M L   `   `   `  ����S  OBase.Clent.HVisionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views,Base.Clent.HVisionModule.Preprocessing.Views  5      T Phalcondotnet, Version=22.5.0.0, Culture=neutral, PublicKeyToken=4973bed59ddbf2b8A0clr-namespace:HalconDotNet;assembly=HalconDotNetHalconDotNet 5      M IHVisionLibs.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulleEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.SharedHVisionLibs.Shared.Controls 5
      =    7Base.Clent.HVisionModule.Preprocessing.Views.MirrorView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Llocal:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views     5      Chalcon0clr-namespace:HalconDotNet;assembly=HalconDotNet     5      ZcontrolsEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.Shared     5
      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      
���   �
�������  � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $M Gpack://application:,,,/HVisionLibs.Shared;component/Themes/Generic.xaml?�6%   �� 5      $d ^pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   �� $��15q�6   $��0��5       .+�5!      �� $��22(�6   $
��Bold"�5"      
���������5#      �� *Service.info.Name+5$      �� 5%      $
��Wrap=�5&      
���������5%      �� *Service.info.Description+5'      5(      �� 5*   
   $��1��6   
�������$��0,1,0,0q�5,      $��0,0,0,15q�5-      �� 5/   
   $��1��6   .@�50      �� $��开始使用��6   .��51      �  � ColumnDefinitions 52      �� 53      $
��0.3* �6+   �� 54      .+�58      �  59      �� 5:      �� 5;      $
��auto �6,   .+�5>      �� $��Hidden=�6'   .��5?      �� $	��5,0q�6)   .+�5@   "   9� $��	图像源��6+   
���������5B   &   �� 5C   *   $��0,5,5,5q�65   .+�5D   .   �� $��加载图像��65   $��0,0,0,5q�6D   ��5E   1   �� *LoadImageCommand+5G   +   5I   #   9� 5K   "   $��运行参数��6+   
������� � Cursor$  ��5L   :   ��5M   &   �� 5N   *   .+�5O   .   �� $
��Mode��68   �� 5P   .   $��0,5,5,5q�67   $��5q�5Q   2    "  MaterialDesignOutlinedComboBox#��[  5S   2   
������� �� Text 5R   2   �� *Service.RunParameter.Mode+.@�5U   2   �� .��6?   column6G   �� 5V   2   .��6?   
diagonal6I   �� 5W   2   .��6?   row6D   5X   /   5Y   +   5[   #   5\      5]      �� 5_      $��1��6%   $��0,5,5,5q�5`   !   .+�5a      �� $��执行��6%   $	��0,5q�62   ��5b   !   �� *
RunCommand+�� 5c      $��保存图像��6%   ��5d   %   �� *SaveImageCommand+5e      5f      � 5h      $��1��6    5i      �� 5j      �� 5k      $
��auto �6,   .+�5m      /  )HVisionLibs.Shared.Controls.ImageEditView     Image 5n      �� *Image+   HWindow 5o      �� *Service.HWindow+	 �� Mode$	 TwoWay=�
   DrawObjectList
 5p      �� *DrawObjectList+�� 5q      $��2��6$   $��5q�5r       ��5s       �� *OperateResult.Message+5t      5u      5v      �� 5w      $��参数说明��6   .��5x      �� $��Hidden=�6   .��5y      �� $
��15,5q�6#   ��5z      �� *Service.info.Parmeters+��5{      �� 5|      .R�5}   "   �� 5~      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
�������$
��Bold"�5�   ,   $��14(�5�   ,   ��5�   0   �� *Name+5�   ,   �� 5�   &   
�������$��20,5,0,0q�5�   ,   ��5�   0   �� *Description+5�   ,   5�   #   5�      5�      5�      5�      �� 5�      $��关联算子��6   .��5�      �� $
��Auto=�6   .��5�      �� $
��15,5q�6#   ��5�      �� *Service.info.Predecessors+��5�      �� 5�      .R�5�   "   �� 5�      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
���������60   �� 5�   ,   5�   #   5�      5�      5�      5�      5�      5�      5�      