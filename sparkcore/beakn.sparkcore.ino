int red = 5;
int yellow = 6;
int green = 7;

void setup() 
{
  Spark.function("setStatus", setStatus);

  pinMode(red, OUTPUT);
  pinMode(yellow, OUTPUT);
  pinMode(green, OUTPUT);
}

void loop() 
{
}

int setStatus(String status)
{
  digitalWrite(red, LOW);
  digitalWrite(yellow, LOW);
  digitalWrite(green, LOW);
  
  if(status == "Busy" || status == "DoNotDisturb"){
    digitalWrite(red, HIGH);
  }else if(status == "Away" || status == "TemporarilyAway"){
    digitalWrite(yellow, HIGH);
  }else if (status == "Free"){
    digitalWrite(green, HIGH);
  }
  
  return 1;
}