from socket import socket


def sumStep(a1,a2,a3):
  b1 = 0
  b2 = 0
  b3 = 0
  string = ""

  while b1 != a3 and b2 != a3 :
    if b1 == a1 :
      b1 = 0
      string += "1:e_"
    if b2 == 0 :
      b2 = a2
      string += "2:f_"
    if b2>0:
      b3 = min(b2, a1-b1)
      b1 = b1 + b3
      b2 = b2 - b3
      string += "2:o_"
  string = string[0:len(string)-1]
  return string

sock = socket()
sock.connect(('125.235.240.166', 11223))

tmp = sock.recv(10240)
print tmp
tmp = sock.recv(10240)
print tmp
str1 = tmp.split("\n")
a1 = int(str1[len(str1)-4].split(": ")[1].strip())
a2 = int(str1[len(str1)-3].split(": ")[1].strip())
a3 = int(str1[len(str1)-2].split(": ")[1].strip())
string = sumStep(a1,a2,a3)
print string + "\n\n"
sock.send((string + "\n").encode())

while True :
  data = sock.recv(10240)
  data = sock.recv(10240)
  print data

  str2 = data.split("\n")
  if str2[2] == 'Congrats, here is your flag':
    print str2[2] + " : " + str2[3]
    break
  else:
    a1 = int(str2[3].split(": ")[1].strip())
    a2 = int(str2[4].split(": ")[1].strip())
    a3 = int(str2[5].split(": ")[1].strip())
    string = sumStep(a1,a2,a3)
    print string
    sock.send((string + "\n").encode())
