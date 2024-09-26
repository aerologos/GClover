#  1. Project name: Glover

Platform for creating graphical interfaces for industrial UAVs

**BRICS2024: UAS Challenge**, Kazan'
 
####  IDE:

Programming language: **C#**

Framework: **Avalonia UI**

Simulation: **Gazebo**

![Project schema](/schemas/schema.jpg)

#  2. Advantages

- Provides a user interface for building blocks for interacting with UAVs.
- Lowers the intellectual bar for entry into the process of developing UAV control systems.
- Designed for users with technical training in the field of unmanned technologies who do not have a base in the direction of software development.
- Cross-platform: allows you to control the drone from any operating system.
 
#  3. Roles of team members

| Full Name | Role  | Responsibilities |
| -------- | -------|------- |
| Valchuk Alexander | Team captain; Software engineer | Documentation; Programming; Simulation testing |
| Chipurko Andrey   | Engineer-technician; Pilot      | quadcopter settings, debugging and testing     |

_Role 1. Team Captain_ — organizing the team's work in GitHub, providing overall management of the team's work, distributing responsibilities, and monitoring compliance with deadlines.

_Role 2. Software Engineer_ -- developing the user interface; writing algorithms for interpreting flight tasks from the program. Working with visualization, writing code for autonomous quadcopter flight, developing an algorithm for safe quadcopter flight.

_Role 3. Technician Engineer_ — modeling and manufacturing the quadcopter payload, working with sensors, testing, servicing, and piloting the quadcopter.

_Role 4. Pilot_ — organizing pre-flight preparation, servicing the UAS, performing visual piloting in emergency situations.

# 4. System Requirements

| Check Procedure                                                                                       | Expected Results     |
| ----------------------------------------------------------------------------------------------------- | -------------------- |
| 1. Launching the Glover program                                                                       | Successful           |
| 2. Adding the IndoorMap module to the selected sector                                                 | Successful           |
| 3. Adding the ElevationMap module to the selected sector                                              | Successful           |
| 4. Adding the ArtificialHorizon module to the selected sector                                         | Successful           |
| 5. Stopping the program                                                                               | Successful           |
| 6. Launching the program with the latest settings                                                     | Successful           |
| 7. Deleting module location settings                                                                  | Successful           |
| 8. Creating a flight mission in the IndoorMap module                                                  | Successful           |
| 9. Checking the display of the copter movement pattern along the x-Z axes in the ElevationMap module  | Successful |
| 10. Saving the flight mission and executable program to the copter from the IndoorMap module          | Successful |
| 11. Launching a flight mission on the copter from the IndoorMap module                                | Successful |
| 12. Interpretation and execution of the flight task on the copter                                     | Successfully executed |

# 5. Development 
### Task table
| Task description | Responsible person | Deadline | Status | technologies / tools / software |
| --------------------------------------------------- | ------------- | -------------- | ---------- | ------------------------------------------ |
| Copter setup | A. Chipurko | 4 hours | Done | Copter Clover and peripherals |
| Checking the suitability for flight in manual mode | A. Chipurko | 1 hour | Done | Copter Clover and peripherals |
| Checking the suitability for flight in autonomous mode | A. Chipurko | 1 hour | Done | Copter Clover and peripherals |
| Setting tasks, declaring requirements | A. Valchuk | 2 hours | Done | github / vscode / markdown |
| Program for autonomous drone flight | A. Valchuk | 0.5 hours | Done | github / vscode / python |
| Software product framework/design | A. Valchuk | 4 hours | Done | github / vscode / dotnet8 / C# / Avalonia UI |
| Flight task construction module | A. Valchuk | 4 hours | Done | github / vscode / dotnet8 / C# / Avalonia UI |
| Altitude graph display module | A. Valchuk | 4 hours | Done | github / vscode / dotnet8 / C# / Avalonia UI |
| Altitude selection in the flight task construction module | A. Valchuk | 1 hour | Done | github / vscode / dotnet8 / C# / Avalonia UI |
| Flight task execution program on a drone | A. Valchuk | 2 hours | Done | github / vscode / python |
| Autonomous modes on variable flight tasks. | A. Chipurko | 1 hour | Done | Clover copter and peripherals |
| Saving a flight task for the copter from the program | A. Valchuk | 1 hour | Done | github / vscode / dotnet8 / C# / Avalonia UI |
| Launching a flight task on the copter from the program | A. Valchuk | 1 hour | Done | github / vscode / dotnet8 / C# / Avalonia UI |
| Building modules on a test PC under Windows | A. Valchuk | 2 hours | Assigned | github / vscode / dotnet8 / C# / Avalonia UI |
| Artificial horizon display module | A. Valchuk | 1 hour | Done | github / vscode / dotnet8 / C# / Avalonia UI |

# 6. Debug

| Name of the System requirement | Function availability mark | Test result |
| ------------------------------------------------------------------------------------- | ------------------------- | ---------------------- |
| The IndorMap module is used to build flight missions on a map with Aruco markers | Done | Successful |
| The ElevationMap module is used to display the flight mission elevation map | Done | Successful |
| The ArtificialHorizon module is used to demonstrate layout options | Done | Successful |
| The IndorMap module allows you to save a flight mission to the quadcopter file system | Done | Successful |
| The IndorMap module allows you to launch the quadcopter | Done | Successful |

# 7. Examples of program layout

#### Arrangement of modules (Example 1)
![-](/schemas/example1.jpg)

#### Arrangement of modules (Example 2)
![-](/schemas/example2.jpg)

#### Arrangement of modules (Example 3)
![-](/schemas/example3.jpg)


# 8. Deployment

### Source code
a. Install git client on your operating system: https://git-scm.com/downloads

b. Copy the path to the Glover repository: https://github.com/aerologos/Glover.git

c. Launch git terminal in any directory convenient for you

d. Run the following command: git clone https://github.com/aerologos/Glover.git

### Deployment
a. Download Visual Studio Code: https://code.visualstudio.com/download

b. Install Visual Studio Code on your operating system

c. Launch Visual Studio Code

d. Open the root directory of the Glover project

e. In the vscode menu, click Run -> StartDebuging -> C# -> C#: Glover

f. Wait for the Glover program to start

### Setup
a. Click the button with the "+" sign

b. In the window that opens, select the module to display, its location and click "Save"

c. Repeat the same action for all other modules

# 9. Demonstration

### Drone flying

Drone flying the deployed flight mission from Glover:
https://www.youtube.com/shorts/FBkyM7M7tIY

### Program demonstration
Glover program demonstration
https://www.youtube.com/watch?v=vpVwevA3Wz8&t=8s
