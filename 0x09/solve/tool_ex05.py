import subprocess
import cv2


black = [29, 19,  5]
number = {
  '[1, 2, 4, 5, 6, 7]':0, '[2, 7]':1, '[1, 2, 3, 5, 6]':2, '[1, 2, 3, 6, 7]':3, '[2, 3, 4, 7]':4,
  '[1, 3, 4, 6, 7]':5, '[1, 3, 4, 5, 6, 7]':6, '[1, 2, 7]':7, '[1, 2, 3, 4, 5, 6, 7]':8, '[1, 2, 3, 4, 6, 7]':9
}
number_under = {1:[2, 10], 2: [12, 16], 3: [20, 10], 4: [12, 5], 5:[30, 2], 6:[40,10], 7:[30,15]}

list_pw_namefile = {}
list_img_word = {}


def read_name_file():
  MyOut = subprocess.Popen(['ls','/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO'], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  return stdout.decode("utf-8").strip().split("\n")

def get_password(name_file):
  MyOut = subprocess.Popen(['strings', '-e', 'l', '/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO/'+name_file], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  arr = str(stdout).split("\\n")
  password = arr[len(arr)-2]
  list_pw_namefile[name_file] = password

def enter_key_gen_image(name_file, password):
  process = subprocess.Popen(['wine', '/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO/'+name_file], stdin=subprocess.PIPE, stdout=subprocess.PIPE)
  process.stdin.write(password.encode())
  name_img,word = process.communicate()[0].decode("utf-8").strip().split("\n")[2].split("=>")
  list_img_word[name_img] = word

def get_number(name_img):
  img = cv2.imread("/home/ngo.van.nghia/Documents/Cyber-Sercurity/sec-exercises/0x09/FLEGGO/"+name_img)
  cv2.imshow("1123", img)
  # cv2.waitKey()

  string = ""
  list_check1 = []
  for key, value in number_under.items():
    arr = list(img[value[0], value[1]])
    if arr == black:
      list_check1.append(key)

  string+=str(number[str(list_check1)])

  list_check2 = []
  for key, value in number_under.items():
    arr = list(img[value[0], (value[1]+25)])
    if arr == black:
      list_check2.append(key)

  if list_check2 == []:
    return int(string)
  else:
    string+=str(number[str(list_check2)])
    return int(string)


print("List File:")
for name_file in read_name_file():
  if ".exe" in name_file:
    get_password(name_file)


print("\nList Password:")
# print(list_pw_namefile)


for (name_file,password) in list_pw_namefile.items():
  if ".exe" in name_file:
    enter_key_gen_image(name_file, password)

print("\nList img_word:")
# print(list_img_word)


print("\nList number_word:")
hash_number_word = {}
for (name_img,word) in list_img_word.items():
  hash_number_word[get_number(name_img.strip())] = word

result = ""
for key in sorted(hash_number_word.keys()):
  result += str(hash_number_word[key]).strip()

print("\nresult : "+result)

# mor3_awes0m3_th4n_an_awes0me_p0ssum@flare-on.com
