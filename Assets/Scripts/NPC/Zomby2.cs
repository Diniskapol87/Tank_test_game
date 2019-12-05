using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zomby2 : Enemy
{
    private float _timerMove = 0;
    private float _timerStay = 0;
    private float _timer = 0;
    private string _action = "MoveToTarget";
    //private Collider _collider;
    private bool _bottom = false;//закапался или нет

    // Start is called before the first frame update
    void Awake()
    {
        //_collider = GetComponent<Collider>();
        _timerMove = Random.Range(300, 600);
        HpAmor = transform.Find("HpArmor").GetComponent<HpArmorLine>();
    }
    //переписываем родительский метод (разворот анимации)
    public override void TurnOnTarget(bool b)
    {
        Anim.ArmatureComponent.armature.flipX = !b;
    }
    //получение урона
    public override void Attack(float damage)
    {
        if (!Death)
        {
            Core.DamagePos.Add(transform.position);
            Core.DamageCount.Add(HpAmor.Damage(damage) * 100);
            if (HpAmor.HP == 0)
            {
                Death = true;
                Anim.ClearFun();
                Anim.FunctionAfterAnimation("Death1", Delete);
            }
        }
    }
    public override void Collide(bool b)
    {
        if (b)
        {
            AttackTarget();            
        }
        else
        {
            
        }
    }
    //атакуем цель
    public void AttackTarget()
    {
        if (Target != null)
        {
            Target.GetComponent<Player>().Attack(Damage);
        }
        else
        {
            _timerMove = Random.Range(300, 600);
            _action = "MoveToTarget";
            Anim.Play("Move");
        }
    }
    //закапываемся
    private void Bottom()
    {
        _bottom = !_bottom;
        if(!_bottom)
        {
            Anim.ArmatureComponent.animation.timeScale = 1;
            _action = "MoveToTarget";
            Anim.Play("Move");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Anim.NameAnim == "Death1")
            Move.Braking();
        else
        {
            if (_action != "Stay" && Vector3.Distance(Core.PlayerPosition, transform.position) < 10)
            {
                //закапываемся
                //_collider.isTrigger = true;
                _action = "Stay";
                Anim.FunctionAfterAnimation("Капаю", Bottom, 1);
            }
            if (_bottom && Vector3.Distance(Core.PlayerPosition, transform.position) > 10)
            {
                //откапываемся
                Anim.ArmatureComponent.animation.timeScale = -2;
                Anim.FunctionAfterAnimation("Капаю", Bottom, 1);
            }
            switch (_action)
            {
                case "MoveToTarget":
                    MoveToTarget();
                    break;
                case "Stay":
                    Move.Braking();
                    break;
            }
        }
    }
}
