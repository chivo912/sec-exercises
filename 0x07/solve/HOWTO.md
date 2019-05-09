# Bài 1:
- Ta có thể inspect ra code html và js của đề bài
- Và nhận thấy thằng có một đoạn điều kiện sử dụng chuỗi `PyvragFvqrYbtvafNerRnfl@syner-ba.pbz` để làm chuỗi điều kiện :
  ```js
    if ("PyvragFvqrYbtvafNerRnfl@syner-ba.pbz" == rotFlag) {
          alert("Correct flag!");
        } else {
          alert("Incorrect flag, rot again");
        }
  ```
- Vậy mấu chốt sẽ nằm ở đoạn chuỗi kia
- Tiếp tục xem đến đoạn mã hóa chuỗi đầu vào
  ```js
    var rotFlag = flag.replace(/[a-zA-Z]/g, function(c) {
          return String.fromCharCode(
            (c <= "Z" ? 90 : 122) >= (c = c.charCodeAt(0) + 13) ? c : c - 26
          );
        });
  ```
- Ta sẽ nghĩ ngay ra ROT 13. Vậy là ta chỉ cần thử chuỗi String trên và giải  mã bằng ROT 13 và sẽ biết kết quả.
- Và không ngoài suy đoán sau khi giải mã ta sẽ được chuỗi sau:

  ```
    ClientSideLoginsAreEasy@flare-on.com
  ```

# Bài 3:
- Điều cần làm bây giờ làm xem được mã nguồn JS cuả Server. Nhưng nó lại bị đưa về một dạng mã đăng biệt `(」・ω・)」うー!(/・ω・)/にゃー!encode`
- Đã thử tìm kiếm hàm decode nhưng không có
- Cuối cùng thì thử với `consol.log`. Đưa thằng mã code đó vào consol.log() xem nó in ra cái gì thì đã phát hiện ra mã nguồn dạng bản rõ.
- Và đoạn quan trọng nhất chính là :
  ```js
    $(function() {
      $("form").submit(function() {
          var t = $('input[type="text"]').val();
          var p = Array(70, 152, 195, 284, 475, 612, 791, 896, 810, 850, 737, 1332, 1469, 1120, 1470, 832, 1785, 2196, 1520, 1480, 1449);
          var f = false;
          if (p.length == t.length) {
              f = true;
              for (var i = 0; i < p.length; i++)
                  if (t.charCodeAt(i) * (i + 1) != p[i]) f = false;
              if (f) alert("(」・ω・)」うー!(/・ω・)/にゃー!");
          }
          if (!f) alert("No");
          return false;
      });
    });
  ```
- Chỉ cần các ký tự của message thỏa mãn kiều kiện `(t.charCodeAt(i) * (i + 1) != p[i])`. Bằng các ký tự của mảng sau khi đã nhân với vị trí cộng 1.
- Bây giờ ta chỉ cần viết một hàm tính ngược lại các giá trị của các phần từ của message
  ```python
    arr = [70, 152, 195, 284, 475, 612, 791, 896, 810, 850, 737, 1332, 1469, 1120, 1470, 832, 1785, 2196, 1520, 1480, 1449]

    for i, c in enumerate(arr):
      print(chr(c//(i+1)), end = "")
    print()

  ```

==> Flag thu được là : `FLAG_fqpZUCoqPb4izPJE`
