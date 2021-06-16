# Refactoring Techniques
## Format of the Refactorings
As I describe the refactorings in this and other chapters, I use a standard format. Each refactoring has five parts, as follows:

- I begin with a **name**. The name is important to building a vocabulary of refactorings. This is the name I use elsewhere in the book.

- I follow the name with a short **summary** of the situation in which you need the refactoring and a summary of what the refactoring does. This helps you find a refactoring more quickly.

- The **motivation** describes why the refactoring should be done and describes
circumstances in which it shouldn't be done.

- The **mechanics** are a concise, step-by-step description of how to carry out the
refactoring.

- The **examples** show a very simple use of the refactoring to illustrate how it works.

## Chapter 1. Composing Methods

### Extract Method

You have a code fragment that can be grouped together.

Turn the fragment into a method whose name explains the purpose of the method.
```java
void printOwing(double amount) {
    printBanner();
    //print details
    System.out.println ("name:" + _name);
    System.out.println ("amount" + amount);
}
```
---
```java
void printOwing(double amount) {
    printBanner();
    printDetails(amount);
}

void printDetails (double amount) {90
    System.out.println ("name:" + _name);
    System.out.println ("amount" + amount);
}
```

Motivation
- First, it increases the chances that other methods can use a method when the method is finely grained.

- Second, it allows the higher-level methods to read more like a series of comments.

- Small methods really work only when you have good names, so you need to pay attention to naming.

### Inline Method
A method's body is just as clear as its name.

Put the method's body into the body of its callers and remove the method.
```java
int getRating() {
    return (moreThanFiveLateDeliveries()) ? 2 : 1;
}

boolean moreThanFiveLateDeliveries() {
    return _numberOfLateDeliveries > 5;
}
```
```java
int getRating() {
    return (_numberOfLateDeliveries > 5) ? 2 : 1;
}
```

Motivation
- sometimes you do come across a method in which the body is as clear as the name. When this happens, you should then get rid of the method.

- Another time to use Inline Method is when you have a group of methods that seem badly factored. You can inline them all into one big method and then reextract the methods.

- I commonly use Inline Method when someone is using too much indirection and it seems that every method does simple delegation to another method, and I get lost in all the delegation.

### Inline Temp
You have a temp that is assigned to once with a simple expression, and the temp is getting in the way of other refactorings.

```java
double basePrice = anOrder.basePrice();
return (basePrice > 1000)
```
---
```java
return (anOrder.basePrice() > 1000)
```

Motivation
- Most of the time *Inline Temp* is used as part of **Replace Temp with Query**, so the real motivation is there.

- he only time *Inline Temp* is used on its own is when you find a temp that is assigned the value of a method call. Often this temp isn't doing any harm and you can safely leave it there.

- If the temp is getting in the way of other refactorings, such as **Extract Method**, it's time to inline it.

### Replace Temp with Query
You are using a temporary variable to hold the result of an expression.

Extract the expression into a method. Replace all references to the temp with the expression. The new method can then be used in other methods.

```java
double basePrice = _quantity * _itemPrice;
if (basePrice > 1000)
    return basePrice * 0.95;
else
    return basePrice * 0.98;98

```
---
```java
if (basePrice() > 1000)
    return basePrice() * 0.95;
else
    return basePrice() * 0.98;
...
double basePrice() {
    return _quantity * _itemPrice;
}
```

Motivation
- The problem with temps is that they are temporary and local, temps tend to encourage longer methods.

- By replacing the temp with a query method, any method in the class can get at the information. That helps a lot in coming up with cleaner code for the class.

- `Replace Temp with Query` often is a vital step before **Extract Method**. Local variables make it difficult to extract, so replace as many variables as you can with queries.

- Other cases are trickier but possible. You may need to use **Split Temporary Variable** or **Separate Query from Modifier** first to make things easier.
    > If the temp is used to collect a result (such as summing over a loop), you need to copy some logic into the query method

### Introduce Explaining Variable
You have a complicated expression

Put the result of the expression, or parts of the expression, in a temporary variable with a name that explains the purpose.

```java
if ((platform.toUpperCase().indexOf("MAC") > -1) &&
     (browser.toUpperCase().indexOf("IE") > -1) &&
      wasInitialized() && resize > 0 )
{
// do something
}
```
---
```java
final boolean isMacOs = platform.toUpperCase().indexOf("MAC") > -1;
final boolean isIEBrowser = browser.toUpperCase().indexOf("IE") > -1;
final boolean wasResized = resize > 0;

if (isMacOs && isIEBrowser && wasInitialized() && wasResized) {
    // do something
}
```

