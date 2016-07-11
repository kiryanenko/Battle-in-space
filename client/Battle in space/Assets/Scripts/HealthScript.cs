using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	/// <summary>
  /// Всего хитпоинтов
  /// </summary>
  public int hp = 1;

  /// <summary>
  /// Враг или игрок?
  /// </summary>
  public bool isEnemy = true;

  /// <summary>
  /// Наносим урон и проверяем должен ли объект быть уничтожен
  /// </summary>
  /// <param name="damageCount"></param>
  public void Damage(int damageCount)
  {
    hp -= damageCount;

    if (hp <= 0)
    {
      // Смерть!
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D otherCollider)
  {
    // Это выстрел?
    ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
    if (shot != null)
    {
      // Избегайте дружественного огня
      if (shot.isEnemyShot != isEnemy)
      {
        Damage(shot.damage);

        // Уничтожить выстрел
        Destroy(shot.gameObject); // Всегда цельтесь в игровой объект, иначе вы просто удалите скрипт.      
      }
    }
  }
}
