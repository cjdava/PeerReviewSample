# AI Code Review System — Architecture & Setup Guide

## Overview

This repository implements an **AI-assisted automated code review pipeline** using **GitHub Agentic Workflows**, **GitHub Actions**, and a **custom engineering standards checklist**.

The system automatically performs the following when a Pull Request (PR) is opened:

1. An **AI Code Review Agent** analyzes the code.
2. The agent evaluates the code against the **PE IT Engineering Standards** checklist.
3. The agent produces a **structured code review report**.
4. A second workflow (**Enforcer**) processes the report.
5. The report is **posted to the Pull Request**.
6. If violations are detected, the workflow **fails and prevents merging**.

This creates an automated **AI-powered engineering governance system**.

---

# Key Concepts

## GitHub Workflow

A **GitHub Workflow** is an automated process defined in a YAML file located in:

```
.github/workflows/
```

Workflows run in response to events such as:

* Pull requests
* Pushes
* Scheduled triggers
* Completion of another workflow

Example event trigger:

```yaml
on:
  pull_request:
```

Workflows consist of:

* **Jobs**
* **Steps**
* **Actions**

Example workflow structure:

```yaml
name: Example Workflow

on:
  pull_request:

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
```

---

# GitHub Agentic Workflows

## What is an Agentic Workflow?

An **Agentic Workflow** is a GitHub workflow powered by an **AI agent**.

Instead of defining every step programmatically, the workflow provides **natural language instructions** to an AI model (GitHub Copilot CLI).

The AI agent then:

* reads repository context
* interprets instructions
* performs tasks dynamically

This allows developers to build **automation powered by AI reasoning** rather than static scripts.

---

## How Agentic Workflows Work

Agentic workflows use a **Markdown file** that contains:

1. YAML metadata (workflow configuration)
2. Natural language instructions (agent prompt)

Example file:

```
.github/workflows/code-review.agent.md
```

This file defines:

* workflow trigger
* permissions
* AI instructions
* expected output format

---

# Compiling Agentic Workflows

Agentic workflow Markdown files must be compiled into a standard GitHub Actions workflow.

This is done using the GitHub CLI extension:

```
gh aw compile
```

The command generates a **locked workflow file**:

```
.github/workflows/code-review.agent.lock.yml
```

The locked workflow executes the AI agent using the **GitHub Copilot CLI runtime**.

---

# Architecture of This System

The system consists of **two workflows**.

```
Pull Request
      │
      ▼
.NET Code Review Agent
      │
      ▼
AI generates review report
      │
      ▼
Enforce AI Code Review workflow
      │
      ├─ Extract report
      ├─ Comment on PR
      └─ Fail workflow if violations exist
```

---

# 1. AI Code Review Agent

## File Location

```
.github/workflows/code-review.agent.md
```

This file defines the **AI code review agent**.

---

## Trigger

The agent runs when a pull request modifies:

```
*.cs
*.sln
*.csproj
```

Example:

```yaml
on:
  pull_request:
    paths:
      - "**/*.cs"
      - "**/*.sln"
      - "**/*.csproj"
```

---

## Responsibilities of the Agent

The AI agent performs the following:

1. Retrieves the engineering standards checklist.
2. Reads modified files in the PR.
3. Analyzes the project structure.
4. Evaluates code compliance.
5. Generates a structured review report.

---

## Engineering Standards Source

The rules used by the AI are stored in a separate repository:

```
https://raw.githubusercontent.com/cjdava/best-practices/main/code-peer-review.md
```

This document contains engineering standards covering:

* Naming conventions
* Exception handling
* Null safety
* Logging practices
* Controller architecture
* Dependency injection
* Unit testing
* Code cleanliness

---

# AI Review Output Format

The AI agent produces a **structured report block**.

Example:

```
=== AI CODE REVIEW FINAL REPORT RUN_ID:22721482871 ===

PR_NUMBER: 14

CODE REVIEW SUMMARY

Checklist Findings:

- Rule: 3.3 Avoid Unchecked Null Usage
  Category: Null Reference Safety
  Severity: HIGH
  File: Models/NonCompliantModel.cs
  Line: 5
  Description: Collection declared but never initialized.

AI_CODE_REVIEW_STATUS: FAIL

=== AI CODE REVIEW END RUN_ID:22721482871 ===
```

---

## Why the Workflow Run ID is Included

The report includes the workflow run ID:

```
RUN_ID:<workflow-run-id>
```

This allows the enforcement workflow to:

* ensure the report belongs to the correct run
* avoid parsing template examples
* avoid parsing previous artifacts
* avoid parsing duplicate outputs

---

# 2. Enforce AI Code Review Workflow

## File Location

```
.github/workflows/code-review-enforcer.yml
```

---

## Trigger

The workflow runs after the AI agent completes:

```yaml
on:
  workflow_run:
    workflows: [".NET Code Review Agent"]
    types:
      - completed
```

---

## Responsibilities

The enforcement workflow performs the following tasks.

### 1. Download agent artifacts

```
gh run download <run_id>
```

This retrieves logs and outputs from the AI workflow.

---

### 2. Extract the AI review report

The workflow extracts the report block matching the workflow run ID.

Example extraction logic:

```
awk "/FINAL REPORT RUN_ID:<run_id>/,/END RUN_ID:<run_id>/"
```

---

### 3. Identify the Pull Request

The PR number is retrieved from the workflow event payload.

Example:

```
jq '.workflow_run.pull_requests[0].number'
```

---

### 4. Comment the report on the PR

The report is posted as a Pull Request comment using GitHub CLI.

Example:

```
gh pr comment <PR_NUMBER>
```

---

### 5. Enforce code quality policy

The workflow checks the report for:

```
AI_CODE_REVIEW_STATUS: FAIL
```

If detected, the workflow exits with a failure:

```
exit 1
```

This enables branch protection rules to **block merges when violations exist**.

---

# Final Pipeline

```
Developer opens Pull Request
        │
        ▼
AI Code Review Agent
        │
        ▼
AI analyzes code
        │
        ▼
AI produces structured review report
        │
        ▼
Enforce AI Code Review Workflow
        │
        ├─ Extract report
        ├─ Comment on PR
        └─ Fail workflow if violations exist
```

---

# Benefits of This System

This implementation provides:

* Automated code quality enforcement
* AI-assisted code review
* Consistent engineering standards
* Pull request feedback automation
* Merge blocking for violations

The system functions similarly to tools such as:

* SonarQube
* CodeQL
* Static code analyzers

However, it uses **AI reasoning combined with custom engineering standards**.

---

# Summary

This project demonstrates how **AI agents can be integrated into CI/CD pipelines** to automate engineering governance.

By combining:

* GitHub Agentic Workflows
* GitHub Actions
* Custom engineering standards
* AI reasoning