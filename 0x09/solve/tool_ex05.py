import subprocess

list_password = []
def get_passwor():
  MyOut = subprocess.Popen(['strings','-e l /0x09/FLEGGO/1BpnGjHOT7h5vvZsV4vISSb60Xj3pX5G.exe'],stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  print(stdout)
  print(stderr)

get_passwor()
