import sys
import os

def read_checklist(checklist_path):
    if checklist_path.startswith("http"):
        import requests
        response = requests.get(checklist_path)
        checklist = response.text
    else:
        with open(checklist_path, 'r') as f:
            checklist = f.read()
    print("Checklist loaded:")
    print(checklist)
    print("-" * 40)
    return checklist

def list_cs_files(source_path):
    cs_files = []
    for root, dirs, files in os.walk(source_path):
        for file in files:
            if file.endswith(".cs"):
                cs_files.append(os.path.join(root, file))
    return cs_files

def analyze_file(filepath):
    issues = []
    with open(filepath, 'r') as f:
        content = f.read()
        if "List<" in content and "=" not in content:
            issues.append("Possible uninitialized list")
        if "null" in content and "if" not in content:
            issues.append("Possible missing null check")
    return issues

def print_issues(all_issues):
    if not all_issues:
        print("✅ No issues found.")
        return
    print("## 🚩 Peer Review Issues Found\n")
    issues_by_file = {}
    for file, msg in all_issues:
        issues_by_file.setdefault(file, []).append(msg)
    for file, msgs in issues_by_file.items():
        print(f"### {file}")
        for msg in msgs:
            print(f"- {msg}")
        print()

def main():
    checklist_path = sys.argv[1]
    source_path = sys.argv[2]
    checklist = read_checklist(checklist_path)
    cs_files = list_cs_files(source_path)
    print("C# files to review:")
    for f in cs_files:
        print(f)
    print("-" * 40)
    all_issues = []
    for f in cs_files:
        issues = analyze_file(f)
        for issue in issues:
            all_issues.append((f, issue))
    print_issues(all_issues)
    if all_issues:
        sys.exit(1)

if __name__ == "__main__":
    main()