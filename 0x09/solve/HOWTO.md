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
