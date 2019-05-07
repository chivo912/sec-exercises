import subprocess, sys
import os

list_password = []

def read_name_file():
  MyOut = subprocess.Popen(['ls'], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  return str(stdout).split("\\n")

def get_password(name_file):
  MyOut = subprocess.Popen(['strings', '-e', 'l', name_file], stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  stdout,stderr = MyOut.communicate()
  arr = str(stdout).split("\\n")
  password = arr[len(arr)-2]
  list_password.append(password)

for name_file in read_name_file():
  if ".exe" in name_file:
    get_password(name_file)
    print(name_file)
