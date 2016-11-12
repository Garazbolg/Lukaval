using System;
using UnityEngine;

public class KitchenFourniture : MonoBehaviour
{
    ///Time CLosed In Seconds
    public float closedTime = 3f;
    
    ///Time the fourniture stays open in Seconds
    public float openTime = 2f;
    
    ///Time the fourniture takes to open in seconds
    public float openingTime = 0.5f;
    
    ///Time the fourniture takes to close in seconds
    public float closingTime = 0.5f;
    
    #region TargetTransform
    
        public Vector3 targetPosition;
        public Quaternion targetRotation;
        // TargetScale ?
        
        public Vector3 startPosition;
        public Quaternion startRotation;
    
    #endregion
    
    private float currentTime = 0f;
    
    void Start()
    {
        currentTime = UnityEngine.Random.Range(0.0f,closedTime);
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    
    void Update(){
        currentTime += Time.deltaTime;
        currentTime -= (int)(currentTime/(closedTime + openingTime + openTime + closingTime))*(closedTime + openingTime + openTime + closingTime);
        if(currentTime<closedTime){
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
        else if(currentTime < closedTime + openingTime){
            transform.position = Vector3.Lerp(startPosition,targetPosition,(currentTime-closedTime)/openingTime);
            transform.rotation = Quaternion.Slerp(startRotation,targetRotation,(currentTime-closedTime)/openingTime);
        }
        else if(currentTime < closedTime + openingTime + openTime){
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
        else{
            transform.position = Vector3.Lerp(targetPosition,startPosition,(currentTime-closedTime-openingTime-openTime)/closingTime);
            transform.rotation = Quaternion.Slerp(targetRotation,startRotation,(currentTime-closedTime-openingTime-openTime)/closingTime);
        }
    }
    
    public void Reset(){
     transform.position = startPosition;
     transform.rotation = startRotation;
    }
}