# BlazorTimer
A blazor timer component to make handling timed events easier.

Easy to use:

    <BlazorTimer Interval=@interval Ellapsed=@Ellapsed IsRunning=@isRunning />

    @code {
        private int interval;
        private bool isRunning;
        private void Ellapsed() => //some logic
    }

Try this view in Blazor to experiment:
    @page "/"
    <PageTitle>Blazor Timer</PageTitle>
    <p role="status">Current count: @currentCount</p>
    <button class="btn btn-primary" @onclick="ResetCount">Reset</button>
    <input type="number" @bind-value=@interval />
    <button class="btn btn-primary" @onclick=@(x => isRunning = true)>Start Timer</button>
    <button class="btn btn-primary" @onclick=@(x => isRunning = false)>Stop Timer</button>
    <BlazorTimer Interval=@interval Ellapsed=@BlazorTimerEllapsed IsRunning=@isRunning/>
    @code {
        private int currentCount = 0;
        private int interval = Timeout.Infinite;
        private bool isRunning;
        private void BlazorTimerEllapsed() => currentCount++;
        private void ResetCount() => currentCount = 0;
    }