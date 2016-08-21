using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    // Максимальная сила тяги
    public int maxTraction;
    // Текущая тяга
    private Vector2 _traction;
    /// Крутящий момент
    public float torque;
    // Прочность корабля
    public int shipStrength;
    // Текущая прочность
    public int strength;
    // Количество мест для оборудования
    public int numberOfEquipments;
    // Количество мест для курсового вооружения
    public int numberOfАrms;
    // Количество мест под тяжелые снаряды
    public int numberOfMissiles;
    // Количество мест для турелей
    public int numberOfTurrets;

	private Rigidbody2D _rb;
	// момент инерции
	private float _angleAcceleration;
	// целевой угол поворота
	public float finalAngle;

    // Текущая тяга в %
    public float Traction
    {
        get { return Mathf.Sqrt(_traction.x * _traction.x + _traction.y * _traction.y) / maxTraction; }
        set 
		{
			float angle = transform.eulerAngles.z * Mathf.PI / 180;
			_traction.x = -value * maxTraction * Mathf.Sin(angle);
			_traction.y = value * maxTraction * Mathf.Cos(angle);
		}
    }

	// Возвращает разность между 2 углами в зависимости от направления
	// Пременима для углов от 0 до 360
    // Сейчас нигде не используется
	public float DifferenceAngleWithWay(float sourceAngle, float targetAngle, bool isАnticlockwise)
	{
		if (isАnticlockwise && targetAngle >= sourceAngle || !isАnticlockwise && targetAngle <= sourceAngle)
		{
			return Mathf.Abs(targetAngle - sourceAngle);
		}
		else
		{
			return 360 - Mathf.Abs(targetAngle - sourceAngle);
		}
	}

	private void Maneuvering()
	{
		//////////////////////////// Маневрирование //////////////////////////////////////////////////////////////////////////
		float angularVelocity = _rb.angularVelocity;
		float dStopAngle = angularVelocity * angularVelocity / (2 * _angleAcceleration);	// Угол через который вращение прекратится
		float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, finalAngle);

		// Определяем время на полную остановку (см. рис "BIS - маневрирование2") и на конечное торможение
		float time, brakingTime;
		if (Mathf.Abs(deltaAngle) >= dStopAngle)
		{
			brakingTime = Mathf.Sqrt((dStopAngle + Mathf.Abs(deltaAngle)) / _angleAcceleration);
			time = 2 * brakingTime - Mathf.Abs(angularVelocity) / _angleAcceleration;
		}
		else
		{
			brakingTime = Mathf.Sqrt((dStopAngle - Mathf.Abs(deltaAngle)) / _angleAcceleration);
			time = Mathf.Abs(angularVelocity) / _angleAcceleration + 2 * brakingTime;
		}
		if (deltaAngle > 0 && angularVelocity < 0 || deltaAngle < 0 && angularVelocity > 0) 
			time += 2 * Mathf.Abs(angularVelocity) / _angleAcceleration;
		// Определяем не достигнем ли цели?
		if (time <= Time.fixedDeltaTime || (brakingTime <= Time.fixedDeltaTime && time - brakingTime <= Time.fixedDeltaTime))	
		{
			// стопаем
			_rb.rotation = finalAngle;
			_rb.angularVelocity = 0;
			_rb.AddTorque(0);
		}
		else
		{
			if (time - brakingTime <= Time.fixedDeltaTime)	// Определяем не пропустим ли момент торможения
			{
				float dAngle = _angleAcceleration * brakingTime * brakingTime / 2;
				float v = _angleAcceleration * brakingTime;
				if (deltaAngle >= 0 && Mathf.Abs(deltaAngle) < dStopAngle ||
					deltaAngle < 0 && Mathf.Abs(deltaAngle) >= dStopAngle)
				{
					v *= -1;
					dAngle *= -1;
				}
				_rb.rotation = finalAngle - dAngle;
				_rb.angularVelocity = v;
				// тормозим
				if (v > 0) _rb.AddTorque(-torque);
				else _rb.AddTorque(torque);
			} 
			else 
				if (Mathf.Abs(deltaAngle) > dStopAngle)
			{
				// Поворачиваем в сторону finalAngle
				if (deltaAngle > 0) _rb.AddTorque(torque);
				else _rb.AddTorque(-torque);
			}
			else
			{
				// тормозим
				if (angularVelocity >= 0) _rb.AddTorque(-torque);
				else _rb.AddTorque(torque);
			}
		}
	}

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
		finalAngle = _rb.rotation;
		_angleAcceleration = torque / _rb.inertia * 180 / Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void FixedUpdate()
    {
        _rb.AddForce(_traction); // Придал импульс
		Maneuvering();
    }

	void OnCollisionEnter(Collision collision)
	{

	}
}
