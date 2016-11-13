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
        public Vector3 TargetRotation;
        private Quaternion targetRotation;
        // TargetScale ?
        
        private Vector3 startPosition;
        private Quaternion startRotation;
    
    #endregion
    
    private float currentTime = 0f;

    private bool moved = false;
    public string soundName;
    
    void Start()
    {
        currentTime = UnityEngine.Random.Range(0.0f,closedTime);
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        targetRotation = Quaternion.Euler(TargetRotation);
    }
    
    void Update(){
        currentTime += Time.deltaTime;
        currentTime -= (int)(currentTime/(closedTime + openingTime + openTime + closingTime))*(closedTime + openingTime + openTime + closingTime);
        if(currentTime<closedTime){
            moved = false;
            transform.localPosition = startPosition;
            transform.localRotation = startRotation;
        }
        else if(currentTime < (closedTime + openingTime)){
            if (!moved)
                AkSoundEngine.PostEvent(soundName, gameObject);
            moved = true;
            transform.localPosition = Vector3.Lerp(startPosition,targetPosition,(currentTime-closedTime)/openingTime);
            transform.localRotation = Quaternion.Slerp(startRotation,targetRotation,(currentTime-closedTime)/openingTime);
        }
        else if(currentTime < (closedTime + openingTime + openTime)){
            moved = false;

            transform.localPosition = targetPosition;
            transform.localRotation = targetRotation;
        }
        else{
            if (!moved)
                AkSoundEngine.PostEvent(soundName, gameObject);
            moved = true;
            transform.localPosition = Vector3.Lerp(targetPosition,startPosition,(currentTime-closedTime-openingTime-openTime)/closingTime);
            transform.localRotation = Quaternion.Slerp(targetRotation,startRotation,(currentTime-closedTime-openingTime-openTime)/closingTime);
        }
    }
    
    public void Reset(){
        AkSoundEngine.PostEvent(soundName, gameObject);
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        enabled = false;
    }
}