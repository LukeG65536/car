using Unity.Netcode.Components;
using UnityEngine;

namespace Unity.Multiplayer.Samples.Utilities.ClientAuthority
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()  //overide to make it so client can move itself
        {
            return false;  //woulnt recomend doing this unless ur game isnt very competitive
        }
    }
}