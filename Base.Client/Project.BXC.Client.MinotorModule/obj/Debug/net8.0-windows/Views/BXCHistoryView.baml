   M S B A M L   `   `   `  ����S  OBXC.Client.MinotorModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:BXC.Client.MinotorModule.ViewsBXC.Client.MinotorModule.Views  5      3    -BXC.Client.MinotorModule.Views.BXCHistoryView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:BXC.Client.MinotorModule.Views     5      X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .��5
      � .+�5   
   �   � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10q�62   $��0��6>   $
��Left=�6K   .+�5      �� $��开始时间:��6   $��Center=�6(   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ StartDatePicker��5      $	��120��66   $	��5,0q�6B      SelectedDate 6O   �� *	StartDate+ �� Mode$ TwoWay=�"  Xceed.Wpf.Toolkit.TimePicker  5      $��80��6"   $	��5,0q�6-   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]   Value 6:   �� *	StartTime+$ TwoWay=��� 5      $��结束时间:��6   $��Center=�6(     5      -   5        $ EndDatePicker��5      $	��120��64   $	��5,0q�6@    6M   �� *	EndDate+$ TwoWay=�  5      $��80��6"   $	��5,0q�6-    6:   �� *	EndTime+$ TwoWay=��� 5      $	��SN:��6   $��Center=�6&   �� 5      -   5         ��Name$ 	SnTextBox��5      $	��150��6-   $	��5,0q�69   ��6F   �� *SN+$ TwoWay=��� 5      $��查询��6   $��80��6&   $
��10,0q�61   ��6?   �� *SearchCommand+5       &   System.Windows.Controls.DataGrid  5"      -   5          Name$ BxcRecordDataGrid��5"      $��1��5#         AutoGenerateColumns$ False��5$      $��10q�5%      	   
IsReadOnly$	 False��5&      ��5"   2   �� *FilteredRecords+$ OneWay=�
   Columns
 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+6L     5)      $ SN��6)   $ * 6L    65   �� *SN+6L     5*      $ 时间��6)   $ * 6N    65   �� *Time+6N     5+      $ 重量��6)   $
 0.7* 6P    65   �� *Weight+6P     5,      $ 最大重量��6)   $
 0.7* 6U    67   �� *	MaxWeight+6U     5-      $ 最小重量��6)   $
 0.7* 6U    67   �� *	MinWeight+6U     5.      $ 运行模式��6)   $
 0.7* 6P    67   �� *Mode+6P     5/      $ 测试结果��6)   $
 0.5* 6V    67   �� *
TestResult+6V     50      $ 错误信息��6)   $
 1.5* 6U    67   �� *	ErrorInfo+6U     51      $ 
MESSendMsg��6)   $
 1.5* 6\    6=   �� *
MESSendMsg+6\     52      $ 	MESRecMsg��6)   $
 1.5* 6Z    6<   �� *	MESRecMsg+6Z   54      �� 57      $��2��6   $��10q�6&   $ 
Horizontal=�62   .+�58      �� $��导出搜索数据��6   $��30��6J   $	��120��6W   $��16(�6c   ��6*   �� *SaveCommand+6c   �� 59      $!��清空数据-建议1月1清��6   $
��10,0q�6/   $��30��6c   $	��320��6p   $��16(�6|   ��6=   �� *ClearTableCommand+6|   5;      5<      5=      5>      