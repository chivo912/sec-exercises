# Bài 1:
- Ta sử dụng công cụ wireshark để quan sát các gói tin của file `for.pcap`
- Sử dụng Follow - UDP Stream đối với những gói tin NTP
- Show data ở chế độ Raw ta thấy có một quy luật là tất cả các gói tin đều bắt đầu bằng `1b` và kết thúc bằng `00`. Hay xem ở chế độ C Arrays t cũng thấy các Packet đều bắt đầu bằng `0x1b` và kết thúc bằng `0x00`

+Raw

  ```
    1b4141414141414141414141414141414141414141414141414141414141414141414141414141414141414141414100
    1b6956424f5277304b47676f414141414e5355684555674141417a6341414144694341494141414438714b364d414100
    1b414141584e535230494172733463365141414141526e51553142414143786a777638595155414141414a6345685a00
    1b6377414144734d4141413744416364767147514141426b5853555242564868653764316259747536726744514f3600
    ...
  ```
+C Arrays
  ```
    char peer0_1[] = { /* Packet 553 */
    0x1b, 0x69, 0x56, 0x42, 0x4f, 0x52, 0x77, 0x30,
    0x4b, 0x47, 0x67, 0x6f, 0x41, 0x41, 0x41, 0x41,
    0x4e, 0x53, 0x55, 0x68, 0x45, 0x55, 0x67, 0x41,
    0x41, 0x41, 0x7a, 0x63, 0x41, 0x41, 0x41, 0x44,
    0x69, 0x43, 0x41, 0x49, 0x41, 0x41, 0x41, 0x44,
    0x38, 0x71, 0x4b, 0x36, 0x4d, 0x41, 0x41, 0x00 };
    char peer0_2[] = { /* Packet 554 */
    ...
  ```
- Có một điều là khi đưa dạng ASCII lên CyberChef để giải mã thì không có được. Quan sát thấy một điều là tất các các gói tin đều bắt đầu bằng `..`
  ```
    .AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA..iVBORw0KGgoAAAANSUhEUgAAAzcAAADiCAIAAAD8qK6MAA..AAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZ..cwAADsMAAA7DAcdvqGQAABkXSURBVHhe7d1bYtu6rgDQO6..4OqOPpaPZkMphz/ZQpiYRAm06YZK2vc3YhPkBQRtuk+b//..AQAwH10aAMCMdGkAADPSpQEAzEiXBgAwI10aAMCMdGkAAD..PSpQEAzEiXBgAwI10aAMCMdGkAADPSpQEAzEiXBgAwI10a..AMCMdGkAADPSpQEAzEiXBgAwI10aAMCMdGkAADPSpQEAzE..iXBgAwI10aAMCMdGkAADPSpQEAzEiXBgAwI10aAMCMdGkA..ADPSpQEAzEiXBgAwI10aAMCMdGkAADPSpQEAzEiXBgAwI1..0aAMCMdGkAADPSpQEAzEiXBgAwI10aAMCMdGkAADPSpQEA..zEiXBgAwI10aAMCMdGkAADPSpQEAzEiXBgAwI10aAMCMdG..kAADPSpQEAzEiXBgAwI10aAMCMdGkAADPSpQEAzEiXBgAw..I10aAMCMdGkAADPSpQEAzEiXBgAwI10aAMCMdGkAADPSpQ..EAzEiXBgAwI10aAMCMdGkAADPSpQEAzEiXBgAwI10aAMCM
  ```
- Riêng có gói tin `AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA` đầu tiên là khác biệt thử xóa đi và giải mã thì ta được một ảnh PNG có nội dụng
  ```
    matesctf{s0_m4ny_3xf1l_pr0t0c0lz}
  ```
