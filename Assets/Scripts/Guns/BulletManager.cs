using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager
{
    private static List<List<GameObject>> _listBullet = new List<List<GameObject>>();
    private static List<GameObject> _listEffect = new List<GameObject>();


    public static GameObject CreateBullet(int type)
    {
        while(_listBullet.Count<=type)
        {
            _listBullet.Add(new List<GameObject>());
        }
        GameObject bullet = null;
        if (_listBullet[type].Count>0)
        {
            bullet = _listBullet[type][0];
            _listBullet[type].RemoveAt(0);
        }
        return bullet;
    }
    public static void DestroyBullet(int type, GameObject bullet)
    {
        bullet.SetActive(false);
        _listBullet[type].Add(bullet);
    }
    public static GameObject CreateBoomEffect()
    {
        GameObject effect = null;
        if(_listEffect.Count>0)
        {
            effect = _listEffect[0];
            _listEffect.RemoveAt(0);
        }
        return effect;
    }
    public static void DestroyEffect(GameObject effect)
    {
        effect.SetActive(false);
        _listEffect.Add(effect);
    }
}
