using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Explosive = false;
    public GameObject Boom_effect;
    public float RangeRadius;
    public float Damage;

    private int _type;
    private Vector3 _vector = Vector3.zero;
    private float _distans;
    private float _speed = 0;
    private Vector3 _startPos;
    private Rigidbody _rbBullet;


    private void Awake()
    {
        _rbBullet = GetComponent<Rigidbody>();
    }
    //проверяем столкновение пали с игровыми объектами
    private void OnTriggerEnter(Collider coll)
    {
        //print(coll.gameObject.tag);
        //если пуля попала в мишень
        if (coll.gameObject.tag == "Enemy")
        {
            //проверяем меткость
            coll.gameObject.GetComponent<Enemy>().Attack(Damage);
            DeleteBollet();
        }
    }
    //выстрел
    public void Shoot(int type, Vector3 start_pos, Vector3 vec_mov, float speed,float distans)
    {
        _type = type;
        transform.position = start_pos;
        _startPos = start_pos;
        _vector = vec_mov.normalized;
        _speed = speed;
        _distans = distans;
        _rbBullet.velocity = _vector * _speed;
    }
    // Update is called once per frame
    void Update()
    {
        //удаление пули после пройденной максимальной дистанции
        if (_distans <= Vector3.Distance(_startPos,transform.position))
        {
            DeleteBollet();
        }
    }
    //удаление снаряда-пули
    private void DeleteBollet()
    {
        if(Explosive)
        {
            GameObject go = BulletManager.CreateBoomEffect();
            if (go == null)
            {
                go = Instantiate(Boom_effect);
            }
            go.transform.position = transform.position;
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().Play();
        }
        BulletManager.DestroyBullet(_type, this.transform.gameObject);
    }
}
