arr_a = {3571:2963, 2843:215, 30243:13059}
arr_b = {80735:51964, 8681:2552, 40624:30931}
arr_c = {99892:92228, 45629:1080, 24497:12651}
arr_d = {54750:26981, 99627:79040, 84339:77510}

def findByMod(div1, mod1, div2, mod2, div3, mod3):
  i = 1
  j = 1
  k = 1
  number1 = div1*i+mod1
  number2 = div2*j+mod2
  number3 = div3*k+mod3
  while number1 != number2 or number2 != number3 or number1 != number3:
    min_number = min(number1,number2,number3)
    if min_number == number1:
      i+=1
    elif min_number == number2:
      j+=1
    else:
      k+=1
    number1 = div1*i+mod1
    number2 = div2*j+mod2
    number3 = div3*k+mod3
  return number1

def findChar(number):
  for a in range(32,128):
    for b in range(32,128):
      for c in range(32,128):
        if (a | (b << 8) | (c << 16)) == number:
          return str(chr(a))+str(chr(b))+str(chr(c))

a = findByMod(3571, 2963 ,2843 , 215, 30243 ,13059 )
b = findByMod(80735, 51964 , 8681, 2552, 40624 , 30931)
c = findByMod(99892, 92228 , 45629, 1080, 24497 , 12651)
d = findByMod(54750, 26981 , 99627, 79040, 84339 , 77510)

# print(a)
# print(b)
# print(c)
# print(d)


a = findChar(a)
b = findChar(b)
c = findChar(c)
d = findChar(d)

arr = [a,b,c,d]
print(arr)

result = ""
for i in range(3):
  result += arr[0][i] + arr[1][i] + arr[2][i] + arr[3][i]

print(result)


# H0W
# 3M3
# r3L
# 3_K
# W3LK0M3_H3r3

# print(32 | (42 << 8) | (61 << 16))
