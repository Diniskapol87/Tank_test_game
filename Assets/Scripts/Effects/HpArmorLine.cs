using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpArmorLine : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float HP;
    [Range(0.0f, 1.0f)]
    public float Armor;
    public LineRenderer LineHp;
    public LineRenderer LineArmor;
    public float MoveUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //визуализируем состояние при помощи LineRenderer
        LineHp.SetPosition(0, new Vector3(transform.position.x - HP / 2, 0f, transform.position.z + MoveUp));
        LineHp.SetPosition(1, new Vector3(transform.position.x + HP / 2, 0f, transform.position.z + MoveUp));
        LineArmor.SetPosition(0, new Vector3(transform.position.x - Armor / 2, 0f, transform.position.z + 0.14f + MoveUp));
        LineArmor.SetPosition(1, new Vector3(transform.position.x + Armor / 2, 0f, transform.position.z + 0.14f + MoveUp));
    }
    //высчитываем нанесенный урон
    public float Damage(float damage)
    {
        float dmg = (1f - Mathf.Clamp(Armor, 0f, 1f)) * damage;
        HP -= dmg;
        HP = Mathf.Clamp(HP, 0f, 1f);
        if(HP==0)
        {
            Armor = 0;
        }
        return dmg;
    }
}
