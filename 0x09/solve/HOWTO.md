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

# Bài 2:
- Ta sẽ sử dụng máy ảo và reverse file bằng phần mềm `ILSpy` để đưa về dạng code bản rõ
- Ta đã tìm được một đoạn show ra flag
  ```cs
    private string GetKey(List<uint> revealedCells)
    {
      revealedCells.Sort();
      Random random = new Random(Convert.ToInt32((revealedCells[0] << 20) | (revealedCells[1] << 10) | revealedCells[2]));
      byte[] array = new byte[32];
      byte[] array2 = new byte[32]
      {245,75,65,142,68,71,100,185,74,127,62,130,231,129,254,243,28,58,103,179,60,91,195,215,102,145,154,27,57,231,241,86
      };
      random.NextBytes(array);
      for (uint num = 0u; num < array2.Length; num++)
      {
        array2[num] ^= array[num];
      }
      return Encoding.ASCII.GetString(array2);
    }
  ```
- Và một hàm check list các ô mình bấm
  ```cs
    private static uint VALLOC_NODE_LIMIT = 30u;

    private static uint VALLOC_TYPE_HEADER_PAGE = 4294966400u;

    private static uint VALLOC_TYPE_HEADER_POOL = 4294966657u;

    private static uint VALLOC_TYPE_HEADER_RESERVED = 4294967026u;

    private uint[] VALLOC_TYPES = new uint[3]
    {
      VALLOC_TYPE_HEADER_PAGE,
      VALLOC_TYPE_HEADER_POOL,
      VALLOC_TYPE_HEADER_RESERVED
    };

    private void AllocateMemory(MineField mf)
    {
      for (uint num = 0u; num < VALLOC_NODE_LIMIT; num++)
      {
        for (uint num2 = 0u; num2 < VALLOC_NODE_LIMIT; num2++)
        {
          bool flag = true;
          uint r = num + 1;
          uint c = num2 + 1;
          if (VALLOC_TYPES.Contains(DeriveVallocType(r, c)))
          {
            flag = false;
          }
          mf.GarbageCollect[num2, num] = flag;
        }
      }
    }

    private uint DeriveVallocType(uint r, uint c)
    {
      return ~(r * VALLOC_NODE_LIMIT + c);
    }

  ```
- Từ code trên sau khi sửa rồi mã để thành file `ex02.cs` thì kết qủa thu được flag là :
  ```
    Ch3aters_Alw4ys_W1n@flare-on.com
  ```
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


# Bài 7:
- Sau khi reverse lại mã nguồn của file `rightsout.exe` ta sẽ tìm được một hàm `check()` - trong file `ex07.cs`
- Sửa và bỏ các điều kiện trong hàm đi ta được.
  ```cs
    public class Program
    {
      public static void Main()
      {
        int[] array = new int[8]{1,	7,	16,	11,	14,	19,	20,	18};
        int[] array2 = new int[33]{85,	111,	117,	43,	104,	127,	117,	117,	33,	110,	99,	43,	72,	95,	85,	85,	94,	66,	120,	98,	79,	117,	68,	83,	64,	94,	39,	65,	73,	32,	65,	72,	51};
        string text = "";
        for (int j = 0; j < array2.Length; j++)
        {
          text += (char)(array2[j] ^ array[j % array.Length]);
        }
        Console.WriteLine(text);
      }
    }
  ```
- Sử dụng công cụ compile online với C#

==> Ta thu được Flag là : The flag is `FLAG_EhiAfPAAY7JG3UZ2`
