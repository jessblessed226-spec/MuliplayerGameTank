using UnityEngine;
using Photon.Pun;

public class PhotonViewRuntimeAdder : MonoBehaviour
{
    private PhotonView photonView;

    void Awake()
    {
        // Vérifie si le GameObject a déjà un PhotonView
        photonView = GetComponent<PhotonView>();
        if (photonView == null)
        {
            // 1️⃣ Ajouter le composant PhotonView
            photonView = gameObject.AddComponent<PhotonView>();
        }

        // 2️⃣ Ajouter le composant PhotonTransformView (optionnel, pour synchroniser position et rotation)
        PhotonTransformView transformSync = gameObject.GetComponent<PhotonTransformView>();
        if (transformSync == null)
        {
            transformSync = gameObject.AddComponent<PhotonTransformView>();
        }

        // 3️⃣ Configurer le PhotonView
        photonView.ObservedComponents = new System.Collections.Generic.List<Component>();
        photonView.ObservedComponents.Add(transformSync);

        // 4️⃣ Définir le mode de synchronisation
        photonView.Synchronization = ViewSynchronization.UnreliableOnChange;

        Debug.Log("PhotonView ajouté et configuré à l’exécution !");
    }

    void Update()
    {
        // Juste pour montrer : si c’est toi le propriétaire, tu peux bouger l’objet
        if (photonView.IsMine)
        {
            float move = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
            transform.Translate(move, 0, 0);
        }
    }
}
