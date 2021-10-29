using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExchangeSharp;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var task = Task.Run(async () => {
                while (true)
                {
                    List<string> GateIoCoinNames = new List<string>() { "link_usdt", "dot_usdt", "ada_usdt", "xtz_usdt", "trx_usdt" };
                    List<string> KrakenCoinNames = new List<string>() { "LINKUSD", "DOTUSD", "ADAUSD", "XTZUSD", "TRXUSD" };

                    List<string> ExchangeNames = new List<string>() { ExchangeName.GateIo, ExchangeName.Kraken };

                    Dictionary<string, List<string>> CoinNames = new Dictionary<string, List<string>>();

                    CoinNames[ExchangeName.GateIo] = GateIoCoinNames;
                    CoinNames[ExchangeName.Kraken] = KrakenCoinNames;

                    foreach(var ExchangeName in ExchangeNames)
                    {
                        var CurExchangeAPI = await ExchangeAPI.GetExchangeAPIAsync(ExchangeName);

                        for(int i = 0; i < CoinNames[ExchangeName].Count; i++)
                        {
                            var CurExchangeTicker = await CurExchangeAPI.GetTickerAsync(CoinNames[ExchangeName][i]);

                            UpdateTextBox($"{ExchangeName} bid: {CurExchangeTicker.Bid} ask: {CurExchangeTicker.Ask}", ExchangeName, i);
                        }
                    }

                    //Thread.Sleep(100);


                    /*
                    var GateIoApi = await ExchangeAPI.GetExchangeAPIAsync(ExchangeName.GateIo);

                    List<string> GateIoPrices = new List<string>();

                    foreach(var GateIoCoinName in GateIoCoinNames)
                    {
                        var GateIoTicker = await GateIoApi.GetTickerAsync(GateIoCoinName);
                        GateIoPrices.Add($"GateIo {GateIoTicker.MarketSymbol} bid: {GateIoTicker.Bid} ask: {GateIoTicker.Ask}");
                    }

                    var KrakenApi = await ExchangeAPI.GetExchangeAPIAsync(ExchangeName.Kraken);

                    List<string> KrakenPrices = new List<string>();

                    foreach(var KrakenCoinName in KrakenCoinNames)
                    {
                        var KrakenTicker = await KrakenApi.GetTickerAsync(KrakenCoinName);
                        KrakenPrices.Add($"Kraken {KrakenTicker.MarketSymbol} bid: {KrakenTicker.Bid} ask: {KrakenTicker.Ask}");
                    }

                    UpdateKrakenTextBox(KrakenPrices);
                    UpdateGateIoTextBox(GateIoPrices);

                    Thread.Sleep(100);

                    */
                }
            });
        }

        public void UpdateTextBox(string value, string TextBoxExchangeName, int TextBoxNum)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string, int>(UpdateTextBox), new object[] { value, TextBoxExchangeName, TextBoxNum });
                return;
            }

            if(TextBoxExchangeName == ExchangeName.GateIo)
            {
                switch (TextBoxNum)
                {
                    case 0:
                        GateIoLinkTextBox.Text = value;
                        break;
                    case 1:
                        GateIoDotTextBox.Text = value;
                        break;
                    case 2:
                        GateIoAdaTextBox.Text = value;
                        break;
                    case 3:
                        GateIoXtzTextBox.Text = value;
                        break;
                    case 4:
                        GateIoTrxTextBox.Text = value;
                        break;
                }
            }else if (TextBoxExchangeName == ExchangeName.Kraken)
            {
                switch (TextBoxNum)
                {
                    case 0:
                        KrakenLinkTextBox.Text = value;
                        break;
                    case 1:
                        KrakenDotTextBox.Text = value;
                        break;
                    case 2:
                        KrakenAdaTextBox.Text = value;
                        break;
                    case 3:
                        KrakenXtzTextBox.Text = value;
                        break;
                    case 4:
                        KrakenTrxTextBox.Text = value;
                        break;
                }
            }
        }

    }
}
