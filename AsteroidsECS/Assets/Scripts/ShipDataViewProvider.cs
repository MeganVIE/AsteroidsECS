using UnityEngine;

public interface IShipDataViewProvider
{
    void SetShipObject(GameObject ship);
    void SetShipPosition(Vector2 newPosition);
}

public class ShipDataViewProvider : IShipDataViewProvider
{
    private GameObject _ship;

    public void SetShipObject(GameObject ship)
    {
        _ship = ship;
    }

    public void SetShipPosition(Vector2 newPosition)
    {
        _ship.transform.position = newPosition;
    }
}