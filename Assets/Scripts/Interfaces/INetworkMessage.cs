using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;

public interface INetworkMessage
{
	void Send(NetworkingPlayer player, Binary frame, NetWorker sender);		
}
