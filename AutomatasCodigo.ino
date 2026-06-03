#include <WiFi.h>
#include <WebServer.h>

const char* ssid = "Meso";
const char* password = "Un1f1M3so";

WebServer server(80);

int q0 = 0;
char q1 = ' ';

const int pinBomba1 = 15; //23
const int pinBomba2 = 2; //24
const int pinBomba3 = 0; //25

const int pinBoton1 = 12; //13
const int pinBoton2 = 14; //12
const int pinBoton3 = 27; //11
const int pinBoton4 = 26; //10

const int pinTrig = 18;
const int pinEco = 19;

void setup() {

  // put your setup code here, to run once:
  pinMode(pinBomba1, OUTPUT);
  pinMode(pinBomba2, OUTPUT);
  pinMode(pinBomba3, OUTPUT);

  pinMode(pinBoton1, INPUT_PULLDOWN);
  pinMode(pinBoton2, INPUT_PULLDOWN);
  pinMode(pinBoton3, INPUT_PULLDOWN);
  pinMode(pinBoton4, INPUT_PULLDOWN);

  digitalWrite(pinBomba1, LOW);
  digitalWrite(pinBomba2, LOW);
  digitalWrite(pinBomba3, LOW);

  pinMode(pinTrig, OUTPUT);
  pinMode(pinEco, INPUT);

  //Conexion con Wafai
  Serial.begin(115200);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Conectando a WiFi...");
    Serial.println("IP:");
    Serial.println(WiFi.localIP());

      server.begin();
  }
}

void loop() {
  server.handleClient();
  // put your main code here, to run repeatedly:

  if(digitalRead(pinBoton4) == HIGH){
    q0 = 1;
  }
  else{
    q0 = 0;
  }

  while(q0 == 1){
    if(digitalRead(pinBoton1) == HIGH){
      q1 = 'a';
      Serial.println("Uno");
      dispensar();
    }
    else if(digitalRead(pinBoton2) == HIGH){
      q1 = 'b';
      Serial.println("Dos");
      dispensar();
    }
    else if(digitalRead(pinBoton3) == HIGH){
      q1 = 'c';
      Serial.println("Tres");
      dispensar();
    }
    else if(digitalRead(pinBoton4) == HIGH){
      q0 = 0;
    }
    else{
      q1 = ' ';
    }
  }
}

//Funciones
void dispensar(){
  switch(q1)
  {
    case 'a': digitalWrite(pinBomba1, HIGH); delay(4000); digitalWrite(pinBomba1, LOW); dealy(1000); break;
    case 'b': digitalWrite(pinBomba2, HIGH); delay(4000); digitalWrite(pinBomba2, LOW); delay(1000); break;
    case 'c': digitalWrite(pinBomba3, HIGH); delay(4000); digitalWrite(pinBomba3, LOW); dealy(1000); break;
  };
}

bool detectarVaso() {
  digitalWrite(pinTrig, LOW);
  delayMicroseconds(2);
  
  digitalWrite(pinTrig, HIGH);
  delayMicroseconds(10);
  digitalWrite(pinTrig, LOW);

  long duracion = pulseIn(pinEco, HIGH, 30000); 
  
  if (duracion == 0) {
    return false;
  }
  
  float distancia = (duracion * 0.0343) / 2;

  if (distancia > 0 && distancia <= 5.0) {
    return true;
  } else {
    return false;
  }
}