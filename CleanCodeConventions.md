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
For instance, it’s confusing to have `fetch`, `retrieve`, and `get` as equivalent methods of different classes. How do you remember which method name goes with which class

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

In the end, I wind up with functions that follow the rules I’ve laid down in this chapter. I don’t write them that way to start. I don’t think anyone could.

# Chapter 3: Formatting

## Vertical Formatting

### Vertical Openness Between Concepts
Each line represents an expression or a clause, and each group of lines represents a complete thought. Those thoughts should be separated from each other with blank lines.

```java
package fitnesse.wikitext.widgets;

import java.util.regex.*;

public class BoldWidget extends ParentWidget {
   public static final String REGEXP = "'''.+?'''";
   private static final Pattern pattern = Pattern.compile("'''(.+?)'''",
      Pattern.MULTILINE + Pattern.DOTALL
   );

   public BoldWidget(ParentWidget parent, String text) throws Exception {
      super(parent);
      Matcher match = pattern.matcher(text);
      match.find();
      addChildWidgets(match.group(1));
   }

   public String render() throws Exception {
      StringBuffer html = new StringBuffer("<b>");
      html.append(childHtml()).append("</b>");
      return html.toString();
   }
}
```
The difference between these two listings is a bit of vertical openness.
```java
package fitnesse.wikitext.widgets;
import java.util.regex.*;
public class BoldWidget extends ParentWidget {
   public static final String REGEXP = "'''.+?'''";
   private static final Pattern pattern = Pattern.compile("'''(.+?)'''",
      Pattern.MULTILINE + Pattern.DOTALL);
   public BoldWidget(ParentWidget parent, String text) throws Exception {
      super(parent);
      Matcher match = pattern.matcher(text);
      match.find();
      addChildWidgets(match.group(1));}
   public String render() throws Exception {
      StringBuffer html = new StringBuffer("<b>");
      html.append(childHtml()).append("</b>");
      return html.toString();
   }
}
```
### Vertical Distance
We want to avoid forcing our readers to hop around through our source
files and classes.

   - #### Variable Declarations
   Variables should be declared as close to their usage as possible. Because our functions are very short, local variables should appear a the top of    each function.

   - #### Instance variables
   on the other hand, should be declared at the top of the class.

   - #### Dependent Functions
   If one function calls another, they should be vertically close,
and the caller should be above the callee, if at all possible

   - #### Conceptual Affinity
Certain bits of code want to be near other bits. They have a certain conceptual affinity. Affinity might be caused because a group of functions perform a similar operation.

```java
public class Assert {
   static public void assertTrue(String message, boolean condition) {
      if (!condition)
         fail(message);
   }
   static public void assertTrue(boolean condition) {
      assertTrue(null, condition);
   }
   static public void assertFalse(String message, boolean condition) {
      assertTrue(message, !condition);
   }
   static public void assertFalse(boolean condition) {
      assertFalse(null, condition);
   }
...
```

# Chapter 4: Objects and Data Structures
## Data Abstraction
Both represent the data of a point on the Cartesian plane. And yet one exposes its implementation and the other completely hides it.

```java
// Abstract Point
public interface Point {
   double getX();
   double getY();
   void setCartesian(double x, double y);
   double getR();
   double getTheta();
   void setPolar(double r, double theta);
}
```

The methods enforce an access policy. You can read the individual coordinates independently, but you must set the coordinates together as an atomic operation.

```java
// Concrete Point
public class Point {
   public double x;
   public double y;
}
```

On the other hand, is very clearly implemented in rectangular coordinates, and it forces us to manipulate those coordinates independently. This exposes implementation.

Hiding implementation is not just a matter of putting a layer of functions between the variables. Hiding implementation is about abstractions!

## Data/Object Anti-Symmetry

>Procedural code (code using data structures) makes it easy to add new functions without changing the existing data structures. OO code, on the other hand, makes it easy to add new classes without changing existing functions.

The complement is also true:
> Procedural code makes it hard to add new data structures because all the functions must change. OO code makes it hard to add new functions because all the classes must change

In any complex system there are going to be times when we want to add new data types rather than new functions. For these cases objects and OO are most appropriate.

## The Law of Demeter
The Law of Demeter says that a method f of a class C should only call
the methods of these:
   - `C`
   - An object created by `f`
   - An object passed as an argument to `f`
   - An object held in an instance variable of `C`

The method should not invoke methods on objects that are returned by any of the allowed functions. In other words, talk to friends, not to strangers.
The following code appears to violate the Law of Demeter:
```java
final String outputDir = ctxt.getOptions().getScratchDir().getAbsolutePath();
```
### Train Wrecks
This kind of code is often called a train wreck because it look like a bunch of coupled train cars. Chains of calls like this are generally considered to be sloppy style and should be avoided.

It is usually best to split them up as follows:
```java
Options opts = ctxt.getOptions();
File scratchDir = opts.getScratchDir();
final String outputDir = scratchDir.getAbsolutePath();
```

If they are objects, then their internal structure should be hidden rather than exposed, and so knowledge of their innards is a clear violation of the Law of Demeter. On the other hand, if `ctxt`, `Options`, and `ScratchDir` are just data structures with no behavior, then they naturally expose their internal structure, and so Demeter does not apply.
```java
final String outputDir = ctxt.options.scratchDir.absolutePath;
```

### Hybrids
This confusion sometimes leads to unfortunate hybrid structures that are half object and half data structure.

Such hybrids make it hard to add new functions but also make it hard to add new data structures. They are the worst of both worlds. Avoid creating them.

