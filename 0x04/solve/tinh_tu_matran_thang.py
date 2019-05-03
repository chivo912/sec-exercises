from socket import socket

dic = {'1': ['  #',
             '  #',
             '  #',
             '  #',
             '  #'],

       '2': ['###',
             '  #',
             '###',
             '#  ',
             '###'],

       '3': ['###',
             '  #',
             '###',
             '  #',
             '###'],

       '4': ['# #',
             '# #',
             '###',
             '  #',
             '  #'],

       '5': ['###',
             '#  ',
             '###',
             '  #',
             '###'],

       '6': ['###',
             '#  ',
             '###',
             '# #',
             '###'],

       '7': ['###',
             '  #',
             '  #',
             '  #',
             '  #'],

       '8': ['###',
             '# #',
             '###',
             '# #',
             '###'],

       '9': ['###',
             '# #',
             '###',
             '  #',
             '###'],

       '0': ['###',
             '# #',
             '# #',
             '# #',
             '###'],

       '+': ['   ',
             ' # ',
             '###',
             ' # ',
             '   '],

       '-': ['   ',
             '   ',
             '###',
             '   ',
             '   '],

       '*': ['   ',
             '# #',
             ' # ',
             '# #',
             '   '],

       '/': ['   ',
             '  #',
             ' # ',
             '#  ',
             '   ']
      }


def caculator_string(s):
    lines = str(s).splitlines()
    result = ''
    leng = len(lines[0]) / 5
    for m in range(leng):
        temp = []
        for n in range(5):
            temp.append(lines[n][m * 5:m * 5 + 3])
        for key in dic:
            if (dic[key] == temp):
                result += key
    return eval(result)

sock = socket()
sock.connect(('188.166.218.1', 2016))

while True:
  data = sock.recv(10240)
  print data
  if "GOOD JOB!" in data :
    break
  else:
    data = sock.recv(10240)
    print(data)
    result = caculator_string(data)
    print result
    sock.send((str(result) + "\n").encode())
