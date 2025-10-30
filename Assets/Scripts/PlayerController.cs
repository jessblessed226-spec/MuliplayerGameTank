using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    PhotonView view;
    public int Speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Speed * Time.deltaTime);
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Speed * 40 * Time.deltaTime);
        }
        
    }
}
