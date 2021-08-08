using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBulletEmitter : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public int EmitCount = 4;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    public void Emit(Vector3 position, int comboCount)
    {
        var prefab = (GameObject)Resources.Load("Prefabs/ShellBullet");

        for (int index = 0; index < EmitCount; ++index)
        {
            var bullet = Instantiate(prefab, position, Quaternion.identity);
            bullet.GetComponent<ShellBullet>().ComboCount = comboCount;
        }
    }
}
