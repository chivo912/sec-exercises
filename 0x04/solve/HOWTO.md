# Bài 1
- Đề bài là thuật toán đổ nước với 3 bình
- code được để trong file ```do_Nuoc.py``` trong cùng thư mục solve

  ```python
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
  ```
- có thể  giải thích code như sau:
### + bước 1 :
  - định nghĩa một hàm với 3 tham số đầu vào
    ```python
    def sumStep(a1,a2,a3):
    ```
  + với a1,a2,a3 tương ứng với thể tích bình 1, bình 2 , bình 3

  - tính các bước phải đổ để thỏa mãn đến khi nào 2 cốc có thể tích bằng cốc thứ 3 thì dừng lại :
    ```python
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
    ```
  - do bước cuối cùng sẽ thừa dấu gạch dưới nên ta sử dụng subString để cắt lấy phần phía trước:
    ```python
      string = string[0:len(string)-1]
      return string
    ```
### + bước 2 :
  - Tạo socket đến địa chỉ = **125.235.240.166** với port = **11223**

    ```python
    sock = socket()
    sock.connect(('125.235.240.166', 11223))
    ```
  - Do lần kết nối đầu tiên sẽ bao gôm cả đề bài và thể tích tương ứng của 3 cốc nên ta phải cắt riêng cho lần đầu tiên:
     + thể tích 3 bình có dạng:
    ```
      1: 14
      2: 22
      z: 18
      op>
    ```

    ```python
      str1 = tmp.split("\n")
      a1 = int(str1[len(str1)-4].split(": ")[1].strip())
      a2 = int(str1[len(str1)-3].split(": ")[1].strip())
      a3 = int(str1[len(str1)-2].split(": ")[1].strip())

      string = sumStep(a1,a2,a3)

      print string + "\n\n"
      sock.send((string + "\n").encode())
    ```
  + ta sẽ lấy từ cuối chuỗi trở lên và cắt ra từng giá trị tương ứng

### + bước 3 :
- Thực hiện tương tự đến khi nào xuất hiện Flag thì dừng lại
- Do bắt đầu từ bài 2 trở đi thì nó sẽ dạng đề bài:
  ```
    14 2: 18
    Correct!
    Round 1
    1: 261
    2: 351
    z: 306
  ```
- Sẽ cắt chuỗi đến khi thấy flag sẽ dừng vòng lặp và show ra kết quả:

  ```python
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
  ```
- Kết quả cuối cùng ta được Flag:
  ```
    Congrats, here is your flag
    matesctf{f1ll_d4t_b0ttl3!}
  ```

# Bài 2:
- Đề bào là trò chơi Sudoku tìm ra giá trị của các ô đang bị ẩn
- Thuật toán này đã có rất nhiều trên mạng công việc của chúng ta bây giờ là phải format làm sao cho Input phải phù hợp với hàm.
 ```python
    puzzle.read(data.replace('_', '?'))
    print(data.replace('_', '?'))
 ```
- Dữ liệu server gửi:
  ```
    _ 3 _ _ _ _ _ _ 8
    _ _ 4 _ _ _ _ _ _
    _ _ 7 _ _ _ 3 5 _
    _ _ _ 1 7 _ _ _ 9
    5 2 _ _ 6 4 _ 1 _
    _ _ _ 3 _ _ _ _ _
    _ _ 1 _ 4 _ _ _ _
    _ _ _ 6 _ _ 4 2 _
    _ 8 _ 2 _ 5
  ```
- Dữ liệu sau khi format để đưa vào hàm tính toán:
  ```
    ? 3 ? ? ? ? ? ? 8
    ? ? 4 ? ? ? ? ? ?
    ? ? 7 ? ? ? 3 5 ?
    ? ? ? 1 7 ? ? ? 9
    5 2 ? ? 6 4 ? 1 ?
    ? ? ? 3 ? ? ? ? ?
    ? ? 1 ? 4 ? ? ? ?
    ? ? ? 6 ? ? 4 2 ?
    ? 8 ? 2 ? 5 ? ? ?
  ```
- Hàm sau khi tính toán sẽ đưa ra kết quả:
  ```
    Solving . . . hard . . . done
    6 3 2 5 9 7 1 4 8
    1 5 4 8 3 6 9 7 2
    8 9 7 4 2 1 3 5 6
    3 4 6 1 7 2 5 8 9
    5 2 8 9 6 4 7 1 3
    7 1 9 3 5 8 2 6 4
    2 6 1 7 4 9 8 3 5
    9 7 5 6 8 3 4 2 1
    4 8 3 2 1 5 6 9 7
  ```

- Sau khi đã tính toán xong ta sẽ gửi trả server
- Cứ như vậy đến khi check thấy có flag thì ta dừng lại
- Flag thu được là :
  ```
    Flag{well_done_sudoku_master}
  ```
# Bài 3:
- Đề bài là sẽ đưa ra một ma trận với các dấu thăng và khoảng trắng. Từ ma trận đó phải đưa ra được phép tính và tính ra kết quả.

