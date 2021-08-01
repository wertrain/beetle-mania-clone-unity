using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBulletEmitter : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    public void Emit(Vector3 position, int comboCount)
    {
        var prefab = (GameObject)Resources.Load("Prefabs/ShellBullet");

        for (int index = 0; index < 4; ++index)
        {
            var bullet = Instantiate(prefab, position, Quaternion.identity);
            bullet.GetComponent<ShellBullet>().ComboCount = comboCount;
        }
    }
}
