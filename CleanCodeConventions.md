# Clean Code Conventions

# Chapter 1: Meaningful Names
## Use Intention-Revealing Names
The name of a variable, function, or class, should answer all the big questions. It should tell you why it exists, what it does, and how it is used.

## Avoid Disinformation
Avoid leaving false clues that obscure the meaning of code.
   - Do not refer to a grouping of accounts as an `accountList` unless it’s actually a `List`.
The word **list** means something specific to programmers.

Beware of using names which vary in small ways.
   - How long does it take to spot the subtle difference between a `XYZControllerForEfficientHandlingOfStrings` in one module and, somewhere a little more distant, `XYZControllerForEfficientStorageOfStrings`?

## Make Meaningful Distinctions
If names must be different, then they should also mean something different.

   - If you have another called `ProductInfo` or `ProductData`, you have made the names different without making them mean anything different. `Info` and `Data` are indistinct noise words like `a`, `an`, and `the`.

## Use Pronounceable Names
```java
class DtaRcrd102 {
   private Date genymdhms;
   private Date modymdhms;
   private final String pszqint = "102";
   /* ... */
};
```
---
```java
class Customer {
   private Date generationTimestamp;
   private Date modificationTimestamp;;
   private final String recordId = "102";
   /* ... */
};
```

## Use Searchable Names
Single-letter names can **ONLY** be used as local variables inside short methods.
```java
for (int j=0; j<34; j++) {
   s += (t[j]*4)/5;
}
```
---
```java
int realDaysPerIdealDay = 4;
const int WORK_DAYS_PER_WEEK = 5;
int sum = 0;
for (int j=0; j < NUMBER_OF_TASKS; j++) {
   int realTaskDays = taskEstimate[j] * realDaysPerIdealDay;
   int realTaskWeeks = (realdays / WORK_DAYS_PER_WEEK);
   sum += realTaskWeeks;
}
```

## Avoid Encodings
### Member Prefixes
- You also don’t need to prefix member variables with m_ anymore. Your classes and functions should be small enough that you don’t need them.
```java
public class Part {
   private String m_dsc; // The textual description
      void setName(String name) {
      m_dsc = name;
   }
}
```
---
```java
public class Part {
String description;
   void setDescription(String description) {
      this.description = description;
   }
}
```

## Class Names
Classes and objects should have noun or noun phrase names like `Customer`, `WikiPage`, `Account`, and `AddressParser`. Avoid words like `Manager`, `Processor`, `Data`, or `Info` in the name of a class. A class name should not be a verb.

## Method Names
Methods should have verb or verb phrase names like `postPayment`, `deletePage`, or `save`. **Accessors**, **mutators**, and **predicates** should be named for their value and prefixed with `get`, `set`, and `is`.

## Pick One Word per Concept
For instance, it’s confusing to have fetch, retrieve, and get as equivalent methods of different classes. How do you remember which method name goes with which class

## Don’t Pun
Avoid using the same word for two purposes. Using the same term for two different ideas is essentially a pun.

## Use Solution Domain Names
Remember that the people who read your code will be programmers. So go ahead and use computer science (CS) terms, algorithm names, pattern names, math terms, and so forth.

## Add Meaningful Context
There are a few names which are meaningful in and of themselves—most are not. Instead, you need to place names in context for your reader by enclosing them in well-named classes, functions, or namespaces. When all else fails, then prefixing the name may be necessary as a last resort.

# Chapter 2: Functions

## Small!
The first rule of functions is that they should be small. The second rule of functions is that _they should be smaller than that_.
   - The blocks within `if` statements, `else` statements, while statements, and so on should be one line long.

   - This also implies that functions should not be large enough to hold nested structures. The indent level of a function should not be greater than one or two.

## Do One Thing
*Function should do one thing. They should do it well. They should do it only.*
   - If a function does only those steps that are one level below the stated name of the function, then the function is doing one thing.

   - The reason we write functions is to decompose a larger concept (in other words, the name of the function) into a set of steps at the next level of abstraction

## One Level of Abstraction per Function
In order to make sure our functions are doing “one thing,” we need to make sure that the statements within our function are all at the same level of abstraction. Example of bad code function:
   - A function which there are concepts in there that are at a very high level of abstraction, such as `getHtml()`; others that are at an intermediate level of abstraction, such as: `String pagePathName = PathParser.render(pagePath);` and still others that are remarkably low level, such as: `.append("\n")`.

## Reading Code from Top to Bottom: The Stepdown Rule
We want every function to be followed by those at the next level of abstraction so that we can read the program, descending one level of abstraction at a time as we read down the list of functions.

## Switch Statements
Unfortunately we can’t always avoid switch statements, but we can make sure that each `switch` statement is buried in a low-level class and is never repeated. We do this, of course, with polymorphism.
```java
public Money calculatePay(Employee e)
   throws InvalidEmployeeType {
   switch (e.type) {
      case COMMISSIONED:
         return calculateCommissionedPay(e);
      case HOURLY:
         return calculateHourlyPay(e);
      case SALARIED:
         return calculateSalariedPay(e);
      default:
         throw new InvalidEmployeeType(e.type);
   }
}
```
There are several problems with this function:
- It’s large, and when new employee types are added, it will grow.
- It very clearly does more than one thing.
- It violates the [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle) because there is more than one reason for it to change.
-  It violates the [Open Closed Principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) because it must change whenever new types are added.

