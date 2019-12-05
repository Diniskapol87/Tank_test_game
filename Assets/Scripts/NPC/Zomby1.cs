using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zomby1 : Enemy
{
    private float _timerMove = 0;
    private float _timerStay = 0;
    private float _timer = 0;
    private string _action = "MoveToTarget";
    
    // Start is called before the first frame update
    void Awake()
    {
        _timerMove = Random.Range(300,600);
        HpAmor = transform.Find("HpArmor").GetComponent<HpArmorLine>();
    }
    //столкновение с врагом
    public override void Collide(bool b)
    {
        if(b)
        {
            _action = "Stay";
            AttackTarget();
        }
        else
        {
            _action = "MoveToTarget";
            Anim.Play("Move");
        }
    }
    //бъем цель
    public void AttackTarget()
    {
        if (Target != null)
        {
            Anim.FunctionAfterAnimation("Attack", AttackTarget);
            Target.GetComponent<Player>().Attack(Damage);
        }
        else
        {
            _timerMove = Random.Range(300, 600);
            _action = "MoveToTarget";
            Anim.Play("Move");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Death)
            Move.Braking();
        else
        {
            switch (_action)
            {
                case "MoveToTarget":
                    _timer++;
                    if (_timer % _timerMove == 0)
                    {
                        Anim.FunctionAfterAnimation("AFK", AttackTarget);
                        _action = "Stay";
                        _timer = 0;
                    }
                    MoveToTarget();
                    break;
                case "Stay":
                    Move.Braking();
                    break;
            }
        }
    }
}
