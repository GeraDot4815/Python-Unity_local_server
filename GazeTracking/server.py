import socket
import cv2
from gaze_tracking import GazeTracking

gaze = GazeTracking()
webcam = cv2.VideoCapture(0)

def Start_Server():
    #try:
        server=socket.socket(socket.AF_INET, socket.SOCK_STREAM)

        IP="127.0.0.1"
        PORT=8080

        server.bind((IP, PORT))

        server.listen(4)
        print("ожидание подключения...")
        user, adres = server.accept()
        print("connect")
        user.send("connect".encode("utf-8"))
        while True:
            Do_Gaze_Tracking()
            data=user.recv(1024)#.decode("utf-8")
            print("Пришли данные: "+data.decode("utf-8"))
            user.send(answer)
#            user.shutdown(socket.SHUT_WR)
    #except:
        #server.close()
        #print("WTF? Server closed!")

def Do_Gaze_Tracking():
    global answer
    answer=""
    # We get a new frame from the webcam
    _, frame = webcam.read()

    # We send this frame to GazeTracking to analyze it
    gaze.refresh(frame)

    frame = gaze.annotated_frame()

    left_pupil = gaze.pupil_left_coords()
    right_pupil = gaze.pupil_right_coords()
    blincing=gaze.is_blinking()

    answer=(str(left_pupil)+":"+str(right_pupil)+":"+str(blincing)).encode("utf-8")

if __name__=='__main__':
    Start_Server()

    webcam.release()
    cv2.destroyAllWindows()