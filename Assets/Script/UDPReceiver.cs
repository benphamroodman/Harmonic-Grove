using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class UDPReceiver : MonoBehaviour
{
    private SystemController systemController;
    public string UDP_LISTEN_IP;
    public int UDP_LISTEN_PORT_IS_KNOCKING;
    public int UDP_LISTEN_PORT_MATERIAL;
    private bool isKnocking;
    private string material;
    private UdpClient isKnockingUdpClient;
    private UdpClient materialUdpClient;
    private Thread receiveIsKnockingThread;
    private Thread receiveMaterialThread;

    // Start is called before the first frame update
    void Start()
    {
        systemController = GameObject.Find("System").GetComponent<SystemController>();

        UDP_LISTEN_IP = "127.0.0.1";
        Debug.Log("Starting to Receive UDP Material on " + UDP_LISTEN_IP + "...");
        
        isKnocking = false;
        UDP_LISTEN_PORT_IS_KNOCKING = 5006;
        isKnockingUdpClient = new UdpClient(UDP_LISTEN_PORT_IS_KNOCKING, AddressFamily.InterNetwork); // 監聽端口
        receiveIsKnockingThread = new Thread(new ThreadStart(ReceiveIsKnockingData));
        receiveIsKnockingThread.IsBackground = true;
        receiveIsKnockingThread.Start();
        Debug.LogWarning("Starting to Receive UDP isKnocking at port " + UDP_LISTEN_PORT_IS_KNOCKING + "... Done");
        
        material = "";
        UDP_LISTEN_PORT_MATERIAL = 5005;
        materialUdpClient = new UdpClient(UDP_LISTEN_PORT_MATERIAL, AddressFamily.InterNetwork); // 監聽端口
        receiveMaterialThread = new Thread(new ThreadStart(ReceiveMaterialData));
        receiveMaterialThread.IsBackground = true;
        receiveMaterialThread.Start();
        Debug.LogWarning("Starting to Receive UDP material at port " + UDP_LISTEN_PORT_MATERIAL + "... Done");
    }
	private void FixedUpdate() {
        systemController.isKnocking = isKnocking;
        systemController.material = material;
	}

	void OnDestroy() {
        if (receiveIsKnockingThread != null) {
            receiveIsKnockingThread.Abort();
        }
        if (receiveMaterialThread != null) {
            receiveMaterialThread.Abort();
        }
        isKnockingUdpClient.Close();
        materialUdpClient.Close();
    }

    private void ReceiveIsKnockingData() {
        while (true) {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(UDP_LISTEN_IP), UDP_LISTEN_PORT_IS_KNOCKING);
            var receivedResults = isKnockingUdpClient.Receive(ref remoteEndPoint);
            string message = Encoding.UTF8.GetString(receivedResults);
            isKnocking = System.String.Equals(message, "Yes");
            Debug.LogWarning("Received isKnocking UDP message: " + isKnocking);
        }
    }

    private void ReceiveMaterialData() {
        while (true) {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(UDP_LISTEN_IP), UDP_LISTEN_PORT_MATERIAL);
            var receivedResults = materialUdpClient.Receive(ref remoteEndPoint);
            string message = Encoding.UTF8.GetString(receivedResults);
            Debug.LogWarning("Received material UDP message: " + message);
            material = message;
        }
    }
}
