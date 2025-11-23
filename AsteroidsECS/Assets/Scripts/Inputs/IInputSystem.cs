namespace Inputs
{
    public interface IInputSystem
    {
        bool MovePressing { get; }
        float RotationValue { get; }
        bool GunUsing { get; }
        bool LaserUsing { get; }
    }
}