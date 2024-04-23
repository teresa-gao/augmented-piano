using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public Vector3 _noteTarget;
    public Vector3 _noteOrigin;
    public float _time;
    public float _velocity = 1.0f;
    public void Start()
    {
        _time = Time.time;
    }

    void Update()
    {
        float ratio = (Time.time - _time) / _velocity;

        Vector3 beginning = (_noteOrigin * (1 - ratio) + _noteTarget * ratio);
        Vector3 end = (_noteOrigin * (1 - (ratio + 0.1f)) + _noteTarget * (ratio + 0.1f));

        if(Vector3.Distance(beginning, _noteTarget) < 0.001)
        {
            Destroy(gameObject);
            return;
        }

        Vector3[] positions = {
                        beginning,
                        end
                    };
        GetComponent<LineRenderer>().SetPositions(positions);
    }
}
