using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory _inventory;
    private Move _move;
    public HpArmorLine HpAmor;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = GetComponent<Inventory>();
        _move = GetComponent<Move>();
        HpAmor = transform.Find("HpArmor").GetComponent<HpArmorLine>();
    }
    //получаем урон
    public void Attack(float damage)
    {
        Core.DamagePos.Add(transform.position);
        Core.DamageCount.Add(HpAmor.Damage(damage) * 100);
        if (HpAmor.HP == 0)
        {
            Delete();
        }
    }
    //удаление- смерть
    public void Delete()
    {
        Destroy(transform.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
            _inventory.Active_gun.Shoot(true);
        if (Input.GetKey(KeyCode.LeftArrow))
            _move.Rotation_left();
        if (Input.GetKey(KeyCode.RightArrow))
            _move.Rotation_right();
        if (Input.GetKey(KeyCode.UpArrow))
            _move.Forward();
        else if (Input.GetKey(KeyCode.DownArrow))
            _move.Back();
        else
            _move.Braking();

        //передаем свою позицию в корневой класс
        Core.PlayerPosition = transform.position;
    }
}
