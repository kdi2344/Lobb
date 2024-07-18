using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Edgegap {
    public class Empty : NetworkBehaviour
    {
        public Button btn;
        private EdgegapLobbyKcpTransport _transport => (EdgegapLobbyKcpTransport)NetworkRoomManager.instance.transport;
        public void CreateRoom()
        {
            NetworkRoomManager nrm = GetComponent<NetworkRoomManager>();
            nrm.CreateRoom();
        }

        public void ServerConnect()
        {
            if (NetworkRoomManager.instance.trsp == null)
            {
                if (NetworkRoomManager.instance.TryGetComponent(out Transport newTransport))
                {
                    NetworkRoomManager.instance.trsp = newTransport;
                }
            }
            
            NetworkRoomManager.instance.OnStartServer();
            GetComponent<EdgegapLobbyKcpTransport>().GiveBtn();
        }
    }
}
