import subprocess

list_pw_namefile = {}
list_img_word = {}
def read_name_file():
  MyOut = subprocess.Popen(['ls','/home/nghia/Desktop/test_python/sec-exercises/0x09/FLEGGO'], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  return stdout.decode("utf-8").strip().split("\n")

def get_password(name_file):
  MyOut = subprocess.Popen(['strings', '-e', 'l', '/home/nghia/Desktop/test_python/sec-exercises/0x09/FLEGGO/'+name_file], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  arr = str(stdout).split("\\n")
  password = arr[len(arr)-2]
  list_pw_namefile[name_file] = password

def enter_key_gen_image(name_file, password):
  process = subprocess.Popen(['wine', '/home/nghia/Desktop/test_python/sec-exercises/0x09/FLEGGO/'+name_file], stdin=subprocess.PIPE, stdout=subprocess.PIPE)
  process.stdin.write(password.encode())
  name_img,word = process.communicate()[0].decode("utf-8").strip().split("\n")[2].split("=>")
  list_img_word[name_img] = word


print("List File:")
for name_file in read_name_file():
  if ".exe" in name_file:
    get_password(name_file)
    print(name_file)


print("\nList Password:")
print(list_pw_namefile)


for (name_file,password) in list_pw_namefile.items():
  if ".exe" in name_file:
    enter_key_gen_image(name_file, password)

print("\nList img_word:")
print(list_img_word)
