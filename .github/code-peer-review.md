# PE IT Engineering Standards (POC)
Version: 1.0.0
Scope: Private Equity IT (.NET Services)

This document defines simple, enforceable engineering standards
for PE IT services as part of the AI-assisted code review POC.

These rules prioritize clarity, consistency, and maintainability.

---

# 1. Naming Conventions

## 1.1 Interfaces

- All interfaces must start with the letter `I`.

Correct:
- IOrderService
- IProcessHandler

Incorrect:
- OrderService (if interface)

---

## 1.2 Classes

- Class names must use PascalCase.
- Class names must clearly describe responsibility.
- Avoid generic names like `Helper`, `Manager`, or `Processor`.

---

## 1.3 Methods

- Public and protected methods must use PascalCase.
- Private methods must use camelCase.
- Method names must describe behavior.
- Avoid ambiguous names like `HandleData()` or `Process()` without context.

Examples:

Public:
- CreateOrder()
- ValidateRequest()

Private:
- validateInput()
- mapToDomain()

---

## 1.4 Variables

- Variables must use camelCase.
- Avoid single-letter variable names (except in loops).
- Boolean variables should be prefixed with `is`, `has`, or `can`.

Example:
- isActive
- hasPermission

---

# 2. Exception Handling

## 2.1 No Silent Catch Blocks

Empty catch blocks are not allowed.

Incorrect:
try
{
    // logic
}
catch (Exception)
{
}

Exceptions must be logged or handled properly.

---

## 2.2 Do Not Swallow Exceptions

Exceptions must not be swallowed without logging.

If rethrowing, use:
throw;

Not:
throw ex;

---

## 2.3 Use Specific Exceptions

Avoid throwing generic Exception.

Incorrect:
throw new Exception("Error");

Correct:
throw new InvalidOperationException("Invalid order state.");

---

# 3. Null Reference Safety

## 3.1 Validate Method Inputs

Public methods must validate input parameters.

Example:
if (request == null)
    throw new ArgumentNullException(nameof(request));

---

## 3.2 Use Null-Coalescing Where Appropriate

Use `??` operator to provide safe defaults where applicable.

Example:
var name = inputName ?? "Unknown";

---

## 3.3 Avoid Unchecked Null Usage

- Public methods must validate input parameters.
- Use the null-coalescing operator (`??`) to provide safe defaults where appropriate.
- Do not assume injected dependencies are non-null; validate constructor parameters.
- Collections (e.g., lists, dictionaries) must be properly instantiated before use to avoid null reference exceptions.
- Avoid unchecked null usage in variables and properties.

---

# 4. Logging Best Practices

## 4.1 Do Not Log Sensitive Data

Never log:
- Passwords
- Tokens
- Personal identifiable information (PII)

---

## 4.2 Use Structured Logging

Use parameterized logging instead of string concatenation.

Incorrect:
logger.LogInformation("User " + userId + " created.");

Correct:
logger.LogInformation("User {UserId} created.", userId);

---

## 4.3 Log Exceptions Properly

Always pass exception object to logger.

Correct:
logger.LogError(ex, "Order processing failed.");

---

# 5. Controller Best Practices (APIs)

## 5.1 Controllers Must Be Thin

Controllers should:
- Validate input
- Call application layer
- Return response

Controllers must NOT:
- Contain business logic
- Access database directly
- Contain complex conditional logic

---

# 6. Dependency Injection

- All services must use constructor injection.
- Do not manually instantiate services inside classes.
- Avoid static service access.

---

# 7. Unit Testing

## 7.1 Business Logic Must Be Tested

Application layer logic must have unit tests.

---

## 7.2 No External Dependencies in Unit Tests

Unit tests must not:
- Call databases
- Call external APIs
- Depend on environment configuration

Use mocks for dependencies.

---

# 8. Code Cleanliness

## 8.1 Remove Dead Code

Unused methods, variables, and commented-out code must not be committed.

---

## 8.2 Avoid Large Methods

Methods should be small and focused.
If a method exceeds ~40–50 lines, consider refactoring.

---

# 9. Pull Request Expectations

Before merging:

- Code builds successfully
- No compiler warnings
- Unit tests pass
- No silent exception handling
- Naming conventions followed

---

# Goal of This POC

These standards aim to:

- Improve code consistency
- Reduce runtime errors
- Prevent common null reference issues
- Enforce basic architectural discipline
- Establish a quality baseline

This is a foundational set of rules and may be expanded in future iterations.