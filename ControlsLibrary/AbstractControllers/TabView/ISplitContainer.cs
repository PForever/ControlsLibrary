namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface ISplitContainer : IPanel
    {
        IPanel Panel1 { get; set; }
        IPanel Panel2 { get; set; }
        int RelativePosition { get; set; }
    }
}