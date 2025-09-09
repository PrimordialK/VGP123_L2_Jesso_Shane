using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXpos;
    [SerializeField] private float maxXpos;

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //player = GameManager.Instance.playerInstance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
        if (player != null)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(player.position.x, minXpos, maxXpos);
            transform.position = pos;
        }
    }
}
