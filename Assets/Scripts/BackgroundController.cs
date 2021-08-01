using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    /// <summary>
    /// スクロール速度
    /// </summary>
    public float Speed { get; set; } = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime * Speed, 0);

        if (transform.position.x <= -14.95f)
        {
            transform.position = new Vector3(24.95f, 0.0f);
        }
    }
}