Motivation
- Expressions can become very complex and hard to read. In such situations temporary variables can be helpful to break down the expression into something more manageable.

- *Introduce Explaining* Variable is particularly valuable with conditional logic in which it is useful to take each clause of a condition and explain what the condition means with a well-named temp.

- Another case is a long algorithm, in which each step in the computation can be explained with a temp.

- I almost always prefer to use **Extract Method** if I can. A temp is useful only within the context of one method. A method is useable throughout the object and to other objects.

- There are times, however, when local variables make it difficult to use **Extract Method**. That's when I use **Introduce Explaining Variable**.

### Split Temporary Variable
You have a temporary variable assigned to more than once, but is not a loop variable nor a collecting temporary variable.

Make a separate temporary variable for each assignment.
```java
double temp = 2 * (_height + _width);
System.out.println (temp);
temp = _height * _width;
System.out.println (temp);
```
---
```java
final double perimeter = 2 * (_height + _width);
System.out.println (perimeter);
final double area = _height * _width;
System.out.println (area);
```

Motivation
- Any variable with more than one responsibility should be replaced with a temp for each responsibility. Using a temp for two different things is very confusing for the reader.

### Remove Assignments to Parameters
The code assigns to a parameter.

Use a temporary variable instead
```java
int discount (int inputVal, int quantity, int yearToDate) {
if (inputVal > 50) inputVal -= 2;
```
---
```java
int discount (int inputVal, int quantity, int yearToDate) {
    int result = inputVal;
    if (inputVal > 50) result -= 2;
```

Motivation
- The reason I don't like this comes down to lack of clarity and to confusion
between pass by value and pass by reference.

- The other area of confusion is within the body of the code itself. It is much clearer if you use only the parameter to represent what has been passed in, because that is a consistent usage.

### Replace Method with Method Object
You have a long method that uses local variables in such a way that you cannot apply **Extract Method**.

