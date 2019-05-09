# Bài 1:

- Sau khi Decompilers online file `MinesweeperChampionshipRegistration.jar`
  ta thu được một file class với code như bên dưới


  ```java
    import javax.swing.JOptionPane;

    public class InviteValidator {
      public InviteValidator() {}

      public static void main(String[] args) { String response = JOptionPane.showInputDialog(null, "Enter your invitation code:", "Minesweeper Championship 2018", 3);
        if (response.equals("GoldenTicket2018@flare-on.com"))
        {
          JOptionPane.showMessageDialog(null, "Welcome to the Minesweeper Championship 2018!\nPlease enter the following code to the ctfd.flare-on.com website to compete:\n\n" + response, "Success!", -1);
        }
        else
        {
          JOptionPane.showMessageDialog(null, "Incorrect invitation code. Please try again next year.", "Failure", 0);
        }
      }
    }
  ```
- Theo yêu cầu flag là địa chỉ email kết thúc bằng `@flare-on.com`.
- Ta có thể nhận thấy :
  ```java
    if (response.equals("GoldenTicket2018@flare-on.com"))
  ```
- Thì chuỗi "GoldenTicket2018@flare-on.com" chính là flag

# Bài 3:
- Sau khi revert code file `code.cpython-37.pyc` ta sẽ được một hàm:
    ```python
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
    ```
- Điều chúng ta cần bây giờ chỉ cần decode được `39830963251313012931406054205649358377525286926249590L` để được flag.
- Do thấy được rằng quy luật là phần dư của phần tử thứ 0 trong chuỗi flag sẽ có giá trị lớn nhất sau đó các vị trí tiếp theo phần dư sẽ giảm dần theo cấp số mũ
- Ý tưởng đặt ra là sẽ dùng vòng lặp để chạy 22 lần check. Bên trong mỗi vòng lặp sẽ check tiếp với hàm chec_flag để đưa ra kêt quả xem giá trị có lớn chuỗi số flag đã mã hóa không
  ```python
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
  ```
- nó sẽ check từ 0 -> z và thay thế vào vị trí thứ i tương ứng rồi tiếng chec_flag để so sánh. Nếu lớn hơn sẽ lấy thằng ký tự trước đó
  vd: ` "b000" lớn hơn sẽ lấy "a000" rồi tiếp theo sẽ xét ac00 mà lớn hơn sẽ lấy ab00 cứ như vậy đến phần tử thứ cuối của chuỗi`

- Kết qua cuối dùng flag là :
  ```
    just_ascii_to_hex_conv
  ```
### Cách 2 : của anh tiến
- sử dụng thuật toán rolling hash và anh giải bằng 1 dòng code python :(
  ```python
    x= 39830963251313012931406054205649358377525286926249590
    hex(x)[2:-1].decode('hex')

    >>> just_ascii_to_hex_conv
  ```


# Bài 5 :
- Sau khi nghiêm cứu và tìm hiểu thì đã tìm ra hướng giải cho bài `FLEGGO` này
#### đối với sử dụng terminal:
- Thì sẽ sử dụng lệnh `strings -e l [name_file]`. Nó sẽ tìm ra tất cả các ký tự có thể đọc được trong file đó. Và với trường hợp này là password của file
- Sau khi sử dụng lệnh `strings` để lấy được password trong file thì ta sẽ sử dụng lệnh `wine [name_file]` để chạy file. Do trên môi trường Ubuntu không thể chạy trực tiếp file .exe nên phải sử dụng thông qua công cụ `wine`. Sau khi chạy file lên nó sẽ yêu cầu nhập mật khẩu, sau khi điền mật khẩu đúng thì một ảnh sẽ được sinh ra và một gửi trả bao gồm tên file cùng một ký tự
- Điều đặc biệt đó là trong ảnh có chứa số thứ tự, từ số thứ tự ảnh ta có thể sắp xếp các ký tự đi cùng ảnh để tạo ra một chuỗi. Chuỗi đó chính là flag
- Điều đặt ra bây giờ là cần viết ra một tự động chạy các lệnh trên và tự động từ ảnh sẽ sắp xếp sau đó sinh chuỗi kết quả theo thứ tự trong ảnh

#### Viết tool bằng python
##### * Bước 1:
  - List ra các file .exe trong thư mục `FLEGGO`, lấy tên của chúng và đưa vào một list để chạy lệnh với từng file
    ```python
      def read_name_file():
        MyOut = subprocess.Popen(['ls','/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO'], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
        stdout,stderr = MyOut.communicate()
        return stdout.decode("utf-8").strip().split("\n")
    ```

  - Sau khi đã có các tên file ta sẽ thực hiện chúng với `strings` và `wine` thông qua `subprocess` của python
    ```python
      def get_password(name_file):
        MyOut = subprocess.Popen(['strings', '-e', 'l', '/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO/'+name_file], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
        ...

      def enter_key_gen_image(name_file, password):
        process = subprocess.Popen(['wine', '/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO/'+name_file], stdin=subprocess.PIPE, stdout=subprocess.PIPE)
        process.stdin.write(password.encode())
        ...
    ```
##### * Bước 2:
  - Thực hiện lần lượt các file với các hàm trên và kết quả sẽ trả ra một hasmap gồm các tên file ảnh `.png` và ký tự
  - Bây giờ sẽ sử dụng thư viện OpenCV để chiết xuất số từng trong ảnh ra
    ```python
      def get_number(name_img):
        img = cv2.imread("/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO/"+name_img)
        cv2.imshow("1123", img)
        ...
    ```
  - Hashmap gồm tên file và ký tự ta sẽ thực hiện`get_number` với từng ảnh để đưa ra được một hashmap mới chỉ bao gồm số thực tự là key và value là ký tự từ. Sau đó là sắp xếp(sort) theo key và in ra

=> Kết quả thu được là một chuỗi flag: `mor3_awes0m3_th4n_an_awes0me_p0ssum@flare-on.com`
