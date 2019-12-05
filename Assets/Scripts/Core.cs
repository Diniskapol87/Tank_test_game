using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Core
{
    //место где нанесли урон
    public static List<Vector3> DamagePos = new List<Vector3>();
    //количество нанесенного урона
    public static List<float> DamageCount = new List<float>();
    //позиция игрока
    public static Vector3 PlayerPosition = new Vector3();
}
