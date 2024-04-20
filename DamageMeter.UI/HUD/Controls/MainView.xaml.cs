using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Tera.Game;

namespace DamageMeter.UI.HUD.Controls;

public partial class MainView : UserControl
{
    private readonly Border _playersOc;

    private readonly VisualBrush _vb;

    private readonly MainViewModel _dc;

    public MainView()
    {
        InitializeComponent();
        Loaded += OnLoaded;

        _playersOc = new Border
        {
            Padding = new Thickness(0),
            CornerRadius = new CornerRadius(0, 0, 8, 8),
            Background = Brushes.White
        };


        _vb = new VisualBrush
        {
            Visual = _playersOc,
            Stretch = Stretch.None,
            //AutoLayoutContent = true,
            AlignmentY = AlignmentY.Top
        };

        _dc = (MainViewModel)Application.Current.MainWindow!.DataContext;
        _dc.GraphData.PropertyChanged += OnGraphDataPropertyChanged;

    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var bh = new Binding
        {
            Source = PlayersContainer,
            Path = new PropertyPath(nameof(PlayersContainer.ActualHeight)),
        };

        var bw = new Binding
        {
            Source = PlayersContainer,
            Path = new PropertyPath(nameof(PlayersContainer.ActualWidth)),
        };

        _playersOc.SetBinding(Border.WidthProperty, bw);
        _playersOc.SetBinding(Border.HeightProperty, bh);

        PlayersContainer.OpacityMask = _vb;
    }

    private void OnGraphDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(RealtimeChartViewModel.IsChartVisible))
        {
            return;
        }

        PlayersContainer.OpacityMask = _dc.GraphData.IsChartVisible
            ? null
            : _vb;
    }

    private void ListEncounter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count != 1) { return; }

        NpcEntity? encounter = null;
        if (e.AddedItems[0] is NpcEntity en && en != MainViewModel.TotalEncounter)
        {
            encounter = en;
        }

        if (encounter != PacketProcessor.Instance.Encounter)
        {
            PacketProcessor.Instance.NewEncounter = encounter;
        }
    }

    private void ListEncounter_OnDropDownOpened(object sender, EventArgs e)
    {
        App.HudContainer.TopMostOverride = false;
    }

    private void ListEncounter_OnDropDownClosed(object sender, EventArgs e)
    {
        App.HudContainer.TopMostOverride = true;
    }

    private void ListEncounterOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
    {
        keyEventArgs.Handled = true;
    }
}