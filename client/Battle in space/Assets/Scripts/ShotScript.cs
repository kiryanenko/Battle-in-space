using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    // 1 – Переменная дизайнера

    /// <summary>
    /// Причиненный вред
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Снаряд наносит повреждения игроку или врагам?
    /// </summary>
    public bool isEnemyShot = false;

    void Start()
    {
        // Ограниченное время жизни, чтобы избежать утечек
        Destroy(gameObject, 20); // 20 секунд
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
