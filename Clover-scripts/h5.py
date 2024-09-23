# Information: https://clover.coex.tech/programming

import math
import rospy
import cv2
from std_srvs.srv import Trigger
from sensor_msgs.msg import Range
from colorama import Fore

from pyzbar import pyzbar
from pyzbar.pyzbar import ZBarSymbol
from sensor_msgs.msg import Image
from cv_bridge import CvBridge

from clover import srv
from clover import long_callback
from clover.srv import SetLEDEffect

rospy.init_node('flight')
bridge = CvBridge()

get_telemetry = rospy.ServiceProxy('get_telemetry', srv.GetTelemetry)
navigate = rospy.ServiceProxy('navigate', srv.Navigate)
land = rospy.ServiceProxy('land', Trigger)
set_effect = rospy.ServiceProxy('led/set_effect', SetLEDEffect)
image_pub = rospy.Publisher('~fire_debug', Image, queue_size=1)

telem = get_telemetry()
print('Battery: {}'.format(telem.voltage))
print('Connected: {}'.format(telem.connected))

def range_callback(msg):
    global h
    h = msg.range

rospy.Subscriber('rangefinder/range', Range, range_callback, queue_size=1)

@long_callback
def image_callback(msg):
    img = bridge.imgmsg_to_cv2(msg, 'bgr8')
    barcodes = pyzbar.decode(img, [ZBarSymbol.QRCODE])
    
    image_pub.publish(bridge.cv2_to_imgmsg(img, 'bgr8'))

    for barcode in barcodes:
        b_data = barcode.data.decode('utf-8')
        #b_type = barcode.type
        (x, y, w, h) = barcode.rect
        xc = x + w/2
        yc = y + h/2

        # we are not interested in other barcodes atm.
        if b_data not in goods_to_zones:
            print(Fore.WHITE + 'Found not interesting {} QRcode'.format(b_data))
            continue

        telem = get_telemetry('aruco_map')
        current_zone = get_code_zone(telem.x, telem.y)
        actual_zone = b_data
        
        if actual_zone == current_zone:
            msg_color = Fore.GREEN
            rect_color = (0, 255, 0)
            object_state = "correct"
            set_effect(r=255, g=255, b=0)  # fill strip with red color
        else:
            msg_color = Fore.RED
            rect_color = (0, 0, 255)
            object_state = "incorrect"
            set_effect(r=255, g=0, b=0)  # fill strip with red color
        
        global current_good
        current_good = b_data

        cv2.rectangle(img, (x, y), (x + w, y + h), rect_color, 3)
        image_pub.publish(bridge.cv2_to_imgmsg(img, 'bgr8'))
        print(msg_color + 'Found {} in a zone for {}: {}'.format(b_data, current_zone, object_state) + Fore.WHITE)

    rospy.sleep(1)

image_sub = rospy.Subscriber('main_camera/image_raw_throttled', Image, image_callback, queue_size=1)

def navigate_wait(x=0, y=0, z=0, yaw=float('nan'), speed=0.5, frame_id='', auto_arm=False, tolerance=0.2):
    navigate(x=x, y=y, z=z, yaw=yaw, speed=speed, frame_id=frame_id, auto_arm=auto_arm)

    while not rospy.is_shutdown():
        telem = get_telemetry(frame_id='navigate_target')
        if math.sqrt(telem.x ** 2 + telem.y ** 2 + telem.z ** 2) < tolerance:
            break
        rospy.sleep(0.2)


print('Take off and hover 1 m above the ground')
navigate_wait(x=0, y=0, z=1, frame_id='body', auto_arm=True)

# Wait for 5 seconds
rospy.sleep(5)
print('Takeoff done!')

try:
    navigate_wait(x=1, y=2, z=1.5, frame_id='aruco_map')
    print('Point 1 done!')

    navigate_wait(x=3, y=3, z=1.5, frame_id='aruco_map')
    print('Point 2 done!')

    navigate_wait(x=2, y=4, z=1.5, frame_id='aruco_map')
    print('Point 3 done!')

    navigate_wait(x=5, y=2, z=1.5, frame_id='aruco_map')
    print('Point 4 done!')

    navigate_wait(x=0, y=0, z=1.5, frame_id='aruco_map')
    print('Point 5 done!')

except:
    print('Something went wrong. Please, try again...')

print('Perform landing')
land()
rospy.sleep(5)
print('Landing is Done')
