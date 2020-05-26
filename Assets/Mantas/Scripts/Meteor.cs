using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float Health = 100;
    private float initialHealth;
    public Rigidbody rb;
    public Transform resourcePrefab;
    public int resourceCount = 1;
    public MeteorSettings.MeteorType meteorType;


    void Start()
    {
        initialHealth = Health;
    }

    void Update()
    {
        CheckHealth();
    }

    void FixedUpdate()
    {

    }

    public void CheckHealth()
    {
        // If meteor health is below or equal to 0, it will be destroyed and resources will be spawned
        if (Health <= 0)
        {
            for (int i = 0; resourceCount < i; i++)
            {
                Vector3 spacing = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
                spacing *= gameObject.transform.localScale.x;
                Instantiate(resourcePrefab, transform.position + spacing, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        float s = Mathf.Clamp((Health / initialHealth), 0.5f, 1f);
        transform.localScale = new Vector3(s, s, s);
    }
}
