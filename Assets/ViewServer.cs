using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewServer : MonoBehaviour
{

    private Server server;

    public void SetServer(Server s)
    {

        server = s;

        UpdateUI();
        
        CancelInvoke();
        InvokeRepeating(nameof(PingServer),0f,5f);

    }
    
    public void ConnectToServer()
    {

        MenuButtons.usernameEntered = server.username;
        Client.instance.ConnectToServer(server.ip);
        
    }

    private void UpdateUI()
    {

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = server.ip;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = server.username;

    }

    private async void PingServer()
    {

        if (await PingHost())
        {
            
            transform.GetChild(2).GetComponent<Image>().color = Color.green;
            GetComponent<Button>().interactable = true;

        }
        else
        {
            
            transform.GetChild(2).GetComponent<Image>().color = Color.red;
            GetComponent<Button>().interactable = false;
            
        }
        
    }
    
    private async Task<bool> PingHost()
    {
        try
        {

            //parse localhost
            string sendIp = string.Equals(server.ip, "localhost", StringComparison.CurrentCultureIgnoreCase) ? "127.0.0.1" : server.ip;
                
            using (var client = new TcpClient())
            {
                client.SendTimeout = 2000;
                client.ReceiveTimeout = 2000;
                await client.ConnectAsync(IPAddress.Parse(sendIp), Client.port);
                return true;

            }

        }
        catch (FormatException)
        {
            
            ErrorDisplayer.Log("U didnt enter an ip address >:(");
            return false;

        }
        catch (Exception ex)
        {
            ErrorDisplayer.Log($"Error pinging host:'" + server.ip + ":" + Client.port + $"', ex: {ex}");
            return false;
        }
    }
}
