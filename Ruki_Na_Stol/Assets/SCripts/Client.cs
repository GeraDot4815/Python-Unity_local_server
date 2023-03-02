using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
    class Client : MonoBehaviour
    {
    [SerializeField] private InputField _input_field;
    [SerializeField] private Text _text;
        // ����� � ���� �������, � �������� ����� ������������
    static int port = 8080; // ���� �������
    static string address = "127.0.0.1"; // ����� �������

    private Socket socket=null;
    private IPEndPoint ipPoint;
    private byte[] data;
    private void Start()
    {
        ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // ������������ � ���������� �����
        socket.Connect(ipPoint);
    }
    private void Update()
    {
        Debug.Log("Let's go!");
        // �������� �����
        data = new byte[1024]; // ����� ��� ������

        StringBuilder builder = new StringBuilder();
        int bytes = 0; // ���������� ���������� ����

        //StartCoroutine(Connect_To_Server());
        do
        {
            bytes = socket.Receive(data, data.Length, 0);
            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
        }
        while (socket.Available > 0);
        Debug.Log(builder.ToString());
        _text.text = "����� �������: " + builder.ToString();
        data = Encoding.UTF8.GetBytes("OK");
        socket.Send(data);
        Debug.Log("Sended");
        // ��������� �����
        //socket.Shutdown(SocketShutdown.Both);
        //socket.Close();
    }
    private IEnumerator Connect_To_Server()
    {
        Debug.Log("Let's go!");
        if (socket == null)
        {
            Debug.Log("Created");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // ������������ � ���������� �����
            socket.Connect(ipPoint);
            yield return new WaitForEndOfFrame();
        }
    }
}
