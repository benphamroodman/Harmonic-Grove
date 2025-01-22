import numpy as np
import tensorflow as tf
import pyaudio
import sounddevice as sd
import aubio
import time
import socket

# Set up audio parameters
SAMPLE_RATE = 44100  # Sample rate for the microphone input
BUFFER_SIZE = 512  # Number of frames per buffer (buffer size)
HOP_SIZE = 512  # Number of frames between pitch calculations

# UDP

UDP_IP = "172.20.10.14"  # 替换为接收端的 IP 地址
#UDP_IP = "172.20.10.3"
UDP_PORT = 5005  # 替换为接收端的端口号

# Specify model and labels directly in the code
MODEL_PATH = "model3.tflite"  # Update with your model path
LABELS_PATH = "labels.txt"  # Update with your labels path


def load_labels(path):
    """從文件中加載標籤"""
    with open(path, 'r') as f:
        return {i: line.strip() for i, line in enumerate(f.readlines())}


def set_input_tensor(interpreter, audio_data):
    """設置輸入張量"""
    tensor_index = interpreter.get_input_details()[0]['index']
    input_tensor = interpreter.tensor(tensor_index)()[0]
    input_tensor[:] = audio_data


def classify_audio(interpreter, audio_data, top_k=1):
    """執行推理並返回分類結果"""
    set_input_tensor(interpreter, audio_data)
    interpreter.invoke()
    output_details = interpreter.get_output_details()[0]
    output = np.squeeze(interpreter.get_tensor(output_details['index']))

    # 如果模型是量化的，則進行去量化
    if output_details['dtype'] == np.uint8:
        scale, zero_point = output_details['quantization']
        output = scale * (output - zero_point)

    ordered = np.argpartition(-output, top_k)
    return [(i, output[i]) for i in ordered[:top_k]]


def continuous_audio_classification(interpreter,
                                    labels,
                                    chunk_size=512,
                                    rate=44100,
                                    duration=1):
    """連續音頻分類，並記錄敲擊頻率"""
    input_size = interpreter.get_input_details()[0]['shape'][1]
    buffer_size = int(rate * duration)  # 模型需要的音頻數據大小
    audio_buffer = np.zeros(buffer_size, dtype=np.int16)  # 初始化音頻緩衝區

    p = pyaudio.PyAudio()
    stream = p.open(format=pyaudio.paInt16,
                    channels=1,
                    rate=rate,
                    input=True,
                    frames_per_buffer=chunk_size)

    # 使用 aubio 偵測頻率
    pitch_detector = aubio.pitch("default", BUFFER_SIZE, HOP_SIZE, SAMPLE_RATE)
    pitch_detector.set_unit("Hz")
    pitch_detector.set_silence(-40)

    # 设置 UDP 套接字
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    print("開始連續分類...")

    has_knock = 0  # 標記是否已檢測到敲擊
    last_valid_pitch = 0.0  # 儲存上一個非 0 的有效頻率
    last_message_time = time.time()  # 上次傳送訊息的時間
    yes_detected = False  # 標記是否有Yes

    while True:
        # 獲取當前的音頻塊
        data = stream.read(chunk_size, exception_on_overflow=False)
        new_audio = np.frombuffer(data, dtype=np.int16)

        rms_volume = np.sqrt(np.mean(new_audio.astype(np.float32)**
                                     2)) / 32768.0
        # 將音頻塊大小調整為 pitch_detector 預期的大小
        if len(new_audio) > BUFFER_SIZE:
            new_audio = new_audio[:BUFFER_SIZE]
        elif len(new_audio) < BUFFER_SIZE:
            new_audio = np.pad(new_audio, (0, BUFFER_SIZE - len(new_audio)),
                               mode='constant')

        # 計算當前音頻塊的頻率
        float_audio = new_audio.astype(np.float32) / 32768.0
        detected_pitch = pitch_detector(float_audio)[0]

        # 更新上一個非 0 的有效頻率
        if detected_pitch > 0:
            last_valid_pitch = detected_pitch

        # 將新音頻數據追加到緩衝區
        audio_buffer = np.concatenate(
            (audio_buffer[len(new_audio):], new_audio))

        # 當緩衝區滿足模型輸入大小時，進行分類
        if len(audio_buffer) >= input_size:
            input_data = audio_buffer[:input_size]
            results = classify_audio(interpreter, input_data)
            # print(results)
            # 判斷是否有敲擊
            for label_id, prob in results:
                if '0' in labels[label_id]:
                    # 如果檢測到背景噪聲，重置敲擊狀態
                    has_knock += 1
                    no_message = "No,0,0"
                    #print(no_message)
                elif has_knock > 5 and prob > 0.9:
                    # 如果是敲擊且未檢測到過，記錄敲擊結果
                    yes_detected = True
                    knock_message = f"Yes,{labels[label_id]},{last_valid_pitch:.2f}"
                    #print(knock_message)
                    has_knock = 0
                    break

            #print(f"{labels[label_id]},{last_valid_pitch:.2f}")
        current_time = time.time()
        if current_time - last_message_time >= 0.2:
            if yes_detected:
                print(knock_message)
                sock.sendto(knock_message.encode("utf-8"), (UDP_IP, UDP_PORT))
                yes_detected = False
            else:
                no_message = "No,0,0"
                print(no_message)
                sock.sendto(no_message.encode("utf-8"), (UDP_IP, UDP_PORT))
            last_message_time = current_time


def main():
    labels = load_labels(LABELS_PATH)
    interpreter = tf.lite.Interpreter(model_path=MODEL_PATH)
    interpreter.allocate_tensors()

    # 啟動連續音頻分類
    continuous_audio_classification(interpreter, labels)


if __name__ == '__main__':
    main()
