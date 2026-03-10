import sys
import os

def read_checklist(checklist_path):
    with open(checklist_path, 'r') as f:
        checklist = f.read()
    print("Checklist loaded:")
    print(checklist)
    print("-" * 40)

def list_cs_files(source_path):
    print("C# files to review:")
    for root, dirs, files in os.walk(source_path):
        for file in files:
            if file.endswith(".cs"):
                print(os.path.join(root, file))

def main():
    checklist_path = sys.argv[1]
    source_path = sys.argv[2]
    read_checklist(checklist_path)
    list_cs_files(source_path)
    # Here you would add logic to analyze each file for checklist items

if __name__ == "__main__":
    main()