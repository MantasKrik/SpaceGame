using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject obj;
    public GameObject loc;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var go = gameObject.transform.parent.gameObject;
            GameObject o = Instantiate(obj, loc.transform.position, Quaternion.identity);
            o.GetComponent<Rigidbody>().AddForce(go.transform.forward * 10, ForceMode.Impulse);
            StartCoroutine(ExecuteAfterTime(3, o));
        }

    }

    IEnumerator ExecuteAfterTime(float time, GameObject o)
    {
        yield return new WaitForSeconds(time);

        Destroy(o);
    }
}
