# Bài 1:
- Sau khi tải file `q8.pcap` và kiểm tra bằng phần mềm Wireshark
- Thì thấy được qua header `Authorization: Basic cTg6RkxBR181dXg3eksyTktTSDhmU0dB`
  ```html
    GET /~q8/ HTTP/1.1
    Host: ctfq.sweetduet.info:10080
    Connection: keep-alive
    Authorization: Basic cTg6RkxBR181dXg3eksyTktTSDhmU0dB
    User-Agent: Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.162 Safari/535.19
    Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
    Accept-Encoding: gzip,deflate,sdch
    Accept-Language: ja,en-US;q=0.8,en;q=0.6
    Accept-Charset: Shift_JIS,utf-8;q=0.7,*;q=0.3

    HTTP/1.1 200 OK
    Date: Sat, 26 May 2012 20:54:05 GMT
    Server: Apache/2.2.15 (CentOS)
    Last-Modified: Sat, 26 May 2012 12:24:46 GMT
    ETag: "422da-b8-4c0ef920b3f8e"
    Accept-Ranges: bytes
    Content-Length: 184
    Connection: close
    Content-Type: text/html; charset=UTF-8

    <!DOCTYPE html>
    <html>
      <head>
        <meta charset="utf-8">
        <title>Q8</title>
      </head>
      <body>
        <p>Congratulations!</p>
        <p>The flag is q8's password.</p>
      </body>
    </html>

  ```
- Và Reponse HTML: `The flag is q8's password.`
- vậy chuỗi : `cTg6RkxBR181dXg3eksyTktTSDhmU0dB` đây là dạng Base64
- Sau khi giải mã ta được chuỗi: `q8:FLAG_5ux7zK2NKSH8fSGA`
--> suy ra flag là : `FLAG_5ux7zK2NKSH8fSGA` là mật khẩu

# Bài 2:
- Sau khi tải file `q9.pcap` và mở bằng Wireshark thì ta nhận thấy rằng
   ```html
    GET /~q9/ HTTP/1.1
    Host: ctfq.sweetduet.info:10080
    Connection: keep-alive
    Authorization: Digest username="q9", realm="secret", nonce="bbKtsfbABAA=5dad3cce7a7dd2c3335c9b400a19d6ad02df299b", uri="/~q9/", algorithm=MD5, response="c3077454ecf09ecef1d6c1201038cfaf", qop=auth, nc=00000001, cnonce="9691c249745d94fc"
    User-Agent: Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.162 Safari/535.19
    Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
    Accept-Encoding: gzip,deflate,sdch
    Accept-Language: ja,en-US;q=0.8,en;q=0.6
    Accept-Charset: Shift_JIS,utf-8;q=0.7,*;q=0.3

    HTTP/1.1 200 OK
    Date: Sat, 26 May 2012 20:54:45 GMT
    Server: Apache/2.2.15 (CentOS)
    Authentication-Info: rspauth="42b425bdd3ad27086858915611646f7c", cnonce="9691c249745d94fc", nc=00000001, qop=auth
    Last-Modified: Sat, 26 May 2012 12:28:32 GMT
    ETag: "422e2-c0-4c0ef9f82a6c7"
    Accept-Ranges: bytes
    Content-Length: 192
    Connection: close
    Content-Type: text/html; charset=UTF-8

    <!DOCTYPE html>
      <head>
        <meta charset="utf-8">
        <title>Q9</title>
      </head>
      <body>
        <p>Congratulations!</p>
        <p>The flag is <a href="flag.html">here</a>.</p>
      </body>
    </html>
   ```
- Có thể nhận thấy nếu qua được bước Authentication là ta có thể lấy được Flag
- Sau khi đã phân tích gói tin ta tìm được HA1 là mã MD5 cua username:password
  ```
    q9:secret:c627e19450db746b739f41b64097d449

  ```
- Việc cần làm bây giờ là phải tính `response = MD5(HA1:nonce:nonceCount:cnonce:qop:HA2)`
- Từ gói tin ta đã có :
  + nonce = `Es7EPC6IBQA=531b3c11fa84aa94757c7ec7c3b17f2aa7747316` - với nonce sẽ được tự động sinh ra sau mỗi lần request
  + uri = `/~q9/`
  + qop = `auth`
  + nc = `00000001`
  + cnonce = `9691c249745d94fc`
- Ta sẽ sử dụng Postman để giả lập request và lấy về được nonce sau mỗi lần request
  --> ```Digest realm="secret", nonce="Es7EPC6IBQA=531b3c11fa84aa94757c7ec7c3b17f2aa7747316", algorithm=MD5, qop="auth"```
- Từ các tham số và công thức ta sẽ tính được Response:
  ```
    7092728c2105c668bc35e0054f21e51b
  ```
- Ta đưa vào header của request với key-value tương ứng:
  ```
    Authorization: Digest username="q9", realm="secret", nonce="Es7EPC6IBQA=531b3c11fa84aa94757c7ec7c3b17f2aa7747316", uri="/~q9/", algorithm=MD5, response="5c5a7b047c4d9ab49b431f12128043c6", qop=auth, nc=00000001, cnonce="9691c249745d94fc"
  ```
- Ta sẽ qua được phần xác thực công việc giờ là phải tính tiếp với URI : `/~q9/flag.html` thì chúng ta mới có thể thấy được flag
- Tính được Response ta tiếp tục gửi request với yêu cầu đến trang flag.html
- Kết qủa flag là : `flag FLAG_YBLyivV4WEvC4pp3`
