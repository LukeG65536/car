using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System;
using Unity.Netcode;
using UnityEngine.UIElements;

public class LobbyManager : NetworkBehaviour 
{
    public Lobby hostLobby;
    private float heartBeatTimer = 10f;
    private float updateLobbyTimer = 1.1f;
    public GameObject relayManager;
    public int maxPlayers = 20;
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync(); 

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log(AuthenticationService.Instance.PlayerId);   //sign in and print id
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync(); 
    }

    private void Update()
    {
        heartBeat(); //keep the lobby alive
        updateLobby();  //update lobby
    }

    private async void heartBeat()
    {
        heartBeatTimer -= Time.deltaTime;

        if (hostLobby != null && IsHost)
        {
            if(heartBeatTimer < 0)
            {
                heartBeatTimer = 10f;

                //Debug.Log("Thump");

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id); //send heartbead every 10 sec
            }
        }
    }

    public async void createLobby()
    {
        string lobbyName = "ff" ;
        try
        {
            CreateLobbyOptions lobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    { "RelayCode", new DataObject(DataObject.VisibilityOptions.Member, "0") },
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Member, "None" )}
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, lobbyOptions);

            hostLobby = lobby;

            Debug.Log(lobby.Name);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void listLobbys()
    {
        QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync();
        Debug.Log(response.Results.Count);
        foreach(Lobby lobby in response.Results)
        {
            Debug.Log(lobby.Name);
        }
    }

    public async void joinLobby()
    {
        QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync();

        if(response.Results.Count != 0)
        {
            foreach (Lobby lobby in response.Results)
            {
                if(lobby.AvailableSlots != 0)
                {
                    hostLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
                    string code = hostLobby.Data["RelayCode"].Value;
                    Debug.Log(code);
                    relayManager.GetComponent<RelayManager>().joinRelay(code);
                    return;
                }
            }
        }
        Debug.Log("No Lobbys Found   :( ");
    }

    public async void updateRelayJoinCode(string newCode)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    { "RelayCode", new DataObject(DataObject.VisibilityOptions.Member, newCode)}
                }
            });
        } 
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void updateLobbyWithOptions(UpdateLobbyOptions updateLobbyOptions)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, updateLobbyOptions);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void updateLobby()
    {
        updateLobbyTimer -= Time.deltaTime;

        if (hostLobby == null) return;

        if(updateLobbyTimer < 0)
        {
            updateLobbyTimer = 1.1f;

            hostLobby = await LobbyService.Instance.GetLobbyAsync(hostLobby.Id);

            //Debug.Log("Lobby Updated");
        }
    }
    private Player GetPlayer() ///not using this stuff anymore look at playerStatManager
    {
        return new Player(AuthenticationService.Instance.PlayerId, null, new Dictionary<string, PlayerDataObject> {
            {"Name", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, "Default Player Name")},
            {"Score", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, new int().ToString())},
            {"Kills", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, new int().ToString())},
            {"Deaths", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, new int().ToString())}
        });
    }

    public async void UpdatePlayer(UpdatePlayerOptions updatePlayerOptions)
    {
        if(hostLobby != null)
        {
            try
            {
                hostLobby = await LobbyService.Instance.UpdatePlayerAsync(hostLobby.Id, AuthenticationService.Instance.PlayerId, updatePlayerOptions);
            }
            catch(LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
}
