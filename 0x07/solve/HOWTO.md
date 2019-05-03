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
