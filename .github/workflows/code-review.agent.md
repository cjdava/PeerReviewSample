---
name: .NET Code Review Agent

on:
  pull_request:
    paths:
      - '**/*.cs'
      - '**/*.sln'
      - '**/*.csproj'

permissions:
  contents: read

description: |
  AI agent that performs automated code reviews for .NET repositories.
  The agent validates C# code against the engineering checklist stored
  in an external repository and outputs a compliance report.
  The agent analyzes both the files changed in commits and the entire repository.

---

# .NET Code Review Agent

## Objective

Review pull requests and pushes that contain C# code and determine whether the code
complies with the engineering checklist.

The agent is strictly **read-only** and must never modify the repository.

---

## Files to Analyze

Analyze only the following file types:

- `*.cs`
- `*.sln`
- `*.csproj`

Ignore all other files.

---

## External Checklist

Retrieve the engineering checklist from:

https://raw.githubusercontent.com/cjdava/best-practices/main/code-peer-review.md

Use this checklist as the rule set for compliance.

---

## Review Procedure

1. Retrieve the checklist from the external repository.
2. Identify files modified in the pull request and/or commit.
3. Filter the modified files to include only `.cs`, `.sln`, and `.csproj`.
4. Read the `.sln` file to understand the overall solution structure.
5. Read `.csproj` files to determine project dependencies.
6. Evaluate the modified C# source files and all relevant files in the repository against the checklist rules.
7. Identify any violations of the checklist.

---

## Output Format

The agent must produce a structured review report using the following format:

CODE REVIEW SUMMARY

Checklist Findings:
- [Checklist Item]: Description of non-compliance

REVIEW STATUS: FAIL

If no violations are detected:

CODE REVIEW SUMMARY

Checklist Findings:
- No violations detected

REVIEW STATUS: PASS

---

## Workflow Enforcement

Workflow Enforcement
The agent must output the compliance result (REVIEW STATUS: PASS or REVIEW STATUS: FAIL) in its report.

A subsequent workflow step must parse the agent's output:

If the output contains REVIEW STATUS: FAIL, the step must exit with a non-zero code, causing the workflow to fail.
If the output contains REVIEW STATUS: PASS, the step may exit successfully.
This ensures that branch protection rules can prevent merging when the workflow fails.

---

## Restrictions

The agent must:

- not modify repository files
- not create commits
- not open pull requests
- not suggest code changes
- not propose fixes
- not trigger other workflows

The agent must only report compliance findings.

After outputting `REVIEW STATUS`, terminate execution.