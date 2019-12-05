using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//основной класс оружия(является родителем для дробовика)
public class Gun : MonoBehaviour
{
    public int typeGun;
    public GameObject Bullet; //префаб пули
    public float Damage;
    public float Distans;//дистанция стрельбы
    public float SpeedBullet;//скорость пули
    public int MaxMagazineCapacity;//максимальный объем магазина
    public int MagazineCapacity;//патрон в магазине
    public float ShootInSecond;//выстрелов в секунду
    public float SpeedReloat;//в секундах
    public float Recoil;//максимальный угол разброса при выстреле
    public float SpeedStabilRecoil;
    //public System.Action<float> Recoil_fun;//метод для визуализацииотдачи для того кто держит оружие

    public Vector3 Vector = Vector3.zero;//вектор направления пули
    public GameObject Barrel;
    public GameObject Center;
    public bool reload = false;
    public bool shoot = false;
    public float time = 0;//таймер
    public string text_info = "";//информация об оружии
    public string text_count_bullet = "0";//информация о кол-ве патрон в оружии

    private float _angleRecoil = 0;
    // Start is called before the first frame update
    void Awake()
    {
        text_count_bullet = MagazineCapacity.ToString();
        Barrel = transform.Find("Stvol").gameObject;
        Center = transform.Find("Center").gameObject;
    }
    //курок зажат
    public virtual void Shoot(bool onOff)
    {
        Vector = Barrel.transform.position - Center.transform.position;
        Vector = Vector.normalized;
        shoot = onOff;
        //_vector = vec.normalized;
    }
    //начата перезaрядка
    public void StartReload()
    {
        text_info = "Перезарядка...";
        reload = true;
        time = 0;
    }
    //перезарядка окончена
    public virtual void Reload()
    {
        MagazineCapacity = MaxMagazineCapacity;
        text_count_bullet = MagazineCapacity.ToString();
        reload = false;
        text_info = "";
        time = 0;
    }
    //создание пуль для выстрела
    public virtual void Bullet_making()
    {
        GameObject bullet = BulletManager.CreateBullet(typeGun);
        if(bullet==null)
        {
            bullet = Instantiate(Bullet);
        }
        Bullet bull = bullet.GetComponent<Bullet>();
        bull.Damage = Damage;
        bull.Shoot(typeGun, Barrel.transform.position, Vector, SpeedBullet, Distans);
        bullet.SetActive(true);
        recoil(Recoil/2 * Random.Range(-1f, 1f));
    }
    private void recoil(float angle)
    {
        _angleRecoil += angle;
    }
    // Update is called once per frame
    void Update()
    {
        //разброс при стрельбе (при выстрелах поворачивает оружие, поэтому плавно стабилизируем его - "прицеливаемся")
        if (Mathf.Abs(_angleRecoil) <= SpeedStabilRecoil)
        {
            _angleRecoil = 0;
        }
        else
        {
            if (_angleRecoil > 0)
            {
                _angleRecoil -= SpeedStabilRecoil;
            }
            else
            {
                _angleRecoil += SpeedStabilRecoil;
            }
        }

        // Трансформируем угол в вектор
        Quaternion rot = Quaternion.Euler(new Vector3(0, _angleRecoil, 0));
        // Изменяем поворот оружия
        transform.localRotation = rot;

        if (!reload)
        {
            if (MagazineCapacity > 0)
            {
                if (time == 0 || time >= 100 / ShootInSecond)
                {
                    if (shoot)
                    {
                        time = Time.deltaTime * 100;
                        //print("POOF");
                        MagazineCapacity--;
                        text_count_bullet = MagazineCapacity.ToString();
                        Bullet_making();
                    }
                }
                else
                {
                    time += Time.deltaTime * 100;
                }
            }
            else
            {
                //автоматическая перезарядка если патроны закончились, а стрелять хочется.
                StartReload();
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time>= SpeedReloat)
            {
                Reload();
            }
        }
        shoot = false;
    }
}
