def check_flag(flag):
  a = 256
  n = 10 ** 60
  l = len(flag)
  if l != 22:
    return False
  else:
    result = 0
    for i in range(0, l):
      result += ord(flag[i]) * a ** (l - i - 1) % n
    return result == 39830963251313012931406054205649358377525286926249590L
def main():
    flag = '0000000000000000000000'
    for i in range(0, 22):
        result1 = 0
        for j in range(48, 127):
            print (flag)
            flag = list(flag)
            flag[i] = chr(j)
            flag = ''.join(flag)
            result1 = check(flag)
            print (result1)
            if result1 > 39830963251313012931406054205649358377525286926249590:
                flag = list(flag)
                flag[i] = chr(j-1)
                flag = ''.join(flag)
                print(flag)
                break
    print (flag)
