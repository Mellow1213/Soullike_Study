using UnityEngine;

public class DebugTime : MonoBehaviour
{
    [Range(0, 3f)]
    public float scaleValue = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = scaleValue;
    }
}