### Hiding Structure
because objects are supposed to hide their internal structure, we should not be able to navigate through them. How then would we get the absolute path of the scratch directory
```java
ctxt.getAbsolutePathOfScratchDirectoryOption();
```
or
```java
ctx.getScratchDirectoryOption().getAbsolutePath()
```
The first option could lead to an explosion of methods in the ctxt object. The second presumes that `getScratchDirectoryOption()` returns a data structure, not an object.

Consider this code from (many lines farther down in) the same module:
```java
String outFile = outputDir + "/" + className.replace('.', '/') + ".class";
FileOutputStream fout = new FileOutputStream(outFile);
BufferedOutputStream bos = new BufferedOutputStream(fout);
```
We see that the intent of getting the absolute path of the scratch directory was to create a scratch file of a given name.

If ctxt is an object, we should be telling it to do *something*. So, what if we told the ctxt object to do this?
```java
BufferedOutputStream bos = ctxt.createScratchFileStream(classFileName);
```

# Chapter 5: Error Handling
## Don’t Return Null
```java
public void registerItem(Item item) {
   if (item != null) {
      ItemRegistry registry = peristentStore.getItemRegistry();
      if (registry != null) {
         Item existing = registry.getItem(item.getID());
         if (existing.getBillingPeriod().hasRetailOwner()) {
            existing.register(item);
         }
      }
   }
}
```
If you work in a code base with code like this, it might not look all that bad to you, but it is bad! When we return null, we are essentially creating work for ourselves and foisting problems upon our callers.

Imagine that you have code like this:
```java
List<Employee> employees = getEmployees();
if (employees != null) {
   for(Employee e : employees) {
      totalPay += e.getPay();
   }
}
```
Right now, getEmployees can return null, but does it have to? If we change `getEmployee` so that it returns an empty list, we can clean up the code:
```java
List<Employee> employees = getEmployees();
for(Employee e : employees) {
   totalPay += e.getPay();
}
```
If you code this way, you will minimize the chance of NullPointerExceptions and your code will be cleaner.

# Chapter 6: Class
## Class Organization
Following the standard Java convention, a class should begin with a list of variables.
   1. Public static constants, if any, should come first.
   2. Then private static variables.
   3. Followed by private instance variables.
   4. There is seldom a good reason to have a public variable.

## Classes Should Be Small!
The first rule of classes is that they should be small. The second rule of classes is that they should be smaller than that.

The name of a class should describe what responsibilities it fulfills. In fact, naming is probably the first way of helping determine class size.
   > For example, class names including weasel words like `Processor` or `Manager` or `Super` often hint at unfortunate aggregation of responsibilities.

## The Single Responsibility Principle
The [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle) states that a class or module should have one, and only one, reason to change.

This principle gives us both a definition of responsibility, and a guidelines for class size. Classes should have one responsibility—one reason to change.

We want our systems to be composed of many small classes, not a few large ones. Each small class encapsulates a single responsibility, has a single reason to change, and collaborates with a few others to achieve the desired system behaviors.

## Cohesion
Classes should have a small number of instance variables. Each of the methods of a class should manipulate one or more of those variables. In general the more variables a method manipulates the more cohesive that method is to its class.

The strategy of keeping functions small and keeping parameter lists short can sometimes lead to a proliferation of instance variables that are used by a subset of methods.

When this happens, it almost always means that there is at least one other class trying to get out of the larger class. You should try to separate the variables and methods into two or more classes such that the new classes are more cohesive.

# Chapter 7: Emergence
## Getting Clean via Emergent Design
What if there were four simple rules that you could follow that would help you create good designs as you worked? According to Kent, a design is “simple” if it follows these rules:
   - Runs all the tests
   - Contains no duplication
   - Expresses the intent of the programmer
   - Minimizes the number of classes and methods

The rules are given in order of importance

## Simple Design Rule 1: Runs All the Tests
## Simple Design Rules 2–4: Refactoring
During this refactoring step, we can apply anything from the entire body of knowledge about good software design. We can increase cohesion, decrease coupling, separate concerns, modularize system concerns, shrink our functions and classes, choose better names, and so on.

This is also where we apply the final three rules of simple design: Eliminate duplication, ensure expressiveness, and minimize the number of classes and methods.

## No Duplication
Duplication is the primary enemy of a well-designed system. It represents additional work, additional risk, and additional unnecessary complexity.
The [TEMPLATE METHOD](https://refactoring.guru/design-patterns/template-method) pattern is a common technique for removing higher-level
duplication.

## Expressive
You can express yourself by choosing good names. We want to be able to hear a class or function name and not be surprised when we discover its responsibilities.

You can also express yourself by keeping your functions and classes small. Small classes and functions are usually easy to name, easy to write, and easy to understand.

You can also express yourself by using standard nomenclature. Design patterns, for example, are largely about communication and expressiveness. By using the standard pattern names, such as COMMAND or VISITOR, in the names of the classes that implement those patterns, you can succinctly describe your design to other developers

## Minimal Classes and Methods
In an effort to make our classes and methods small, we might create too many tiny classes and methods. So this rule suggests that we also keep our function and class counts low.

# Chapter 8: Smells and Heuristic
## Functions
### F1: Too Many Arguments
Functions should have a small number of arguments. No argument is best, followed by one, two, and three. More than three is very questionable and should be avoided.

### F2: Output Arguments
Output arguments are counterintuitive. Readers expect arguments to be inputs, not outputs. If your function must change the state of something, have it change the state of the object it is called on.