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
