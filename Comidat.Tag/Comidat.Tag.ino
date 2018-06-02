#include <ESP8266WiFiMulti.h>

const String macId = WiFi.macAddress();
const int tagId = ESP.getChipId() & 0xFF;
const char* wifiSSID = "AKKAYA";
const char* wifiPassword = "Akkaya1996";
IPAddress serverIP(192,168,1,24);
uint16_t  serverPort = 5757;

IPAddress ip(192, 168, 1, tagId);
IPAddress gateway(192, 168, 1, 1);
IPAddress subnet(255, 255, 255, 0);
IPAddress dns(192, 168, 1, 1);

String gon = "";
int networksCount = 0;
unsigned long now = 0;
//unsigned long lastMsg = 0;
unsigned long lastConnect = 0;
ESP8266WiFiMulti wifiMulti;
WiFiClient client;

void setup() {
	Serial.begin(115200);
	delay(10);
	//Settings Wifi
	WiFi.mode(WIFI_STA);
	WiFi.config(ip, gateway, subnet, dns, dns);

	//Add AP Wifis
	wifiMulti.addAP(wifiSSID, wifiPassword);
	Serial.println();
}

void loop() {
	now = millis();
	WifiConnect();
	ServerConnect();
	SendData();
}

void WifiConnect() {
	if (WiFi.isConnected()) return;
	Serial.print("Wifi Baglaniyor");
	while ((wifiMulti.run() != WL_CONNECTED)) {
		Serial.print(".");
		delay(500);
	}
	Serial.println();
	//WiFi.printDiag(Serial);
	Serial.println("Wifi Baglandi");
}

void ServerConnect() {
	if (client.status() == ESTABLISHED) return;
	if (now - lastConnect < 5000) return;
	lastConnect = now;
	if (client.connect(serverIP, serverPort)) {
		Serial.println("Servera Baglandi");
	}
	else {
		Serial.println("Hata Servera Baglanmadi,5 saniye sonra tekrar denenicek!");
	}
}

void SendData() {
	if (client.status() != ESTABLISHED) return;
	//if (now - lastMsg < 500) return; lastMsg = now;
	networksCount = WiFi.scanNetworks(false, true);
	gon = "";
	for (int i = 0; i < networksCount && i<20; ++i)
	{
		gon += ";";
		gon += WiFi.RSSI(i);
		gon += ";";
		gon += tagId;//macId;
		gon += ";";
		gon += WiFi.BSSIDstr(i);
		//Avoid overflow buffer size
		if (i == 20) {
			client.print(gon);
			gon = "";
		}
	}
	client.print(gon);
}

