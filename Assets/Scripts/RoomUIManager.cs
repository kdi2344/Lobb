using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgegap;
using UnityEngine.UI;
using TMPro;
namespace Mirror.Examples.EdgegapLobby
{
    public class RoomUIManager : NetworkBehaviour
    {
        //public GameObject[] ShowDisconnected;
        public GameObject[] ShowServer;
        public GameObject[] ShowHost;
        public GameObject[] ShowClient;
        [SerializeField] private Button stopServerBtn;
        [SerializeField] private Button stopHostBtn;
        [SerializeField] private Button stopClientBtn;
        private EdgegapLobbyKcpTransport _transport;
        [SerializeField] private Status _status;
        public TextMeshProUGUI StatusText;
        public TextMeshProUGUI roomName;
        enum Status
        {
            Offline,
            Server,
            Host,
            Client
        }

        private void Awake()
        {
            roomName.text = NetworkRoomManager.instance.RoomName;
            stopServerBtn.onClick.AddListener(() =>
            {
                NetworkRoomManager.instance.StopServer();
            });
            stopHostBtn.onClick.AddListener(() =>
            {
                NetworkRoomManager.instance.StopHost();
            });
            stopClientBtn.onClick.AddListener(() =>
            {
                NetworkRoomManager.instance.StopClient();
            });

        }

        private void Start()
        {
            _transport = (EdgegapLobbyKcpTransport)NetworkRoomManager.instance.transport;
            if (NetworkRoomManager.instance.RoomName != null)
            {
                roomName.text = NetworkRoomManager.instance.RoomName;
            }
        }
        private void Update()
        {
            var status = GetStatus();
            if (_status != status)
            {
                _status = status;
                Refresh();
            }
            if (_transport)
            {
                StatusText.text = _transport.Status.ToString();
            }
            else
            {
                StatusText.text = "";
            }
        }
        private void Refresh()
        {
            Debug.Log($"---RoomUIManager -Refresh");
            switch (_status)
            {

                case Status.Offline:
                    SetUI(ShowServer, false);
                    SetUI(ShowHost, false);
                    SetUI(ShowClient, false);
                    //SetUI(ShowDisconnected, true);
                    break;
                case Status.Server:
                    //SetUI(ShowDisconnected, false);
                    SetUI(ShowHost, false);
                    SetUI(ShowClient, false);
                    SetUI(ShowServer, true);
                    break;
                case Status.Host:
                    //SetUI(ShowDisconnected, false);
                    SetUI(ShowServer, false);
                    SetUI(ShowClient, false);
                    SetUI(ShowHost, true);
                    break;
                case Status.Client:
                    //SetUI(ShowDisconnected, false);
                    SetUI(ShowServer, false);
                    SetUI(ShowHost, false);
                    SetUI(ShowClient, true);
                    break;
                default:
                    throw new System.Exception();
                    //throw new ArgumentOutOfRangeException();
            }
        }

        private void SetUI(GameObject[] gos, bool active)
        {
            Debug.Log($"---RoomUIManager -SetUI");
            foreach (GameObject go in gos)
            {
                go.SetActive(active);
            }
        }
        private Status GetStatus()
        {
            //Debug.Log($"---UILobbyStatus -GetStatus");
            if (NetworkServer.active && NetworkClient.active)
            {
                return Status.Host;
            }
            if (NetworkServer.active)
            {
                return Status.Server;
            }
            if (NetworkClient.active)
            {
                return Status.Client;
            }
            return Status.Offline;
        }
    }
}
