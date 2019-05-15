public class Network
{
    public string NetWorkStatus(NetworkStatus status)
    {
        return status == NetworkStatus.Online ? "<color=#00ff00ff>Online</color>" : "<color=#ff0000ff>Offline</color>";
    }
}