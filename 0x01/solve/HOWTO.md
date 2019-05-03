# Bài 1
- Sử dụng các trang web encode base-n online để  chuyển chuỗi `1234`
+ Base-2 : `00110001001100100011001100110100`
+ Base-8 : `2322`
+ Base-16: `4d2`
+ Base-36: `ya`
+ Base-58: `2FwFnT`
+ Base-64: `MTIzNA==`

# Bài 2
- Đang tiến hành viết hàm encode và decode

# Bài 3
- Thấy chuỗi đầu tiền được viết hoa `EBG KVVV` và tra cứu thì được biết đó là ROT 13. Nên sử dụng ROT 13 Onile để giải mã và được đoạn text:`ROT XIII is a simple letter substitution cipher that replaces a letter with the letter XIII letters after it in the alphabet. ROT XIII is an example of the Caesar cipher, developed in ancient Rome. Flag is FLAGSwzgxBJSAMqwxxAU. Insert an underscore immediately after FLAG.` Theo như dịch nghĩa thì Flag là `FLAGSwzgxBJSAMqwxxAU`, chèn thêm dấu gạch dưới vào ngay sau FLAG --> `FLAG_SwzgxBJSAMqwxxAU`

# Bài 4
- Nhận thấy đây là dạng của Base64 nên ta tiến hành giải mã. Nhưng giải mã 1-2 lần thấy không được kết quả. Nên đã thử một vòng lặp với số N mình nhập vào thì sau khi thử thì N = 16 đã thấy có kết quả. Như vậy đây là bản mã hóa 16 lần kết quả flag là :
  ```
    begin 666 <data>
    51DQ!1U]&94QG4#-3:4%797I74$AU

    end
  ```

# Bài 5
- Dấu hiệu là mã toàn chữ hoa và số nên nghĩ ngay đến Base32 sau khi giải mã ta được đoạn bản rõ: ```matesctf{w3lc0m3_to_th3_f1n4l_r0und}```
