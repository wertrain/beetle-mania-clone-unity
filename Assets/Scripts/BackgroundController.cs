using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    /// <summary>
    /// スクロール速度
    /// </summary>
    public float Speed { get; set; } = 32.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime * Speed, 0);

        if (transform.position.x <= -400f)
        {
            transform.position = new Vector3(400f - 4f, 0f);
        }
    }
}
