using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float SpeedRotation;
    public float MaxSpeedMove;
    public float Acceleration;//ускорение

    public Vector3 Vector = new Vector3();

    private float _angle = 0;//угол поворота
    private Rigidbody _rb;
    private float _speed = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Rotation_left()
    {
        _angle -= SpeedRotation;
        Vector = new Vector3(Mathf.Cos(_angle / Mathf.Rad2Deg), 0, -Mathf.Sin(_angle / Mathf.Rad2Deg)).normalized;
    }
    public void Rotation_right()
    {
        _angle += SpeedRotation;
        Vector = new Vector3(Mathf.Cos(_angle / Mathf.Rad2Deg), 0, -Mathf.Sin(_angle / Mathf.Rad2Deg)).normalized;
    }
    public void VectorAngle(float angle)
    {
        _angle = angle;
        Vector = new Vector3(Mathf.Cos(angle / Mathf.Rad2Deg), 0, -Mathf.Sin(angle / Mathf.Rad2Deg)).normalized;
    }
    public void Forward()
    {
        move(Acceleration);
    }
    public void Back()
    {
        move(-Acceleration);
    }
    // Update is called once per frame
    void Update()
    {
        _rb.MovePosition(_rb.transform.position + Vector * _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, _angle, 0));
    }
    public void Braking()//тормажение
    {
        if (_speed != 0)
        {
            _speed -= _speed / 10;
            if (Mathf.Abs(_speed) < 0.002)
            {
                _speed = 0;
            }
        }
    }
    private void move(float accel)
    {
        if (_speed + accel > MaxSpeedMove)
        {
            //максимальная скорость движения вперед
            _speed = MaxSpeedMove;
        }
        else if(_speed + accel < -MaxSpeedMove)
        {
            //максимальная скорость движение назад
            _speed = -MaxSpeedMove;
        }
        else
        {
            _speed += accel;
        }
    }
}
