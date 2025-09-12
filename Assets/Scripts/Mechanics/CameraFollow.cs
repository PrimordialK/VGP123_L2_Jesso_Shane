using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

     private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnPlayerControllerCreated += (playerController) => target = playerController.transform;

        // Immediately set target if player already exists
        if (GameManager.Instance.playerInstance != null)
        {
            target = GameManager.Instance.playerInstance.transform;
        }
    }
  
    //private void PlayerControllerCreated(PlayerController playerInstance)
    //{
    //    target = playerInstance.transform;
    //}

    void Update()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        transform.position = pos;

        
    }
}