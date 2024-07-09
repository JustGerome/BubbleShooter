using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerDestroy(time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TimerDestroy(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