#### + Bước 1 :
 - Từ chuỗi gốc sẽ cắt ra theo từng dòng. Và ý tưởng sẽ là từ các dòng đơn lẻ đó ta đưa chúng về dạng ma trận cho từng số và so sánh với một mẫu có sẵn để đưa ra giá trị của số đố ở dạng số.
 - Ta nhận thấy mỗi số trong phép toán thường có 2 chữ số và một phép toán ở giữa. Các số các nhau bằng 2 khoảng trắng và kích thước mốĩ số sẽ là 3 cột
 - Nên ta sẽ chia ma trận thành các khối 5x5 trong đó 3 cột là số và 2 cột là khoảng trắng cứ
    ```
      ###  ###         #    #
      #    #           #    #
      ###  ###  ###    #    #
      # #  # #         #    #
      ###  ###         #    #
    ```
  - Sau khi xác định được số hay phép toán ta chỉ việc nối chuỗi cho chúng vào chuỗi kết quả. Rồi sử dụng hàm eval() trong python để thực hiện phép toán.
#### + Bước 2:
  - Sau khi đã có ý tưởng ta tiến hành đi vào viết code
  - Đầu tiên tạo 1 mẫu sẵn các chuỗi bao gồm các số và phép toán theo dạng key-value để sau ta sẽ dùng để so sánh.
  - Tiếp đến là viết hàm tạo ma trận 5x5 và tìm ra phép toán
    ```python
      leng = len(lines[0]) / 5
    ```
  - Xác định xem sẽ bao nhiêu khối 5x5
  - sau khi đã xác định được số lượng khối ta sẽ đi vào xác định số hay phép toán cho từng khối
    ```python
      temp.append(lines[n][m * 5:m * 5 + 3])
    ```
    ```python
      for key in dic:
              if (dic[key] == temp):
                  result += key
    ```
  - Sau khi đã xác định được phép toán sẽ tính toán và trả về giá trị
    ```python
      return eval(result)
    ```

#### + Bước 3:
- Sau khi đã tạo hàm tính toán xong chúng ta sẽ đến bước tạo socket với server tính toán trả về kết quả cho server đến khi nào thấy cờ sẽ dừng lại
```python
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
```
- Kết quả sẽ thấy 1 chuỗi **"GOOD JOB!"** và bên dưới là 1 ma trận nếu ta zoom thật nhỏ lại sẽ thấy chữ

  ```
    Flag{such_a_hard_worker}
  ```

# Bài 4
- Sau khi đọc mã nguồn ta có thể nhìn ra hướng giải đó chính là tính ngược lại được 4 số  `a,b,c,d` từ các phép so sánh chia dư dưới kia.
- Đầu tiên sẽ viết một hàm tìm số từ các `số chia` và `phần dư` kia
  ```python
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
  ```
- Với `div` là các số chia còn `mod` là phần dư tương ứng như trong mã nguồn
- Ta sẽ tính đước bốn số trên
  ```python
    a = findByMod(3571, 2963 ,2843 , 215, 30243 ,13059 )
    b = findByMod(80735, 51964 , 8681, 2552, 40624 , 30931)
    c = findByMod(99892, 92228 , 45629, 1080, 24497 , 12651)
    d = findByMod(54750, 26981 , 99627, 79040, 84339 , 77510)
  ```
- Sau khi đã tính được `a,b,c,d` ta sẽ nhìn vào các biểu thức trong mã nguồn
  ```c
    unsigned int a = buf[0] | (buf[4] << 8) | (buf[8] << 16);
    unsigned int b = buf[1] | (buf[5] << 8) | (buf[9] << 16);
    unsigned int c = buf[2] | (buf[6] << 8) | (buf[10] << 16);
    unsigned int d = buf[3] | (buf[7] << 8) | (buf[11] << 16);
  ```
- Rút ra được biểu thức tổng quát
  ```python
    number = x | y << 8) | (z << 16)
  ```
- Việc bây giờ là ta sẽ định nghĩa một hàm để tìm ra cặp 3 số  `x,y,z` thỏa mãn. Ta sẽ khoanh vùng flag sẽ để đọc được sẽ chỉ gồm ký tự đọc được. Ta sẽ cho 3 vòng lặp lồng nhau với phạm vi từ `(32->127)`
  ```python
    def findChar(number):
      for a in range(32,128):
        for b in range(32,128):
          for c in range(32,128):
            if (a | (b << 8) | (c << 16)) == number:
              return str(chr(a))+str(chr(b))+str(chr(c))
  ```

- Ta sẽ tính ra được 4 chuỗi
  ```python
    a = findChar(a)
    b = findChar(b)
    c = findChar(c)
    d = findChar(d)
  ```
- Có một điều là các ký tự không được sắp xếp theo chiều tuần tự như thế này mà như trên biểu thức thì
  ```c
    buf[0] | (buf[4] << 8) | (buf[8] << 16)
    buf[1] | (buf[5] << 8) | (buf[9] << 16)
    buf[2] | (buf[6] << 8) | (buf[10] << 16)
    buf[3] | (buf[7] << 8) | (buf[11] << 16)
  ```
- Nó sẽ là `0-4-8`, `1-5-9` ,.....
- Giờ ta đưa nó 4 chuỗi trên vào 1 mảng sau đó chạy vòng lặp 3 lần là lấy từng kí tứ mỗi lần cộng lại
  ```python
    arr = [a,b,c,d] # ['W0H', '3M3', 'L3r', 'K_3']
    result = ""
    for i in range(3):
      result += arr[0][i] + arr[1][i] + arr[2][i] + arr[3][i]
  ```
- Sẽ lấy đúng theo thứ tự `0-1-2-3`, `4-5-6-7`, `7-8-9-10-11` đúng 3 vòng lặp

==> kết quả flag là : `W3LK0M3_H3r3`
