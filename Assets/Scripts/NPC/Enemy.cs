using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float Damage;//урон персонажа
    //имена анимаций
    public string NameAnimAttack;
    public string NameAnimDeath;
    public string NameAnimStay;

    public Move Move;
    public Animator Anim;
    public GameObject Target;
    public HpArmorLine HpAmor;

    public bool Death = false;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Move = GetComponent<Move>();
    }
    private void OnTriggerEnter(Collider coll)
    {
        //приблизились к герою
        if (coll.gameObject.tag == "Player")
        {
            Target = coll.gameObject;
            Collide(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //отдалились от героя
        if (other.gameObject.tag == "Player")
        {
            Target = null;
            Collide(false);
        }
    }
    //получем урон
    public virtual void Attack(float damage)
    {
        if (!Death)
        {
            Core.DamagePos.Add(transform.position);
            Core.DamageCount.Add(HpAmor.Damage(damage) * 100);
            if (HpAmor.HP == 0)
            {
                Death = true;
                Anim.ClearFun();
                Anim.FunctionAfterAnimation("Death", Delete);
            }
        }
    }
    //родительская фнкция столкновения используется в детях с изменениями
    public virtual void Collide(bool b)
    {

    }
    //удаление объекта
    public void Delete()
    {
        Destroy(transform.gameObject);
    }
    //получение вектора движения(стрельбы) к цели
    public Vector3 TargetVector()
    {
        Vector3 v = Core.PlayerPosition - transform.position;
        return v.normalized;
    }
    //разворот анимации
    public virtual void TurnOnTarget(bool b)
    {
        Anim.ArmatureComponent.armature.flipX = b;
    }
    //движение к цели
    public void MoveToTarget()
    {
        if (transform.position.x < Core.PlayerPosition.x)
            TurnOnTarget(true);
        else
            TurnOnTarget(false);

        if (Move != null)
        {
            Move.Vector = TargetVector();
            Move.Forward();
        }
    }
}
