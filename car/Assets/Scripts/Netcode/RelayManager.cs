using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : NetworkBehaviour
{
    Allocation allocation;

    public GameObject lobbyManager;

    //public ScoreImBoardManager scoreImBoardManager;

    public bool isConnected = false;

    private void Start()
    {
        //scoreImBoardManager = GameObject.Find("ScoreBoardManager").GetComponent<ScoreImBoardManager>();
    }

    public async void createRelay()
    {
        try
        {
            allocation = await RelayService.Instance.CreateAllocationAsync(10);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            lobbyManager.GetComponent<LobbyManager>().updateRelayJoinCode(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.StartHost();

            isConnected = true;
            //scoreImBoardManager.initPlayerServerRPC(OwnerClientId);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void joinRelay(string code)
    {
        try
        {
            Debug.Log(code);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(code);

            /*NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );*/

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();

            isConnected = true;

            //scoreImBoardManager.initPlayerServerRPC(OwnerClientId);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
