# Information: https://clover.coex.tech/programming

import math
import rospy
from std_srvs.srv import Trigger
from sensor_msgs.msg import Range

from clover import srv
from clover import long_callback
from clover.srv import SetLEDEffect

rospy.init_node('flight')

get_telemetry = rospy.ServiceProxy('get_telemetry', srv.GetTelemetry)
navigate = rospy.ServiceProxy('navigate', srv.Navigate)
land = rospy.ServiceProxy('land', Trigger)
set_effect = rospy.ServiceProxy('led/set_effect', SetLEDEffect)

telem = get_telemetry()
print('Battery: {}'.format(telem.voltage))
print('Connected: {}'.format(telem.connected))

def navigate_wait(x=0, y=0, z=0, yaw=float('nan'), speed=0.5, frame_id='', auto_arm=False, tolerance=0.2):
    navigate(x=x, y=y, z=z, yaw=yaw, speed=speed, frame_id=frame_id, auto_arm=auto_arm)

    while not rospy.is_shutdown():
        telem = get_telemetry(frame_id='navigate_target')
        if math.sqrt(telem.x ** 2 + telem.y ** 2 + telem.z ** 2) < tolerance:
            break
        rospy.sleep(0.2)


print('Take off and hover 1 m above the ground')
navigate_wait(x=0, y=0, z=1, frame_id='body', auto_arm=True)
print('Takeoff done!')

try:
    navigate_wait(x=0, y=0, z=2.0, frame_id='aruco_12')
    print('Point 1 done!')

    navigate_wait(x=0, y=0, z=2.0, frame_id='aruco_14')
    print('Point 2 done!')

    navigate_wait(x=0, y=0, z=2.0, frame_id='aruco_20')
    print('Point 3 done!')

    navigate_wait(x=0, y=0, z=2.0, frame_id='aruco_29')
    print('Point 4 done!')

    navigate_wait(x=0, y=0, z=2.0, frame_id='aruco_27')
    print('Point 5 done!')

except:
    print('Something went wrong. Please, try again...')

print('Perform landing')
land()
rospy.sleep(5)
print('Landing is Done')
