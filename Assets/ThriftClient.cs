using System.Threading;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using UnityEngine;

public class ThriftClient : MonoBehaviour
{
    private bool _connectionOpen;
    private GameUi.Client _client;
    public void Start()
    {
        GamePlatformHandler.StartSpin += GameUiHandler_OnStartSpin;

        var thread = new Thread(StartServer);
        thread.Start();
    }

    private void GameUiHandler_OnStartSpin()
    {
        Debug.Log("Event Invoked! Thanks to Platform :)");
    }

    private void StartServer()
    {
        // Create the handler and register a callback.
        var handler = new GamePlatformHandler();
        var processor = new GamePlatform.Processor(handler);
        var serverTransport = new TServerSocket(9091);
        var server = new TSimpleServer(processor, serverTransport);

        Debug.Log("Starting Unity Server");
        server.Serve();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_connectionOpen) return;
            Debug.Log("Connecting to Platform...");
            var transport = new TSocket("localhost", 9090);
            var protocol = new TBinaryProtocol(transport);
            _client = new GameUi.Client(protocol);
            transport.Open();
            _connectionOpen = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_client != null)
            {
                Debug.Log("Sending Message");
                _client.SendMessageFromUI();
            }
        }
    }
}