namespace Inputs
{
    public interface IInputService
    {
        bool MovePressing { get; }
        float RotationValue { get; }
        bool GunUsing { get; }
        bool LaserUsing { get; }
    }
}