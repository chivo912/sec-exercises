import cv2

img = cv2.imread("input_image.png", 0)
ret, thresh = cv2.threshold(img, 10, 255, cv2.THRESH_OTSU)
print ("Threshold selected : ", ret)
cv2.imwrite("./output_image.png", thresh)
