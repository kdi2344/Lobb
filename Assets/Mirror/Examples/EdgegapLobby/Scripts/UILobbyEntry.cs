using System;
using System.Collections;
using System.Collections.Generic;
using Edgegap;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.EdgegapLobby
{
    public class UILobbyEntry : MonoBehaviour
    {
        public Button JoinButton;
        public Text Name;
        public Text PlayerCount;

        private LobbyBrief _lobby;
        private UILobbyList _list;
        private void Awake()
        {
            JoinButton.onClick.AddListener(() =>
            {
                _list.Join(_lobby);
            });
        }

        public void Init(UILobbyList list, LobbyBrief lobby, bool active = true)
        {
            Debug.Log($"---UILobbyEntry -Init");
            //방 버튼 하나하나 정보 입력
            gameObject.SetActive(active && lobby.is_joinable);
            JoinButton.interactable = lobby.available_slots > 0;
            _list = list;
            _lobby = lobby;
            Name.text = lobby.name;
            PlayerCount.text = $"{lobby.player_count}/{lobby.capacity}";
        }
    }

}
