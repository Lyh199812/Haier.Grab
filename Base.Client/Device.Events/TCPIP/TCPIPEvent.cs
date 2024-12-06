namespace Device.Events.TCPIP
{
    public class DeviceDataReceivedEvent : PubSubEvent<DataReceivedEventArgs> { }
    public class ClientConnectedEvent : PubSubEvent<ClientEventArgs> { }

    public class ClientDisconnectedEvent : PubSubEvent<ClientEventArgs> { }
    public class ClientDisconnectedManuallyEvent : PubSubEvent<ClientEventArgs> { }
    
}
