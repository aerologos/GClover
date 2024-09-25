# Information: https://clover.coex.tech/programming

import math
import rospy
from std_srvs.srv import Trigger
from sensor_msgs.msg import Range

from pymavlink import mavutil
from mavros_msgs.srv import CommandLong
from mavros_msgs.msg import State

from clover import srv
from clover import long_callback
from clover.srv import SetLEDEffect

rospy.init_node('flight')

get_telemetry = rospy.ServiceProxy('get_telemetry', srv.GetTelemetry)
navigate = rospy.ServiceProxy('navigate', srv.Navigate)
land = rospy.ServiceProxy('land', Trigger)
set_effect = rospy.ServiceProxy('led/set_effect', SetLEDEffect)

send_command = rospy.ServiceProxy('mavros/cmd/command', CommandLong)

def calibrate_gyro():
    rospy.loginfo('Calibrate gyro')
    if not send_command(command=mavutil.mavlink.MAV_CMD_PREFLIGHT_CALIBRATION, param1=1).success:
        return False

    calibrating = False
    while not rospy.is_shutdown():
        state = rospy.wait_for_message('mavros/state', State)
        if state.system_status == mavutil.mavlink.MAV_STATE_CALIBRATING or state.system_status == mavutil.mavlink.MAV_STATE_UNINIT:
            calibrating = True
        elif calibrating and state.system_status == mavutil.mavlink.MAV_STATE_STANDBY:
            rospy.loginfo('Calibrating finished')
            return True

set_effect(r=255)

calibrate_gyro()

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
    file1 = open('flight-mission.txt', 'r')
    points = file1.readlines()

    for point in points:
        parts = point.split(',')
        if len(parts) != 2:
            print('You have made a wrong input. I cannot process it.')
            continue

        marker = int(parts[0])
        z = float(parts[1])

        frameid = 'aruco_{}'.format(marker)
        navigate_wait(z=z, yaw=math.pi/2, frame_id=frameid)
        telem = get_telemetry()
        print('Marker {} reached! Current height is {}'.format(marker, telem.z))
        rospy.sleep(3)

except:
    print('Something went wrong. Please, try again...')

print('Perform landing')
land()
rospy.sleep(5)
print('Landing is Done')