The solution to this problem (see Listing 3-5) is to bury the switch statement in the basement of an [ABSTRACT FACTORY](https://refactoring.guru/design-patterns/abstract-factory).
```java
public abstract class Employee {
   public abstract boolean isPayday();
   public abstract Money calculatePay();
   public abstract void deliverPay(Money pay);
}
-----------------
public interface EmployeeFactory {
   public Employee makeEmployee(EmployeeRecord r) throws InvalidEmployeeType;
}
-----------------
public class EmployeeFactoryImpl implements EmployeeFactory {
   public Employee makeEmployee(EmployeeRecord r) throws InvalidEmployeeType {
      switch (r.type) {
      case COMMISSIONED:
         return new CommissionedEmployee(r) ;
      case HOURLY:
         return new HourlyEmployee(r);
      case SALARIED:
         return new SalariedEmploye(r);
      default:
         throw new InvalidEmployeeType(r.type);
      }
   }
}
```

## Use Descriptive Names
- Don’t be afraid to spend time choosing a name.

- Choosing descriptive names will clarify the design of the module in your mind and help you to improve it.

- Be consistent in your names. Use the same phrases, nouns, and verbs in the function names you choose for your modules.

## Function Arguments
Three arguments should be avoided where possible. More than three requires very special justification—and then shouldn’t be used anyway.

### Common Monadic Forms (Single Argument Function)
You may be asking a question about that argument, as in `boolean fileExists(“MyFile”).`

Or you may be operating on that argument, transforming it into something else and returning it. For example, `InputStream fileOpen(“MyFile”)` transforms a file name String into an InputStream return value.
### Flag Arguments
Flag arguments are ugly. Passing a boolean into a function is a truly terrible practice, We should have split the function in two instead.
   > Still, the method call `render(true)` is just plain confusing to a poor reader. Mousing over the call and seeing `render(boolean isSuite)` helps a little, but not that much. We should have split the function into two: `renderForSuite()` and `renderForSingleTest()`.

### Dyadic Functions
There are times, of course, where two arguments are appropriate. For example,
Point `p = new Point(0,0);` is perfectly reasonable.

You should be aware that they come at a cost and should take advantage of what mechanims may be available to you to convert them into monads.
   > For example, you might make the `writeField` method a member of `outputStream` so that you can say `outputStream.writeField(name)`. Or you might make the `outputStream` a member variable of the current class so that you don’t have to pass it. Or you might extract a new class like `FieldWriter` that takes the `outputStream` in its constructor and has a write method.

### Argument Objects
When a function seems to need more than two or three arguments, it is likely that some of those arguments ought to be wrapped into a class of their own.
```java
Circle makeCircle(double x, double y, double radius);
Circle makeCircle(Point center, double radius);
```

### Argument Lists
Consider, for example, the `String.format` method:
```java
String.format("%s worked %.2f hours.", name, hours);
```
If the variable arguments are all treated identically, as they are in the example above, then they are equivalent to a single argument of type `List`.

### Verbs and Keywords
In the case of function with only argument, the function and argument should form a very nice verb/noun pair. For example, `write(name)` is very evocative.

Using this form we encode the names of the arguments into the function name. For example, `assertEquals` might be better written as `assertExpectedEqualsActual(expected, actual)`.

### Output Arguments
Arguments are most naturally interpreted as inputs to a function. For example:
```java
appendFooter(s);
```
Does this function append s as the footer to something? Or does it append some footer to s? Is s an input or an output? It doesn’t take long to look at the function signature and see:
```java
public void appendFooter(StringBuffer report)
```
In other words, it would be better for appendFooter to be invoked as
```java
report.appendFooter();
```

### Prefer Exceptions to Returning Error Codes
When you return an error code, you create the problem that the caller must deal with the error immediately.

```java
if (deletePage(page) == E_OK)
```
---
```java
if (deletePage(page) == E_OK) {
   if (registry.deleteReference(page.name) == E_OK) {
      if (configKeys.deleteKey(page.name.makeKey()) == E_OK){
         logger.log("page deleted");
      } else {
         logger.log("configKey not deleted");
      }
   } else {
   logger.log("deleteReference from registry failed");
   }
} else {
   logger.log("delete failed");
   return E_ERROR;
}
```
On the other hand, if you use exceptions instead of returned error codes, then the error processing code can be separated from the happy path code and can be simplified:
```java
try {
   deletePage(page);
   registry.deleteReference(page.name);
   configKeys.deleteKey(page.name.makeKey());
}
catch (Exception e) {
   logger.log(e.getMessage());
}
```

## Don’t Repeat Yourself
Duplication may be the root of all evil in software. Many principles and practices have been created for the purpose of controlling or eliminating it.

## Structured Programming
Dijkstra said that every function, and every block within a function, should have one entry and one exit. 
   > Following these rules means that there should only be one return statement in a function, no `break` or `continue` statements in a loop, and never, ever, any `goto` statements.

So if you keep your functions small, then the occasional multiple `return`, `break`, or `continue` statement does no harm and can sometimes even be more expressive than the single-entry, single-exit rule. On the other hand, `goto` only makes sense in large functions, so it should be avoided.

## How Do You Write Functions Like This?
When I write functions, they come out long and complicated. They have lots of indenting and nested loops. They have long argument lists. The names are arbitrary, and there is duplicated code. But I also have a suite of unit tests that cover every one of those clumsy lines of code.

So then I massage and refine that code, splitting out functions, changing names, eliminating duplication. I shrink the methods and reorder them. Sometimes I break out whole classes, all the while keeping the tests passing.

In the end, I wind up with functions that follow the rules I’ve laid down in this chapter. I don’t write them that way to start. I don’t think anyone could