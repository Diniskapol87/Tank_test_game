using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageVisualization : MonoBehaviour
{
    public GameObject Text;//Префаб текстого поля вылетающего при нанесении урона
    private List<GameObject> _arText = new List<GameObject>();//массип префабов текстового поля
    private List<Vector3> _arVector = new List<Vector3>();//траектория полета
    private List<int> _arLife = new List<int>();//время существования

    // Update is called once per frame
    void Update()
    {
        //смотрим был ли нанесен урон, если да то визуализируем его
        if (Core.DamageCount.Count > 0)
        {
            _arLife.Add(0);
            _arText.Add(Instantiate(Text, transform));
            _arVector.Add(new Vector3(Random.Range(-10f, 10f), 0.4f, Random.Range(-10f, 10f)).normalized);
            TextMesh text = _arText[_arText.Count - 1].GetComponent<TextMesh>();
            //Если урон меньше или равен нулю пишем Miss
            if (Core.DamageCount[Core.DamageCount.Count - 1] <= 0)
            {
                //text.text = "Miss";
            }
            else
            {
                //Тут считаем общий нанесенный урон и выводим на экран
                //HUD_txt.text = (int.Parse(HUD_txt.text) + Core.damageCount[Core.damageCount.Count - 1]).ToString();

                text.text = Core.DamageCount[Core.DamageCount.Count - 1].ToString();
            }
            //если урон больше или равен 9 подсвечиваем его красным
            if (Core.DamageCount[Core.DamageCount.Count - 1] >= 9)
            {
                text.color = Color.red;
            }
            _arText[_arText.Count - 1].transform.position = Core.DamagePos[Core.DamagePos.Count - 1];
            Core.DamageCount.RemoveAt(Core.DamageCount.Count - 1);
            Core.DamagePos.RemoveAt(Core.DamagePos.Count - 1);
        }
        //удаляем старые записи, двигаем молодые
        for (int i = _arText.Count - 1; i >= 0; i--)
        {
            if (_arLife[i] >= 15)
            {
                Destroy(_arText[i]);
                _arLife.RemoveAt(i);
                _arText.RemoveAt(i);
                _arVector.RemoveAt(i);
            }
            else
            {
                _arText[i].transform.position += _arVector[i] * 0.02f;
                _arLife[i]++;
            }
        }
    }
}
