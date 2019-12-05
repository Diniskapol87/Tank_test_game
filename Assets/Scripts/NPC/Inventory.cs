using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text TextCountBullet;
    public Text TextMessageGun;
    public Text TextNameGun;

    public List<GameObject> Guns = new List<GameObject>();//массив оружия в инвентаре

    public Gun Active_gun;//активное оружие
    private int _activGun_Id = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(Guns.Count>0)
        {
            ActiveGun(0);
        }
    }
    //сохраняем найденное оружие в инвентарь
    private void new_gun(GameObject gun)
    {
        gun.transform.SetParent(transform);
        gun.transform.position = transform.position;
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (Guns.Count == 0)
        {
            Active_gun = gun.GetComponent<Gun>();
        }
        else
        {
            gun.SetActive(false);
        }
        Guns.Add(gun);
        ActiveGun(Guns.Count - 1);
    }
    //подбираем разбросанное оружие
    private void OnTriggerEnter(Collider coll)
    {
        //print(coll.gameObject.name);
        if (coll.gameObject.tag == "Gun")
        {
            new_gun(coll.gameObject);
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        //print(coll.gameObject.name);
    }
    //активируем подобранное оружие
    private void ActiveGun(int id)
    {
        if(id<0)
        {
            id = Guns.Count - 1;
        }
        if(id> Guns.Count-1)
        {
            id = 0;
        }
        _activGun_Id = id;
        if (id<Guns.Count)
        {
            for(int i=0;i<Guns.Count;i++)
            {
                if(id!=i)
                {   
                    //если нужно скрывать оружие
                    //Guns[i].SetActive(false);
                }
                else
                {
                    //если нужно отображать активное оружие
                    //Guns[i].SetActive(true);
                    Active_gun = Guns[i].GetComponent<Gun>();
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            ActiveGun(0);
        if (Input.GetKeyUp(KeyCode.Alpha2))
            ActiveGun(1);
        if (Input.GetKeyUp(KeyCode.Alpha3))
            ActiveGun(2);
        if (Input.GetKeyUp(KeyCode.R))//перезарядка
            Active_gun.StartReload();
        if (Input.GetKeyUp(KeyCode.Q))
            ActiveGun(_activGun_Id - 1);
        if (Input.GetKeyUp(KeyCode.W))
            ActiveGun(_activGun_Id + 1);

        //отображаем данные по оружию игроку
        TextCountBullet.text = Active_gun.text_count_bullet;
        TextMessageGun.text = Active_gun.text_info;
        TextNameGun.text = "Оружие №"+ (_activGun_Id+1)+ "\nБоезапас:";
    }
}
