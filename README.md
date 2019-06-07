# CsSelenium
## Introduction
To build this simple frame I looked closely to the Selenide project built in JAVA.

--> selenide.org

This project truely managed to capture Selenium in quite a straighforward framework.

As it stands now the framework is nothing like Selenide for what concerns the architecture. Selenide is miles ahead for what concerns maturity when it comes to that. This project does shares core ideas.

I took this on as an exercise, and I do not intend to plagiarize. I personally find the Selenide framework inspiring. The general idea behind the framework is copied shamelessly from Selenide.

## Philosophy
### Simple interface
Whenever a letter of code is writting: KISS.

Whenever we need a complex item:
1. Build the item
2. Make it avaialbe in case someone really wants it
3. Make sure it is readily available behind the scenes

### Simple searches
Example: Threadbased driver + WebDriverFactory.
The framework will generate a driver when the thread requesting it is new (for parallel execution).
The logic to manage and call the driver is hidden in behind the scenes classes.


The user can have code like this:

```C#
namespace SomeTest
{
    [TestClass]
    public class SomeTests
    {
        [Initialize]
        public void Initialize()
        {
            open("http://www.somepagehere.someext");
        }
        
        [Cleanup]
        public void Cleanup()
        {
            QuiteAndDestroy();
        }
        
        [TestMethod]
        public void SomeTestOne()
        {
            f("some css.reference").GetText().Equals("the stuff to equal");
        }
        
        [TestMethod]
        public void SomeTestTwo()
        {
            ff("css.ref to many").Size() == 5;
        }
    }
}
```

Running these tests in Parrallel will create two drivers. One per thread for a test.
With 'open()' you call a driver (created when not there yet).

To find a element you just go f("identifier") for css. For xpath you go fx("identifier"). You can still use the selenium basics to insert finds with By objects like f(By.CssSelector("some selector")).

To find many objects you type ff("identifier") which will return a list of elements.

That's all there is right now:
- this framework removes boilerplating for finds
- the framework manages the webdriver on a thread basis

### Assertions on framework level
The user has the ability to create custom assertions with frameworks and assertion librarires of choice by retrieving the web element object from the CsSeElement object. This provides total freedom.

The assertions on framework level are built to make the process of asserting both fluent and easy.

The only Assertions framework implemented at this point in time are the assertion libraries from Microsoft.VisualStudio.TestTools.UnitTesting.

### Logging on framework level
The user has the ability to launch a log and write that log to disk. The log is stored as a JSON array that can be marshalled into a html reporting template if the user likes.

As it stands the logging offers a comprehensive message, an error message that is formulated automatically and (should be) custom per log event.

There are different log events to eventually support nested logged items in order to drill down in results from one single json blob per run.

## Architecture notes
### High-Level Architecture
#### Statics
The statics folder contains the static imports that are needed to work with the framework.

The choice to make these methods static is because we then don't need to create objects ourselves. We call the static methods, and those methods serve an object. The user then works with this object.

There are five types at this point in time:

|Class| Description|Remarks|
|-----|------------|-------|
|CsSe|First entrypoint. Functions like the initial finds and the 'open' statement are included herein.|If you start a project, this is what you import and continue from there. To import, use `using static` rather then just `using` in order to be able to write the static methods directly in your code like shown above.|
|CsSeAction|A class supporting modelled actions. All actions extend the abstract class `Action`.|Although it might seem redundant to put the actions in separate classes, it will improve the ability to maintain what we want to do with the standard framework with regards to catching and 'translating' error messages for reporting purposes. Actions can be `Click`, but also the standardized checks in the form of Conditions can be launched with the `ShouldAction`class implemented in the custom `CsSeElement`class holding a custom implementation of the IWebElement object.|
|CsSeCondition|A class supporting modelled conditions. All conditions extend the abstract class `Condition`.|It follows the Actions architecture mantra. This to control logic centrally and outside of the `CsSeElement`class.|
|CsSeConfigurationManager|An object that holds configuration.|Right now: Hardcode defaults + Callable from code to update any of the available values. Todo here is to make it possioble to load custom defaults in an object that is extendable rather then a pure coded class.|
|CsSeDriver|This class manages the driver. The class is (or should be) thread-safe.|When `using static CsSeSeleniumFrame.src.statics.CsSeDriver`you can get the driver for the current thread just by calling GetDriver(). The thread-safety is implemented for synchronous execution.|

These classes are supported mainly by the classes in the `src.Core`namespace. This namespace holds the classes that are at the core of the framework.

Basically the `CsSeSeleniumFrame.src.Statics` should be exactly that: pure static references that return objects. The logic itself should be in the `CsSeSeleniumFrame.src.Core`namespace.

#### Core
The `Core` package contains the core components. It might be necessary to import these libraries in `using` statements when developing a custom implementation of the framework.

For what concerns the other packages, they are not commenly imported by creators of page objects or tests. Only framework maintainers are likely to import and use those items.

The `Core` package is meant as a bridge between what is strictly necessary and more programmatical freedom. Essentially a user should be able to start testing only by importing `Statics.CsSe`. For page objects the user will need `Core.CsSeElement` and `Core.CsSeElementCollection` as well.

The Core components as they are now:

|Class| Description|Remarks|
|-----|------------|-------|
|CsSeElement|This is a true core element. CsSeElement implements IWebElement and is therefore usable in existing operations that possibly already exist to handle elements. There is however extra functionality to make things easier like the continued fluent search with `f("string here")`like explained above. Also the checks that are done here are diverted to CsSeActions that are written to a log if desired. At this point in time not all actions are implemented. It remains to be seen if we continue to go down this road.|This is a core element that will not disappear. It will continue to comply to IWebElement. It might be extended, and the implementation of the contract methods and properties might change over time to evolve the framework.
|CsSeElementCollection|This class is analogous to the one above except that it does not implement an interface. It is a collection of `IWebElement`s. For now it will be just that. In the future this collection should be made into a collection of `CsSeElement` instances. It is to be seen how this will be done most efficiently. For now it is a collection of `IWebElement` instances with a separate set of conditions (extending the standard `Condition` class as will be discussed below. At this point in time there are no conditions written yet for `CsSeElementCollection`s.|This item will evolve. Now it is mainly a wrapper to be ready for a custom implementation in the framework. 
|CsSeProperties|Implementation of a properties file. At this point in time the file is flat, but it could be extended with objects. In essence it would be the goal to have a serializable JSON that can be imported and merged so that you only have to define the non-standard values.|This feature only exists in code now. Hardcoded defaults, defaults need to be overridden from the test code.
|CsSeScreenshot|Wrapper to handle screenshots. Goal is to hide a way some repetitive actions that need to be taken care of with regards to logging and maybe logging to the reporting engine when taking screenshots for occasions like image compare and 'save image on end of test' kind of actions.| |
|WebDriverFactory|As might be expected the `IWebDriver` factory.|In the future to be extended with an abstract webdriver factory class that will have multiple specific implementations in order to keep the code clean per implementation.
|WebDriverTypes|Enum of webdriver types. Currently included: \[Chrome, Firefox, InternetExplorer, Remote\].

#### Actions

#### Ex (Exceptions)

#### Conditions

#### Logger

# Roadmap

- Assertions (to remove the boilerplate code there as well)
- Logging (to trace back the results) (!) --> First up
- Describe architecture
