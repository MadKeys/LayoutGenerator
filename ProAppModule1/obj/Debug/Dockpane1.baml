   M S B A M L   `   `   `  ����H  DProAppModule1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ProAppModule1ProAppModule1  5      U QArcGIS.Desktop.Extensions, Version=11.4.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:ArcGIS.Desktop.Extensions;assembly=ArcGIS.Desktop.ExtensionsArcGIS.Desktop.Extensions 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics 5      !    ProAppModule1.Dockpane1View   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *uiclr-namespace:ProAppModule1     5      a
extensionsJclr-namespace:ArcGIS.Desktop.Extensions;assembly=ArcGIS.Desktop.Extensions     5      Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     5        � 	Resources  5       �� MergedDictionaries<  6ArcGIS.Desktop.Extensions.DesignOnlyResourceDictionary   Source�� 5   
    5        5      $S Mpack://application:,,,/ArcGIS.Desktop.Framework;component\Themes\Default.xaml?�6:   %    5      .��5      �  J� RenderTransformOrigin$ 	0.3,0.487&�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   ��. -  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Local=�66   $��30��6_   .+�5      �� $��1��6   $��Center=�6A   $��Center=�6\   $��16(�5      $��Light"�6#   ��5   (   �� *	Heading+ � ToolTip 5      � 5       � Orientation$ Vertical=�6    $	��300��67   .+�5      �� $
��Wrap=�6=   ��6$   �� *	Heading+6=   5      5!      5"      �� 5#   
   -   5         ��Name$ 	cityLabel��5#      $
��City��6#   $
��Left=�62   $��	10,10,0,0q�6M   $��1��6`   $	��Top=�6m   �� 5$   
   -   5        	 ��Name$	 cityComboBox��5$      $
��Left=�6)   $��	10,31,0,0q�6D   $��1��6W   $	��Top=�6d   $	��216��6|   ��6�   �� *	CityNames+6�   �� 5%   
   -   5        
 ��Name$
 cityZoomText��5%      $��23��6(   $��231,30,10,0q�64   $��1��6I    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j    �� 
IsReadOnly . ��6�   �� *CityZoomCompleted+�� 5&   
   -   5        $ cityZoomLabel��5&      $��Focused?��6'   $
��Left=�6:   $��	231,9,0,0q�6U   $��1��6h   $	��Top=�6u   �� 5'   
   -   5        $ neighborhoodLabel��5'      $��Neighborhood��6+   $
��Left=�6B   $��	10,58,0,0q�6]   $��1��6p   $	��Top=�6}   �� 5(   
   -   5        $	 neighborhoodComboBox��5(      $��	10,79,0,0q�61   $	��Top=�6D   $��1��6\   $
��Left=�6�   $	��216��6  ��6�   �� *NeighborhoodNames+6  �� 5)   
   -   5        $
 neighborhoodZoomText��5)      $��23��60   $��231,78,10,0q�6<   $��1��6Q   $
 Wrap=�6^   $	��Top=�6r    . ��6�   �� *NeighborhoodZoomCompleted+�� 5*   
   -   5        $ neighborhoodZoomLabel��5*      $��Focused?��6/   $
��Left=�6B   $��
231,58,0,0q�6]   $��1��6q   $	��Top=�6~   5+      5,      