# Bài 1:
- Theo như phép giải mã của hệ mật RSA ta sẽ viết ra được code như trong file `ex01.py`
- `m = c ^ d mod n` từ `c` ban đầu ta cần tìm `d`
- Và sử dụng:
  ```python
    print ("%0512x"%m).decode("hex")
  ```
=> Kết quả Flag là : `FLAG_NbZ6gQeKDMVFJMaU`

# Bài 2:
- Đề bài ta có thể thấy yêu cầu đó chính là tìm căn bậc `101` của `x`
- Theo như lý thuyết thì `sqrt(x) = x^(1/ 2)` vậy căn bậc `101` = `x ^ (1/101)`. Nhưng có một điều đó là do `x` quá lớn sẽ làm tràn bộ nhớ của Python.
- Ta sẽ nghĩ ngay đến tìm kiếm nhịn phân
- Việc đầu tiên cần tìm khoảng `cận trên` và `cận dưới` của khoảng
- Đối với tìm cận trên bằng cách `num_max = 10 ^ x` với i tăng step = 10 đến khi nào `num_max ^ 101 > x` thì dừng. Còn num_min sẽ cho là `0`.
- Với vòng lặp tìm `num_max` ta được `x = 40 -> num_max = 10**40`
- Phần tiếp theo ta sẽ sử dụng vòng lặp với thuật toán nhị phân.
- Ta thu được `y = 545783032743911043438387247245888260273`
- Theo như đề bài flag là `FLAG_y`
--> Kết qủa flag: `FLAG_545783032743911043438387247245888260273`

# Bài 3:
- Sau khi sử dụng công cụ `Wrieshark` để phân tích file `p33.pcap`. Có thể nhận thấy giao thức trao đổi ở đây đang sử dụng là `TLSv1` để mã hóa dữ liệu trên đường truyền gửi đi.
- Theo nhưu tìm hiểu thì `TLS` và `SSL` đều giống nhau về mặt mã hóa. Chúng sử dụng chứng thực `X.509` để xác thực.
- Điều cần làm bây giờ là ta cần giải mã để tìm ra dữ liệu được truyền tải trên đường truyền để xem có flag nằm ở đâu trong các gói tin.
- Việc đầu tiên là cần đi tìm các gói tin `chữ ký số(Certificate)`. Thì ta thấy có 2 gói `Certificate` cả 2 đều có Infor là `Certificate, Server Hello Done`
- Mỗi gói bên trong đều có 2 chứng chỉ 1 là của Client gửi lên Server và 1 là của Server gửi lại Client.
- Ta sẽ lấy được chứng chỉ Của client gửi lên Server vì chúng sẽ sử dụng chúng khóa công khai của Server để mã hóa và chứng chỉ lấy được đó chính là `N` của RSA.
- Có 2 cách để lấy ra chứng chỉ 1 là sử dụng Wrishark và đưa ra 1 file `public.der` sau đó sử dụng công cụ `openssl` trong `Kali` hoặc `Ubuntu` để tìm ra `N` tiếp đó đưa nó từ hệ `Hex` về hệ `10` và phân tích thành thừa số nguyên tố tìm `p` và `q`. Còn cách 2 đơn giản hơn chỉ cần dùng wireshark đọc vào đến phần `modulus` trong chứng chỉ và đưa nó về hệ 10 là được `N`.
- Theo như đề bài thì 2 chứng chỉ này có sự tương tự nhau, chắc chắn chúng sẽ có mối liên hệ và việc nghĩ đến đầu tiên là sẽ có 1 trong 2 hệ số cấu thành nên `N` của RSA là `p` hoắc `q` sẽ trùng nhau.
- Để làm điều đó ta chỉ cần tìm UCLN của 2 số thì sẽ ra được. Ta coi hệ số đó là `p` trùng nhau.
- Sử dụng code của `ex03.py` ta sẽ tính ra được `p` chung và `q1` `q2`
- Bây giờ ta sử dụng một công cụ khác là `rsatool.py` để từ 2 hệ số trên và đưa ra 2 priavte.key
- Từ 2 file này ta đưa nó vào wireshark để nó giả mã và sau khi đưa 2 file private.key vào thì ta sẽ thấy ngay có 4 ảnh được truyền.
- Trong đó có 2 ảnh chứa flag mỗi ảnh chứa 1 nửa
==> Kết quả flag là : `FLAG_txkbcPbk2ZJagaXs`





