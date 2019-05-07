# Bài 1:
  - sau khi quan sát thì đã phát hiện ra đoạn mã cuối cùng
    ```php
      eval(ugqkkci($ssyowfjkl, $cxcejhixho));
    ```
- Với `function ugqkkci()` theo như quan sát chính là hàm giải mã của đoạn code đã mã hóa chứa bởi biến `$ssyowfjkl`
- Bây giờ thay vì eval() ta sẽ echo để nó show ra code bên trong hàm eval() đó sẽ chạy
- Kết quả là thu được code như trong file `ex01.php`
# Bài 2:
- Ta có thể xem được mã nguồn của server
  ```php
    <!DOCTYPE html>
    <html>
      <head>
        <title>Simple Auth</title>
      </head>
      <body>
        <div>
    <?php
    $password = 'FLAG_????????????????';
    if (isset($_POST['password']))
        if (strcasecmp($_POST['password'], $password) == 0)
            echo "Congratulations! The flag is $password";
        else
            echo "incorrect...";
    ?>
        </div>
        <form method="POST">
          <input type="password" name="password">
          <input type="submit">
        </form>
      </body>
    </html>
  ```
- Có thể thấy chỉ cần pass qua được điều kiện so sánh có sử dụng hàm strcasemp() là ta có thể thấý được Flag
  ```php
    if (strcasecmp($_POST['password'], $password) == 0)
  ```
- Theo như tìm hiểu thì hàm strcasemp() chỉ so sánh chuối nếu giống nhau nó sẽ trả về = 0. Nhưng ta không truyền vào 1 chuỗi mà ta sẽ đưa vào 1 mảng. Nó sẽ bị lỗi không so sánh được và trả về 0 vây lá pass qua được if.
- Lúc này flag sẽ được in ra :
  ```
    Congratulations! The flag is FLAG_VQcTWEK7zZYzvLhX
  ```

# Bài 3:
- Sau khi test và được đọc mã nguồn của server thì ta có thể nhận ra một điều đó là server sử dụng Cookie để check dữ liệu
- Trong Cookie sẽ có hay 2 cặp key-value đó là `ship` và `signature`
- Với `ship` là một mảng chứa danh sách các số nguyên ngẫu nhiên random từ `0-count($shipname) - 2`
  ```php
    $shipname = array(
      'Nagato',
      'Mutsu',
      'Kongo',
      'Hiei',
      'Haruna',
      'Kirishima',
      'Fuso',
      'Yamashiro',
      'Ise',
      'Hyuga',
      "Yamato [Congratulations! The flag is $salt. ??????????????????????????????????????.]"
    );
  ```
- Theo như server thì mỗi lần nếu ta bấm nút `Gacha` thì một số ngẫu nhiên trong khoảng từ `0-count($shipname) - 2` sẽ được thêm vào mảng `ship` và nó sẽ biến mảng `ship` thành 1 chuỗi cách nhau bởi dấu `","` rồi nối chuỗi với `flag` sau đó mã hõa `sha-512` để tạo ra `signature`. Phần hiển thị sẽ show ra các ký tự trong mảng `$shipname` tương ứng với mảng `ship` với các phần tử trong mảng chính là index của các phần tử trong `$shipname`.
- Có một điều là sẽ không bao giờ có flag được in ra vì `count($shipname) - 2` thì tức là không bao giờ có vị trí thứ 10 được add vaò mảng `ship` cả
- Ý tưởng đặt ra bây giờ là ta phải sửa đổi cookie vì server check qua cookie, nhưng vấn đề đặt ra là server sẽ tạo ra `ship` mới nếu nó kiểm tra thấy giá trị `signature` và mã hash của `flag` cộng chuỗi với chuỗi `ship` không bằng nhau.
  ```php
    hash('sha512', $salt.$_COOKIE['ship']) === $_COOKIE['signature'])
  ```
- Theo như tìm hiểu thì ta có thể tấn công theo hình thức `Length extension attack`
- Bây giờ từ cookie của lần bấm đầu tiên ta sẽ ra được mã của `signature` ta sẽ sử dụng tool để tính ra với trường hợp có thêm số `10` trong mảng ship thì mã hash sẽ là bao nhiêu để vượt qua được điều kiện kiểm tra của server.
- VD:
  ```
    với : 2
    mã hash: c999b96f60f240ef248f8058f12d99d8fe64e117ed6a159f3eecb2fd7f9d61458ed7cff05e5b2b34ad1a255085f63faccccc45ca738fbf5d7b3876b8ed15532d
  ```
- Bây giờ sẽ tính với trường hợp thêm 10. Sẽ sử dụng công cụ https://wiki.mma.club.uec.ac.jp/CTF/Toolkit/HashPump ta sẽ tính ra
  ```
    hashpump -s c999b96f60f240ef248f8058f12d99d8fe64e117ed6a159f3eecb2fd7f9d61458ed7cff05e5b2b34ad1a255085f63faccccc45ca738fbf5d7b3876b8ed15532d -d 2 -k 21 -a ,10
  ```
- Tương ứng với mã hash của `2`, độ dài của `flag` theo như mã nguồn là `21` và chuỗi sẽ thêm vào là `,10`
- Kết quả :
  ```
    e23f464e2bb7d37cb3efb55e98e057abaf024de68c8bb0c7e7412366a65b2e393fcc12ac1c86d6ba3680e48e3fa81b872bbb929e298507d20f0d59ad35008b7f
    2\x80\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\xb0,10
  ```
- Với trên là mã hash để set của `signature` còn dưới sẽ là chuỗi set vào cho `ship`
- Nhưng do trên đường truyền các ký tự `\x` sẽ bị mã hóa nên ta sẽ chuyển các kỹ tự `\x` này thành `%`
  ```
    2%80%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%00%b0,10
  ```
- Set hai giá trị trên vào cookie và submit flag sẽ được show phía bên dưới cũng các phần từ của `ship`

=> Kết quả thu được flag là : ``` FLAG_uc8qVFa8Sr6DwYVP```


# Bài 4
- Xem mã nguồn ta có thế thấy được rằng Server sử dụng database là SQLite
- Và theo như tìm hiểu thì có thể truy cập đến trực tấp file .db của SQLite mà không cần xác thực.
- Và các thử đó là sửa đổi URL thay vì đến file `auth.php` thì thử chuyển hướng đến file `database.db` chẳng hạn
- Kết quả là trình duyệt trả về file `database.db`
- công việc bây giờ chỉ cần đọc file và tìm ra password của tài khoản thỏa mãn.
- Sau khi đưa lên SQLite online và truy vấn all đến bảng user thì ta thấy duy nhất một bản ghi
  | id        | password              |
  | ----------|:---------------------:|
  | root      | FLAG_iySDmApNegJvwmxN |
- Nhập vào trang `auth.php` thông báo thành công
=> Vậy flag là : `FLAG_iySDmApNegJvwmxN`
