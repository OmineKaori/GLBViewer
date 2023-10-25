import sys
import json
import os
import subprocess
from ctypes import windll
import UnityEngine

pixyz_path = "Assets/Pixyz/"

def main():
    output_folder, extensions, optimization = read_config(pixyz_path + "config.json")
    input_file = read_input_file_path()
    UnityEngine.Debug.Log("input_file: " + input_file)
    UnityEngine.Debug.Log("output_folder: " + output_folder)
    executeScenarioProcessor(input_file, output_folder, extensions, optimization)

def read_input_file_path():
    with open(pixyz_path + "input_file.json") as file:
        input_file = json.load(file)['input_file']
    return input_file

def read_config(config_file):
    with open(config_file) as config:
        inputs = json.load(config)

    # convert relative path to absolute path (if needed)
    output_folder = inputs['output_folder'] 
    # convert relative path to absolute path
    output_folder = os.path.abspath(output_folder)
    extensions = inputs['extensions']
    optimization = inputs['optimization']

    return output_folder, extensions, optimization

def getFileExtension(file):
    return os.path.splitext(file)[1]

def executeScenarioProcessor(input_file, output_folder, extensions, optimization):
    args = ['C:\Program Files\PiXYZScenarioProcessor\PiXYZScenarioProcessor.exe', pixyz_path + 'scripts/sampleScript.py', input_file, output_folder, str(extensions), str(optimization)]
    print(sys.argv[0])
    print(args)
    p = subprocess.Popen(args, shell=True, stdout=subprocess.PIPE, stderr=subprocess.STDOUT, universal_newlines=True)
    while p.poll() is None:
        l = str(p.stdout.readline().rstrip()) # This blocks until it receives a newline.
        print(l)
    print(p.stdout.read())

if __name__ == "__main__":
    main()