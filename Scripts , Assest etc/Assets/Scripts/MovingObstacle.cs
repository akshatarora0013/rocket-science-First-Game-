using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 5f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){
            return;
        }
        const float tau = Mathf.PI * 2;
        float cycle = Time.time/period;
        float rawSineWave = Mathf.Sin(cycle * tau);   
        movementFactor = (rawSineWave + 1f) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        
        
    }
}
