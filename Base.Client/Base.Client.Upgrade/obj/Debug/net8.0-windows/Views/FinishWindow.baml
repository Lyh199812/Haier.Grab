   M S B A M L   `   `   `  ����N  JBase.Client.Upgrade, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Base.Client.Upgrade.ViewsBase.Client.Upgrade.Views  5   	   ,    &Base.Client.Upgrade.Views.FinishWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Base.Client.Upgrade.Views     5   	   $��Microsoft YaHei)�5   	   $��
ExtraLight"�6&   $��14(�6>   ���#333  � WindowStartupLocation$  CenterOwner=�5	   	   
������  � WindowStyle$
 None=�6F    � 
ResizeMode$ NoResize=�5
   	    � Title$ 更新完成��5   	   $	��200��6   $	��350��6#   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ -1q�6   .��5      � ���#F7F9FA � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      �� 5      $��40 �6   .+�5   
   �� 
�������6   �� 5   
   
�������$��2��6$   �� 5   
   $��更新完成��6   $��Center=�6    $��Center=�6;   $��16(�6X   �� 5   
   $��Center=�6   $
��20,0q�60   $��1��6>   .+�5      �� $G��A所有文件更新已完成！点击[确认]退出更新程序。��6   $��	0,10,0,20q�67   �� 5      $$��退出时立即启动主程序��6   $��12(�6,   $��Center=�6V    d� 	IsChecked 6:   �� *IsRun+6V   5      �� 5   
   $��确认��6   $��2��6   $��30��6+   $	��100��67   ��6C   �� *ConfirmCommand+5      5      