Turn the method into its own object so that all the local variables become fields on that object. You can then decompose the method into other methods on the same object.
```java
class Order...
    double price() {
        double primaryBasePrice;
        double secondaryBasePrice;
        double tertiaryBasePrice;
        // long computation;
        ...
    }
```
---
![Image](https://i.imgur.com/kkcRgHm.png)

Motivation
- In this book I emphasize the beauty of small methods. By extracting pieces out of a large method, you make things much more comprehensible.

- The difficulty in decomposing a method lies in local variables. If they are rampant, decomposition can be difficult. Using **Replace Temp with Query** helps to reduce this burden.

- Applying **Replace Method with Method** Object turns all these local variables into fields on the method object. You can then use **Extract Method** on this new object to create additional methods that break down the original method.

### Substitute Algorithm
You want to replace an algorithm with one that is clearer.

Replace the body of the method with the new algorithm.

```java
String foundPerson(String[] people){
    for (int i = 0; i < people.length; i++) {
        if (people[i].equals ("Don")) {
            return "Don";
        }
        if (people[i].equals ("John")) {
            return "John";114
        }
        if (people[i].equals ("Kent")) {
            return "Kent";
        }
    }
    return "";
}
```
---
```java
String foundPerson(String[] people){
    List candidates = Arrays.asList(new String[] {"Don", "John", "Kent"});

    for (int i=0; i<people.length; i++)
        if (candidates.contains(people[i]))
            return people[i];

    return "";
}
```

Motivation
- Sometimes when you want to change the algorithm to do something slightly different, it is easier to subtitute the algorithm first into something easier for the change you need to make.

- Substituting a large, complex algorithm is very difficult; only by making it simple can you make the substitution tractable.

## Chapter 2. Moving Features Between Objects
### Move Method
A method is, or will be, using or used by more features of another class than the class on which it is defined.

Create a new method with a similar body in the class it uses most. Either turn the old method into a simple delegation, or remove it altogether.

![](https://i.imgur.com/AlRVIxg.png)

Motivation
- I move methods when classes have too much behavior or when classes are collaborating too much and are too highly coupled.

- I usually look through the methods on a class to find a method that seems to reference another object more than the object it lives on.

### Move Field
A field is, or will be, used by another class more than the class on which it is defined.

Create a new field in the target class, and change all its users.

![](https://i.imgur.com/ByMlrrJ.png)

Motivation

- I consider moving a field if I see more methods on another class using the field than the class itself.

- Another reason for field moving is when doing **Extract Class**. In that case the fields go first and then the methods.

### Extract Class
You have one class doing work that should be done by two.

Create a new class and move the relevant fields and methods from the old class into the new class.

Motivation

- Such a class is one with many methods and quite a lot of data. A good sign is that a subset of the data and a subset of the methods seem to go together.

### Inline Class
A class isn't doing very much.

Move all its features into another class and delete it.

![](https://i.imgur.com/QMnkaIU.png)

Motivation

- Inline Class is the reverse of Extract Class. I use Inline Class if a class is no longer pulling its weight and shouldn't be around any more

- Often this is the result of refactoring that moves other responsibilities out of the class so there is little left.

### Hide Delegate
A client is calling a delegate class of an object.

Create methods on the server to hide the delegate.

![](https://i.imgur.com/oFuk8jJ.png)

Motivation
- Encapsulation means that objects need to know less about other parts of the system. Then when things change, fewer objects need to be told about the change.

- If a client calls a method defined on one of the fields of the server object, the client needs to know about this delegate object. If the delegate changes, the client also may have to change.
    > You can remove this dependency by placing a simple delegating method on the server, which hides the delegate `(Figure 7.1)`. Changes become limited to the server and don't propagate to the client.

    ![](https://i.imgur.com/nyDxS85.png)

### Remove Middle Man
A class is doing too much simple delegation.

Get the client to call the delegate directly.

![](https://i.imgur.com/tmaKxkW.png)

Motivation
- Every time the client wants to use a new feature of the delegate, you have to add a simple delegating method to the server. After adding features for a while, it becomes painful and perhaps it's time for the client to call the delegate directly.

### Introduce Foreign Method
```java
Date newStart = new Date (previousEnd.getYear(),
    previousEnd.getMonth(), previousEnd.getDate() + 1);
```
---
```java
Date newStart = nextDay(previousEnd);

private static Date nextDay(Date arg) {
    return new Date (arg.getYear(),arg.getMonth(), arg.getDate() + 1);
}
```

Motivation
- You are using this really nice class that gives you all these great services. Then there is one service it doesn't give you but should.

### Introduce Local Extension
A server class you are using needs several additional methods, but you can't modify the class.

Create a new class that contains these extra methods. Make this extension class a subclass or a wrapper of the original.

![](https://i.imgur.com/9wvshMf.png)

Motivation
- If you need one or two methods, you can use **Introduce Foreign Method**. Once you get beyond a couple of these methods, however, they get out of hand.

## Chapter 3. Organizing Data

### Self Encapsulate Field
```java
private int _low, _high;
boolean includes (int arg) {
    return arg >= _low && arg <= _high;
}
```
---
```java
private int _low, _high;
boolean includes (int arg) {
    return arg >= getLow() && arg <= getHigh();
}
int getLow() {return _low;}
int getHigh() {return _high;}
```

Motivation
- The advantages of *indirect variable access* are that it allows a subclass to override how to get that information with a method and that it supports more flexibility in managing the data.
    > Such as lazy initialization, which initializes the value only when you need to use it.

- The advantage of direct variable access is that the code is easier to read.

- The most important time to use *Self Encapsulate Field* is when you are accessing a field in a superclass but you want to override this variable access with a computed value in the subclass.

### Replace Data Value with Object
You have a data item that needs additional data or behavior.

Turn the data item into an object.

![](https://i.imgur.com/ziO7uCQ.png)

Motivation

- Often in early stages of development you make decisions about representing simple facts as simple data items. As development proceeds you realize that those simple items aren't so simple anymore.

### Change Value to Reference
You have a class with many equal instances that you want to replace with a single object.

Turn the object into a reference object.

![](https://i.imgur.com/ElNDk3D.png)

Motivation

- The decision between reference and value is not always clear. Sometimes you start with a simple value with a small amount of immutable data. Then you want to give it some changeable data and ensure that the changes ripple to everyone referring to the object

### Change Reference to Value
You have a reference object that is small, immutable, and awkward to manage.

Turn it into a value object.

![](https://i.imgur.com/McW7e1L.png)

Motivation

- The trigger for going from a reference to a value is that working with the reference object becomes awkward.

- An important property of value objects is that they should be *immutable*. Any time you invoke a query on one, you should get the same result.
    > It's important to be clear on what immutable means. If you have a money class with a currency and a value, that's usually an immutable value object. That does not mean your salary cannot change. It means that to change your salary, you need to replace the existing money object with a new money object rather than changing the amount on an exisiting money object.

### Replace Array with Object
You have an array in which certain elements mean different things.

Replace the array with an object that has a field for each element.

```java
String[] row = new String[3];
row [0] = "Liverpool";
row [1] = "15";
```
---
```java
Performance row = new Performance();
row.setName("Liverpool");
row.setWins("15");
```

Motivation
-   Arrays are a common structure for organizing data. However, they should be used only to contain a collection of similar objects in some order.
    > Sometimes, however, you see them used to contain a number of different things. Conventions such as "the first element on the array is the person's name" are hard to remember.

    With an object you can use names of fields and methods to convey
    this information so you don't have to remember it.

### Duplicate Observed Data (Unnecessary, Here For Fun)
You have domain data available only in a GUI control, and domain methods need access.

Copy the data to a domain object. Set up an observer to synchronize the two pieces of data.

![](https://i.imgur.com/xgsWj98.png)

Motivation

- A well-layered system separates code that handles the user interface from code that handles the business logic. It does this for several reasons. You may want several interfaces for similar business logic; the user interface becomes too complicated if it does both; it is easier to maintain and evolve domain objects separate from the GUI; or you may have different developers handling the different pieces.

- Although the behavior can be separated easily, the data often cannot. Data needs to be embedded in GUI control that has the same meaning as data that lives in the domain model. User interface frameworks, from model-view-controller (MVC) onward, used a multitiered ystem to provide mechanisms to allow you to provide this data and keep everything in sync.

- If you come across code that has been developed with a two-tiered approach in which business logic is embedded into the user interface, you need to separate the behaviors. Much of this is about decomposing and moving methods. For the data, however, you cannot just move the data, you have to duplicate it and provide the synchronization mechanism.

### Change Unidirectional Association to Bidirectional
You have two classes that need to use each other's features, but there is only a one-way link.

Add back pointers, and change modifiers to update both sets

![](https://i.imgur.com/x9G75k3.png)

Example

A simple program has an order that refers to a customer:
```java
class Order...
    Customer getCustomer() {
        return _customer;
    }

    void setCustomer (Customer arg) {
        _customer = arg;
    }
    Customer _customer;
```

The customer class has no reference to the order.

As a customer can have several orders, so this field is a collection. Because I don't want a customer to have the same order more than once in its collection, the correct collection is a set:
```java
class Customer {
    private Set _
```

Now I need to decide which class will take charge of the association. My decision process runs as follows:

1. If both objects are reference objects and the association is one to many, then the object that has the one reference is the controller. (That is, if one customer has many orders, the order controls the association.)

2. If one object is a component of the other, the composite should control the association.

3. If both objects are reference objects and the association is many to many, it doesn't matter whether the order or the customer controls the association.

Because the order will take charge, I need to add a helper method to the customer that allows direct access to the orders collection.

I use the name friendOrders to signal that this method is to be used only in this special case. I also minimize its visibility by making it package visibility if at all possible.
```java
class Customer...
Set friendOrders() {
    /** should only be used by Order when modifying the association */
    return _orders;
}
```

Now I update the modifier to update the back pointers:
```java
class Order...
    void setCustomer (Customer arg) ...
        if (_customer != null) _customer.friendOrders().remove(this);
        _customer = arg;
        if (_customer != null) _customer.friendOrders().add(this);
}
```

### Change Bidirectional Association to Unidirectional
You have a two-way association but one class no longer needs features from the other.

Drop the unneeded end of the association

Motivation

Bidirectional associations are useful, but they carry a price. The price is the added complexity of maintaining the two-way links and ensuring that objects are properly created and removed.

Bidirectional associations are not natural for many programmers, so they often are a source of errors.

Lots of two-way links also make it easy for mistakes to lead to zombies: objects that should be dead but still hang around because of a reference that was not cleared.

Bidirectional associations force an interdependency between the two classes. Any change to one class may cause a change to another.

You should use bidirectional associations when you need to but not when you don't. As soon as you see a bidirectional association is no longer pulling its weight, drop the unnecessary end.

### Replace Magic Number with Symbolic Constant
```java
double potentialEnergy(double mass, double height) {
    return mass * 9.81 * height;
}
```
---
```java
double potentialEnergy(double mass, double height) {
    return mass * GRAVITATIONAL_CONSTANT * height;
}
static final double GRAVITATIONAL_CONSTANT = 9.81;
```

Motivation

Magic numbers are really nasty when you need to reference the same
logical number in more than one place. If the numbers might ever change, making the change is a nightmare.

Before you do this refactoring, you should always look for an alternative. Look at how the magic number is used. Often you can find a better way to use it. If the magic number is a type code, consider **Replace Type Code with Class**. If the magic number is the length of an array, use `anArray.length` instead.

### Encapsulate Field
