import hashlib as md5

def create_HA1(username, realm, password_md5):
  string = username + ":" + realm + ":" + password_md5
  return md5.md5(bytes(string,"ascii")).hexdigest()

def create_HA1_sess(username, realm, password_md5, nonce, cnonce):
  string = username + ":" + realm + ":" + password_md5
  ha1 = (md5.md5(bytes(string,"ascii"))).hexdigest()
  string = ha1 + ":" + nonce + ":" + cnonce
  ha1_sess = (md5.md5(bytes(string,"ascii"))).hexdigest()
  return ha1_sess

def create_HA2(method, digestURI):
  string = method + ":" + digestURI
  return md5.md5(bytes(string,"ascii")).hexdigest()

def create_response(HA1, nonce, nonceCount, cnonce, qop, HA2):
  string =  HA1+":"+nonce+":"+nonceCount+":"+cnonce+":"+qop+"+"+HA2
  return md5.md5(bytes(string,"ascii")).hexdigest()

# print("HA1:      "+create_HA1("q9", "secret", "c627e19450db746b739f41b64097d449"))
# print("HA1_sess: "+create_HA1_sess("q9", "secret", "c627e19450db746b739f41b64097d449", "bbKtsfbABAA=5dad3cce7a7dd2c3335c9b400a19d6ad02df299b", "6945eb2a7ba8cf7f"))
# print("HA2:      "+create_HA2("GET:", "/~q9/"))

HA1 = "c627e19450db746b739f41b64097d449"
HA2 = create_HA2("GET", "/~q9/")
reponse = create_response("c627e19450db746b739f41b64097d449", "bbKtsfbABAA=5dad3cce7a7dd2c3335c9b400a19d6ad02df299b","00000001","9691c249745d94fc","auth", "ffffdd8b8029499600f95a69beb239c2" )
print("Reponse: "+reponse)




HA2 = "31e101310bcd7fae974b921eb148099c"
reponse = "c627e19450db746b739f41b64097d449:bbKtsfbABAA=5dad3cce7a7dd2c3335c9b400a19d6ad02df299b:00000001:9691c249745d94fc:auth:31e101310bcd7fae974b921eb148099c"
response = "c3077454ecf09ecef1d6c1201038cfaf"

reponse1 = "c627e19450db746b739f41b64097d449:Es7EPC6IBQA=531b3c11fa84aa94757c7ec7c3b17f2aa7747316:00000001:9691c249745d94fc:auth:ffffdd8b8029499600f95a69beb239c2"
