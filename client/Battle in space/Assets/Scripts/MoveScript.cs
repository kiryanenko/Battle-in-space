using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

    /// <summary>
    /// Скорость объекта
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Направление движения
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;

    void Update()
    {
        // 2 - Перемещение
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    void FixedUpdate()
    {
        // Применить движение к Rigidbody
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
