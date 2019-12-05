using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public List<GameObject> Enemys = new List<GameObject>();
    public List<Enemy> EnemysSc = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemysSc.Count<10 && Enemys.Count!=0)
        {
            //если врагов меньше 10 создаем нового и выбираем рандомную позицию за сценой
            EnemysSc.Add(Instantiate(Enemys[Random.Range(0, Enemys.Count)], transform).GetComponent<Enemy>());
            float r_x = Random.Range(-1, 1);
            if (r_x == 0)
                r_x = 1;
            float r_y = Random.Range(-1, 1);
            if (r_y == 0)
                r_y = 1;
            int random = Random.Range(0, 2);
            Vector3 v = new Vector3();
            if(random==0)
            {
                v.x = Random.Range(-23f, 23f);
                v.z = r_y * 23f;
            }
            else
            {
                v.z = Random.Range(-23f, 23f);
                v.x = r_x * 23f;
            }
            v.y = 1;
            EnemysSc[EnemysSc.Count - 1].transform.position = v;
        }
        for(int i = EnemysSc.Count-1;i>=0;i--)
        {
            if(EnemysSc[i].Death)
            {
                //удаляем убитого
                EnemysSc.RemoveAt(i);
            }
        }
    }
}
