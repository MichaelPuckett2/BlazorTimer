using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace BlazorTimerComponents;
public class BlazorTimer : ComponentBase, IDisposable, INotifyPropertyChanged
{
    private readonly Timer timer;
    private int interval = Timeout.Infinite;
    private bool isRunning;

    public event PropertyChangedEventHandler? PropertyChanged = default!;

    public BlazorTimer()
    {
        timer = new Timer(async state => await InvokeAsync(async () => await Ellapsed.InvokeAsync()), null, Timeout.Infinite, Timeout.Infinite);
        PropertyChanged += BlazorTimer_PropertyChanged;
    }

    [Parameter]
    public EventCallback Ellapsed { get; set; } = default!;

    [Parameter]
    public int Interval
    {
        get => interval;
        set
        {
            if (value == interval) return;
            interval = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Interval)));
        }
    }

    [Parameter]
    public bool IsRunning
    {
        get => isRunning;
        set
        {
            if (value == IsRunning) return;
            isRunning = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
        }
    }

    private void BlazorTimer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IsRunning):
                if (IsRunning) timer.Change(0, Interval);
                else timer.Change(Timeout.Infinite, Timeout.Infinite);
                break;
            case nameof(Interval):
                if (IsRunning) timer.Change(0, Interval);
                break;
        }
    }

    public void Dispose()
    {
        timer.Dispose();
        GC.SuppressFinalize(this);
    }

    ~BlazorTimer() => Dispose();
}