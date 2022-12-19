using UnityEngine;

public class PlayerMouseDirection : MonoBehaviour
{
    [SerializeField] Transform mouseDirection;
    [SerializeField] Camera mainCamera;
    Vector2 offset;

    private void Update()
    {
        //Vector2 mousePoint = Mouse.current.position.ReadValue();
        Vector2 mousePoint = Input.mousePosition;

        Vector2 attackPoint = mainCamera.WorldToScreenPoint(mouseDirection.position);

        offset = mousePoint - attackPoint;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        mouseDirection.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Vector2 GetMouseDirection()
    {
        return offset.normalized;
    }

    public float GetMouseHorizontalDirection()
    {
        float _horizontalDirection = GetMouseDirection().x > 0f ? 1f : -1f;
        return _horizontalDirection;
    }
}
