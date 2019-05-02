# Bài 1
- Đề bào là thuật toán đổ nước
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
  - do bước cuối cùng sẽ thừa dấu gạch dưới nên ta sử dụng subString để cắt lấy phần các bước:
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
- Do bắt đầu từ bài 2 trở đi thì nó sẽ dạng để bài:
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

# Bài 3:
