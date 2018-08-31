using ControlsLibrary.Containers;
namespace ControlsLibrary.AbstractControllers
{
    public interface IPanel : IContainer
    {
        Orientation Orientation { get; set; }
    }
}
