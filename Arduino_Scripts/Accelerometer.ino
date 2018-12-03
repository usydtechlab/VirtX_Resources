const int zAxis = A2;
const int yAxis = A1;
const int xAxis = A0;

int zHigh = 419, zLow = 283,
        xHigh = 399, xLow = 264,
        yHigh = 396, yLow = 260;

void setup() {
  Serial.begin(9600);
}

void loop() {

  short angleX = map( constrain (  analogRead(xAxis), xLow, xHigh ), xLow, xHigh, -90, 90  );
  short angleY = map( constrain (  analogRead(yAxis), yLow, yHigh ), yLow, yHigh, -90, 90  );
  short angleZ = map( constrain (  analogRead(zAxis), zLow, zHigh ), zLow, zHigh, -90, 90  );

  String dataString = String(angleX);

  dataString += ",";
  dataString += String(angleY);
  dataString += ",";
  dataString += String(angleZ);

  Serial.println(dataString);

}
