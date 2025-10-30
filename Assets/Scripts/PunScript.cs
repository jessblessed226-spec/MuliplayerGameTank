using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PunScript : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI TxtInfo;
    public Transform SpawnPoint;
    public GameObject PrefabTank;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // Synchronise la scène entre joueurs
        PhotonNetwork.GameVersion = "1.0";           // Version du jeu
        PhotonNetwork.ConnectUsingSettings();        // Connexion avec les paramètres du serveur Photon
    }

    public override void OnConnectedToMaster()
    {
        TxtInfo.text = "Connecté au serveur ! Rejoint ou crée la salle...";
        PhotonNetwork.JoinOrCreateRoom("WarRoom", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        string masterStatus = PhotonNetwork.IsMasterClient ? " (You are the Master)" : " (You are not the Master)";
        TxtInfo.text = $"Connecté à la salle : {PhotonNetwork.CurrentRoom.Name}\n" +
                       $"Joueurs : {PhotonNetwork.CurrentRoom.PlayerCount}\n" +
                       $"Rôle : {masterStatus}";
        PhotonNetwork.Instantiate(PrefabTank.name, SpawnPoint.position, Quaternion.identity,0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string masterStatus = PhotonNetwork.IsMasterClient ? "(You are the Master)" : "(You are not the Master)";
        TxtInfo.text = $"Un nouveau joueur a rejoint : {newPlayer.NickName}\n" +
                       $"Joueurs : {PhotonNetwork.CurrentRoom.PlayerCount}\n" +
                       $"Rôle : {masterStatus}";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // ✅ Cet événement manquait ! Il gère la déconnexion d’un joueur de la salle.
        string masterStatus = PhotonNetwork.IsMasterClient ? "(You are the Master)" : "(You are not the Master)";
        TxtInfo.text = $"Le joueur {otherPlayer.NickName} a quitté la salle.\n" +
                       $"Joueurs restants : {PhotonNetwork.CurrentRoom.PlayerCount}\n" +
                       $"Rôle : {masterStatus}";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        TxtInfo.text = $"Déconnecté : {cause}";
    }

    void Update()
    {
        // Optionnel : mise à jour continue de l’état réseau
        if (PhotonNetwork.InRoom)
        {
            string masterStatus = PhotonNetwork.IsMasterClient ? "(You are the Master)" : "(You are not the Master)";
            TxtInfo.text = $"Salle : {PhotonNetwork.CurrentRoom.Name}\n" +
                           $"Joueurs : {PhotonNetwork.CurrentRoom.PlayerCount}\n" +
                           $"Rôle : {masterStatus}";
        }
        else
        {
            TxtInfo.text = PhotonNetwork.NetworkClientState.ToString();
        }
    }
}
